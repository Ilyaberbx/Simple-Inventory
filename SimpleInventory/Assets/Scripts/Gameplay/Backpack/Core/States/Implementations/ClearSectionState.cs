using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Services;

namespace Gameplay.Backpack.Core.States
{
    public sealed class ClearSectionState : BaseBackpackState
    {
        private readonly List<IStorable> _itemsWaitingForClearing;
        private readonly IBackpackContext _backpackContext;
        private InputService _inputService;

        public ClearSectionState(List<IStorable> itemsWaitingForClearing, IBackpackContext backpackContext)
        {
            _itemsWaitingForClearing = itemsWaitingForClearing;
            _backpackContext = backpackContext;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _inputService = ServiceLocator.Get<InputService>();
            _inputService.Lock();
            return ProcessItems(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _inputService.Unlock();
            return Task.CompletedTask;
        }

        private async Task ProcessItems(CancellationToken token)
        {
            var tasks = new List<Task>();

            foreach (var storable in _itemsWaitingForClearing.ToList())
            {
                var task = ProcessItem(storable, token);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            if (_itemsWaitingForClearing.IsEmpty())
            {
                return;
            }

            await ProcessItems(token);
        }

        private async Task ProcessItem(IStorable storable, CancellationToken token)
        {
            if (!await TryProcessHandlers(storable, token))
            {
                return;
            }

            _backpackContext.Clear(storable, storable.SectionType);
            _itemsWaitingForClearing.Remove(storable);
            storable.EnableForStoring();
        }

        private async Task<bool> TryProcessHandlers(IStorable storable, CancellationToken token)
        {
            var handlers = storable.CreateClearHandlers(_backpackContext);

            if (handlers.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var handler in handlers)
            {
                var successStep = await handler.TryHandle(token);

                if (!successStep)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
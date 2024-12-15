using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Gameplay.Services.Items.Persistent;
using Services;
using UnityEngine;

namespace Gameplay.Backpack.Core.States
{
    public sealed class StoreItemState : BaseBackpackState
    {
        private readonly StoreItemData _data;

        private ItemsStorageService _storageService;
        private InputService _inputService;

        public StoreItemState(StoreItemData data)
        {
            _data = data;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            _storageService = ServiceLocator.Get<ItemsStorageService>();
            _inputService = ServiceLocator.Get<InputService>();

            _inputService.Lock();
            await ProcessItems(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _inputService.Unlock();
            return Task.CompletedTask;
        }

        private async Task ProcessItems(CancellationToken token)
        {
            var tasks = new List<Task>();

            foreach (var storable in _data.ItemsWaitingForStoring.ToList())
            {
                var task = ProcessItem(token, storable);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            if (_data.ItemsWaitingForStoring.IsEmpty())
            {
                return;
            }

            await ProcessItems(token);
        }

        private async Task<bool> ProcessItem(CancellationToken token, IStorable storable)
        {
            storable.DisableForStoring();

            if (!await TryProcessHandlers(storable, token))
            {
                _data.ItemsWaitingForStoring.Remove(storable);
                return false;
            }

            Store(storable);
            ShowFx();
            _data.ItemsWaitingForStoring.Remove(storable);
            return true;
        }

        private async Task<bool> TryProcessHandlers(IStorable storable, CancellationToken token)
        {
            var backpackContext = _data.BackpackContext;
            var handlers = storable.CreateStoreHandlers(backpackContext);

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

        private void Store(IStorable storable)
        {
            var item = storable.ItemType;
            var section = storable.SectionType;

            var backpackContext = _data.BackpackContext;

            backpackContext.Store(section, storable);
            _storageService.Add(item, section);
        }

        private void ShowFx()
        {
            var vfxPrefab = _data.VfxPrefab;
            var point = _data.VfxPoint;
            var vfx = Object.Instantiate(vfxPrefab, point.position, Quaternion.identity);
            vfx.Play();
        }
    }
}
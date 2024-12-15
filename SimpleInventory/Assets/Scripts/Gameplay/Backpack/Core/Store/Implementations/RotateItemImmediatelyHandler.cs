using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Items;

namespace Gameplay.Backpack.Core.Implementations
{
    public sealed class RotateItemImmediatelyHandler : IStoreHandler
    {
        private readonly BaseItemBehaviour _item;

        public RotateItemImmediatelyHandler(BaseItemBehaviour item)
        {
            _item = item;
        }

        public Task<bool> TryHandle(CancellationToken token)
        {
            if (_item == null)
            {
                return Task.FromResult(false);
            }

            _item.RotateToStoreImmediately();
            return Task.FromResult(true);
        }
    }
}
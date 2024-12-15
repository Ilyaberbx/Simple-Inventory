using System.Threading;
using System.Threading.Tasks;
using Gameplay.Items;

namespace Gameplay.Backpack.Core.Implementations
{
    public class RotateItemHandler : IStoreHandler
    {
        private readonly BaseItemBehaviour _item;

        public RotateItemHandler(BaseItemBehaviour item)
        {
            _item = item;
        }

        public async Task<bool> TryHandle(CancellationToken token)
        {
            if (_item == null)
            {
                return false;
            }

            await _item.RotateToStore();
            return true;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Backpack.Core.Implementations
{
    public sealed class MoveItemHandler : IStoreHandler
    {
        private readonly Vector3 _to;
        private readonly BaseItemBehaviour _item;

        public MoveItemHandler(Vector3 to
            , BaseItemBehaviour item)
        {
            _to = to;
            _item = item;
        }

        public async Task<bool> TryHandle(CancellationToken token)
        {
            if (_item == null)
            {
                return false;
            }

            await _item.Jump(_to);
            return true;
        }
    }
}
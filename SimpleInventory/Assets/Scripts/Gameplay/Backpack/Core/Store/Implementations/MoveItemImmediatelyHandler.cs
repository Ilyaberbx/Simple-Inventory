using System.Threading;
using System.Threading.Tasks;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Backpack.Core.Implementations
{
    public class MoveItemImmediatelyHandler : IStoreHandler
    {
        private readonly BaseItemBehaviour _itemBehaviour;
        private readonly Vector3 _at;

        public MoveItemImmediatelyHandler(BaseItemBehaviour itemBehaviour, Vector3 at)
        {
            _itemBehaviour = itemBehaviour;
            _at = at;
        }

        public Task<bool> TryHandle(CancellationToken token)
        {
            if (_itemBehaviour == null)
            {
                return Task.FromResult(false);
            }

            _itemBehaviour.transform.position = _at;
            return Task.FromResult(true);
        }
    }
}
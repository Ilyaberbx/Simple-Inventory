using System.Linq;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Gameplay.Backpack.Core;
using Gameplay.Core;
using Gameplay.Items;
using Gameplay.Services.Items;
using UnityEngine;

namespace Gameplay.Commons
{
    public sealed class RestoreItemsBehaviour : MonoBehaviour
    {
        [SerializeField] private ItemPointData[] _restoreItemsData;
        [SerializeField] private BackpackBehaviour _backpackBehaviour;

        private ItemsRegisterService _itemsRegisterService;
        private IBackpackContext BackpackContext => _backpackBehaviour.Context;

        private void Start()
        {
            _itemsRegisterService = ServiceLocator.Get<ItemsRegisterService>();
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            if (!_backpackBehaviour.IsWaitingForItems)
            {
                return;
            }

            var items = _itemsRegisterService.Items;

            if (items.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in items)
            {
                var item1 = item;
                var data = _restoreItemsData.FirstOrDefault(temp => temp.ItemType == item1.ItemType);

                if (data == null)
                {
                    return;
                }

                if (BackpackContext.IsOccupied(item.SectionType))
                {
                    continue;
                }

                item.transform.position = data.Point.position;
            }
        }
    }
}
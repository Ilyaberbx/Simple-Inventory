using System.Linq;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Gameplay.Backpack.Core;
using Gameplay.Core;
using Gameplay.Services.Items;
using Services;
using UnityEngine;

namespace Gameplay.Commons
{
    public sealed class RestoreItemsBehaviour : MonoBehaviour
    {
        [SerializeField] private KeyCode _restoreKeyCode;
        [SerializeField] private ItemPointData[] _restoreItemsData;
        [SerializeField] private BackpackBehaviour _backpackBehaviour;

        private ItemsRegisterService _itemsRegisterService;
        private InputService _inputService;
        private IBackpackContext BackpackContext => _backpackBehaviour.Context;

        private void Start()
        {
            _itemsRegisterService = ServiceLocator.Get<ItemsRegisterService>();
            _inputService = ServiceLocator.Get<InputService>();
        }

        private void Update()
        {
            if (!_inputService.GetKeyDown(_restoreKeyCode)) return;

            var items = _itemsRegisterService.Items;

            if (items.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in items)
            {
                var data = _restoreItemsData.FirstOrDefault(temp => temp.ItemType == item.ItemType);

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
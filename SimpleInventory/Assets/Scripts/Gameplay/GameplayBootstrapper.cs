using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Gameplay.Backpack.Core;
using Gameplay.Camera;
using Gameplay.Core;
using Gameplay.Services.Items;
using Services;
using UnityEngine;

namespace Gameplay
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private ItemPointData[] _itemsPointData;
        [SerializeField] private BackpackBehaviour _backpackBehaviour;
        [SerializeField] private CameraLookAroundBehaviour _cameraLookAround;

        private UserService _userService;
        private ItemsRegisterService _itemsRegisterService;

        private async void Start()
        {
            _userService = ServiceLocator.Get<UserService>();
            _itemsRegisterService = ServiceLocator.Get<ItemsRegisterService>();
            var backpackData = _userService.BackpackDataProperty.Value;

            await InitializeBackpack(backpackData);
            InitializeItems(backpackData.Sections);
            _cameraLookAround.SetPivot(_cameraTarget);
        }

        private void InitializeItems(IReadOnlyCollection<SectionRuntimeData> sectionsRuntimeData)
        {
            foreach (var itemPointData in _itemsPointData)
            {
                if (sectionsRuntimeData.Any(temp => temp.Item == itemPointData.ItemType))
                {
                    continue;
                }

                var itemType = itemPointData.ItemType;
                var point = itemPointData.Point;
                _itemsRegisterService.New(itemType, point.position);
            }
        }

        private Task InitializeBackpack(BackpackRuntimeData backpackData)
        {
            return _backpackBehaviour.SetRuntime(backpackData);
        }
    }
}
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Gameplay.Backpack.Core;
using Gameplay.Items;
using Gameplay.Services.Items;
using Services;
using UnityEngine;

namespace Gameplay
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private BackpackBehaviour _backpackBehaviour;
        [SerializeField] private Transform _appleSpawnPoint;
        [SerializeField] private Transform _bookSpawnPoint;
        [SerializeField] private Transform _bottleSpawnPoint;

        private UserService _userService;
        private ItemsCreationService _itemsCreationService;

        private async void Start()
        {
            _userService = ServiceLocator.Get<UserService>();
            _itemsCreationService = ServiceLocator.Get<ItemsCreationService>();

            await InitializeBackpack();
            InitializeItems();
        }

        private void InitializeItems()
        {
            _itemsCreationService.New(ItemType.Apple, _appleSpawnPoint.position);
            _itemsCreationService.New(ItemType.Book, _bookSpawnPoint.position);
            _itemsCreationService.New(ItemType.Bottle, _bottleSpawnPoint.position);
        }

        private Task InitializeBackpack()
        {
            var backpackData = _userService.BackpackDataProperty.Value;

            return _backpackBehaviour.SetRuntime(backpackData);
        }
    }
}
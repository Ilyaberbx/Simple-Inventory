using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Gameplay.Items;
using Gameplay.Section;
using Gameplay.Services.Items.Configuration;
using Gameplay.Services.Items.Networking;
using UnityEngine;

namespace Gameplay.Backpack.Core
{
    public sealed class BackpackNetworkingBehaviour : MonoBehaviour
    {
        [SerializeField] private BackpackBehaviour _backpackBehaviour;

        private ItemsNetworkingService _networkingService;
        private ItemsConfigurationService _configurationService;

        private void Start()
        {
            _networkingService = ServiceLocator.Get<ItemsNetworkingService>();
            _configurationService = ServiceLocator.Get<ItemsConfigurationService>();
            _backpackBehaviour.OnStoredEvent.AddListener(OnItemStored);
            _backpackBehaviour.OnClearedEvent.AddListener(OnSectionCleared);
        }

        private void OnDestroy()
        {
            _backpackBehaviour.OnStoredEvent.RemoveListener(OnItemStored);
            _backpackBehaviour.OnClearedEvent.RemoveListener(OnSectionCleared);
        }

        private void OnSectionCleared(ItemType item, BackpackSectionType section)
        {
            Send(item).Forget();
        }

        private void OnItemStored(ItemType item, BackpackSectionType section)
        {
            Send(item).Forget();
        }

        private async Task Send(ItemType item)
        {
            if (item == ItemType.None)
            {
                return;
            }

            var configuration = _configurationService.GetConfiguration(item);

            if (configuration == null)
            {
                return;
            }

            var dto = new ItemNetworkDto(item, configuration.Name, configuration.Weight);

            var response = await _networkingService.PostRequest(dto);

            Debug.Log(response);
        }
    }
}
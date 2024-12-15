using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Gameplay.Items;
using Gameplay.Services.Items.Configuration;
using UnityEngine;

namespace Gameplay.Services.Items
{
    [Serializable]
    public sealed class ItemsCreationService : PocoService
    {
        private ItemsFactory _factory;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            var configurationService = ServiceLocator.Get<ItemsConfigurationService>();
            _factory = new ItemsFactory(configurationService);
            return Task.CompletedTask;
        }

        public BaseItemBehaviour New(ItemType type, Vector3 at, Transform parent = null)
        {
            return _factory.Create(type, at, parent);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Services.Items
{
    [Serializable]
    public sealed class ItemsRegisterService : PocoService
    {
        private ItemsFactory _factory;
        public IReadOnlyList<BaseItemBehaviour> Items => _items;
        private readonly List<BaseItemBehaviour> _items = new();

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
            var item = _factory.Create(type, at, parent);
            _items.Add(item);

            return item;
        }
    }
}
using Gameplay.Services.Items;
using UnityEngine;

namespace Gameplay.Items
{
    public sealed class ItemsFactory
    {
        private readonly IConfigurationProvider _configurationProvider;
        private const string RootDirectory = "Items/";

        public ItemsFactory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public BaseItemBehaviour Create(ItemType type, Vector3 at, Transform parent)
        {
            var loadName = type.ToString();

            if (string.IsNullOrEmpty(loadName))
            {
                return null;
            }

            var path = GetLoadingPath(loadName);
            var prefab = Resources.Load<BaseItemBehaviour>(path);
            var item = Object.Instantiate(prefab, at, Quaternion.identity, parent);
            var configuration = _configurationProvider.GetConfiguration(type);
            item.Initialize(configuration);
            return item;
        }

        private string GetLoadingPath(string loadName)
        {
            return RootDirectory + loadName;
        }
    }
}
using System;
using System.Linq;
using Better.Services.Runtime;
using Gameplay.Items;

namespace Gameplay.Services.Items.Configuration
{
    [Serializable]
    public class ItemsConfigurationService : PocoService<ItemsConfigurationServiceSettings>, IConfigurationProvider
    {
        public ItemConfiguration GetConfiguration(ItemType type)
        {
            var configuration = Settings.Configurations.FirstOrDefault(temp => temp.Type == type);
            return configuration;
        }
    }
}
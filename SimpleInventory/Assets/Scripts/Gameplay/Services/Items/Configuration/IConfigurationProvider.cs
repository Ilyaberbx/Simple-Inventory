using Gameplay.Items;

namespace Gameplay.Services.Items.Configuration
{
    public interface IConfigurationProvider
    {
        public ItemConfiguration GetConfiguration(ItemType type);
    }
}
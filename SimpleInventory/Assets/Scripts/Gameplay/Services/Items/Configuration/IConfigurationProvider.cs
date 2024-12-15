using Gameplay.Items;

namespace Gameplay.Services.Items
{
    public interface IConfigurationProvider
    {
        public ItemConfiguration GetConfiguration(ItemType type);
    }
}
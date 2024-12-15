using Gameplay.Backpack.Core;
using Gameplay.Items;

namespace Gameplay.Utils
{
    public sealed class ItemInitializationDto : ItemBehaviourDto
    {
        public bool OpenAfterHandling { get; }

        public ItemInitializationDto(IBackpackContext backpackContext, 
            ItemConfiguration configuration,
            BaseItemBehaviour item,
            bool openAfterHandling) : base(backpackContext, configuration, item)
        {
            OpenAfterHandling = openAfterHandling;
        }
    }
}
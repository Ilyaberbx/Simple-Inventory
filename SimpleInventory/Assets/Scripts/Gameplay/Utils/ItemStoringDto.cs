using Gameplay.Backpack.Core;
using Gameplay.Items;

namespace Gameplay.Utils
{
    public class ItemBehaviourDto
    {
        public IBackpackContext BackpackContext { get; }
        public ItemConfiguration Configuration { get; }
        public bool CloseCoverAfterHandling { get; }
        public BaseItemBehaviour Item { get; }

        public ItemBehaviourDto(IBackpackContext backpackContext, ItemConfiguration configuration,
            bool closeCoverAfterHandling, BaseItemBehaviour item)
        {
            BackpackContext = backpackContext;
            Configuration = configuration;
            CloseCoverAfterHandling = closeCoverAfterHandling;
            Item = item;
        }
    }
}
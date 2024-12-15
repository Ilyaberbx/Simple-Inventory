using Gameplay.Backpack;
using Gameplay.Backpack.Core;
using Gameplay.Backpack.Core.Implementations;

namespace Gameplay.Items
{
    public sealed class BookBehaviour : BaseItemBehaviour
    {
        public override IStoreHandler[] CreateStoreHandlers(IBackpackContext backpackContext)
        {
            var storeSection = Configuration.StoreSectionType;

            if (!backpackContext.TryGetStorePosition(storeSection, out var to))
            {
                return new IStoreHandler[] { new FailureHandler() };
            }

            var openPocket = new ToggleSectionHandler(true, backpackContext, storeSection);
            var storeItem = new MoveItemHandler(to, this);
            var closePocket = new ToggleSectionHandler(false, backpackContext, storeSection);
            var rotateItems = new RotateItemHandler(this);

            return new IStoreHandler[]
            {
                new StoreHandlersBlock(openPocket, storeItem, rotateItems),
                closePocket
            };
        }

        public override IStoreHandler[] CreateClearHandlers(IBackpackContext backpackContext)
        {
            var storeSection = Configuration.StoreSectionType;

            if (!backpackContext.TryGetClearPosition(storeSection, out var to))
            {
                return new IStoreHandler[] { new FailureHandler() };
            }

            var openPocket = new ToggleSectionHandler(true, backpackContext, storeSection);
            var clearItem = new MoveItemHandler(to, this);
            var closePocket = new ToggleSectionHandler(false, backpackContext, storeSection);

            return new IStoreHandler[] { openPocket, clearItem, closePocket };
        }

        public override IStoreHandler[] CreateSavedHandlers(IBackpackContext backpackContext)
        {
            var storeSection = Configuration.StoreSectionType;

            if (!backpackContext.TryGetStorePosition(storeSection, out var to))
            {
                return new IStoreHandler[] { new FailureHandler() };
            }

            var rotateImmediately = new RotateItemImmediatelyHandler(this);
            var moveImmediately = new MoveItemImmediatelyHandler(this, to);

            return new IStoreHandler[] { rotateImmediately, moveImmediately };
        }
    }
}
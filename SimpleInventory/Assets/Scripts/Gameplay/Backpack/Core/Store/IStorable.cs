using Gameplay.Items;
using Gameplay.Section;
using UnityEngine;

namespace Gameplay.Backpack.Core
{
    public interface IStorable
    {
        IStoreHandler[] CreateStoreHandlers(IBackpackContext backpackContext);
        IStoreHandler[] CreateClearHandlers(IBackpackContext backpackContext);
        IStoreHandler[] CreateSavedHandlers(IBackpackContext backpackContext);
        void DisableForStoring();
        void EnableForStoring();
        Transform Transform { get; }
        ItemType ItemType { get; }
        BackpackSectionType SectionType { get; }
    }
}
using System;
using System.Threading.Tasks;
using Gameplay.Items;
using Gameplay.Section;
using UnityEngine;

namespace Gameplay.Backpack.Core
{
    /// <summary>
    /// This interface provides an API for interacting with a backpack system. It includes methods for storing and clearing items, 
    /// opening and closing backpack sections, and checking the state of the backpack (e.g., whether a section is open or occupied).
    /// Events are triggered when items are stored or cleared.
    /// </summary>
    public interface IBackpackContext
    {
        event Action<ItemType, BackpackSectionType> OnCleared;
        event Action<ItemType, BackpackSectionType> OnStored;
        bool TryGetStorePosition(BackpackSectionType type, out Vector3 position);
        bool TryGetClearPosition(BackpackSectionType type, out Vector3 position);
        Task Open(BackpackSectionType type);
        Task Close(BackpackSectionType type);
        void OpenImmediately(BackpackSectionType type);
        void CloseImmediately(BackpackSectionType type);
        bool TryGetStorable(BackpackSectionType type, out IStorable storable);
        void Store(BackpackSectionType type, IStorable storable);
        void Clear(IStorable storable, BackpackSectionType type);
        bool IsOpened(BackpackSectionType type);
        bool IsOccupied(BackpackSectionType type);
    }
}
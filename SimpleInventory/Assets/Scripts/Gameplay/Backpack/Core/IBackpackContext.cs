using System.Threading.Tasks;
using Gameplay.Items;
using Gameplay.Section;
using UnityEngine;

namespace Gameplay.Backpack.Core
{
    public interface IBackpackContext
    {
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
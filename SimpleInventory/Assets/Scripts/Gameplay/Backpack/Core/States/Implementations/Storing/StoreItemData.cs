using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Backpack.Core.States
{
    public sealed class StoreItemData
    {
        public ParticleSystem VfxPrefab { get; }
        public Transform VfxPoint { get; }
        public IBackpackContext BackpackContext { get; }
        public List<IStorable> ItemsWaitingForStoring { get; }

        public StoreItemData(ParticleSystem vfxPrefab,
            Transform vfxPoint,
            IBackpackContext backpackContext,
            List<IStorable> itemsWaitingForStoring)
        {
            VfxPoint = vfxPoint;
            BackpackContext = backpackContext;
            ItemsWaitingForStoring = itemsWaitingForStoring;
            VfxPrefab = vfxPrefab;
        }
    }
}
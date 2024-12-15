using System;
using System.Collections.Generic;
using Gameplay.Items;
using Gameplay.Section;

namespace Gameplay.Backpack.Core
{
    [Serializable]
    public sealed class BackpackRuntimeData
    {
        public List<SectionRuntimeData> Sections = new();
    }

    [Serializable]
    public sealed class SectionRuntimeData
    {
        public ItemType Item;
        public BackpackSectionType SectionType;
        public bool IsOccupied => Item != ItemType.None;
    }
}
using Gameplay.Items;
using Gameplay.Section;
using UnityEngine;

namespace UI.Sections
{
    public sealed class SectionInfoModel
    {
        public Sprite ItemIcon { get; }

        public ItemType ItemType { get; }

        public BackpackSectionType SectionType { get; }

        public string Name { get; }

        public float Weight { get; }

        public SectionInfoModel(Sprite itemIcon,
            ItemType itemType,
            BackpackSectionType sectionType,
            string name,
            float weight)
        {
            ItemIcon = itemIcon;
            ItemType = itemType;
            SectionType = sectionType;
            Name = name;
            Weight = weight;
        }
    }
}
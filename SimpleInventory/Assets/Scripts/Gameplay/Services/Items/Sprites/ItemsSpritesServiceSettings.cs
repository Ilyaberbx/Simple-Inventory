using System;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Services.Items
{
    [CreateAssetMenu(menuName = "Create SpritesServiceSettings", fileName = "SpritesServiceSettings", order = 0)]
    public sealed class ItemsSpritesServiceSettings : ScriptableObject
    {
        [SerializeField] private SpriteData[] _sprites;

        public SpriteData[] Sprites => _sprites;
    }

    [Serializable]
    public sealed class SpriteData
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ItemType _itemType;

        public Sprite Sprite => _sprite;

        public ItemType ItemType => _itemType;
    }
}
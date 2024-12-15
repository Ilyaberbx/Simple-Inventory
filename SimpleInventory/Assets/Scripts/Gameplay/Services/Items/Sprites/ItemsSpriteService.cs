using System;
using System.Linq;
using Better.Services.Runtime;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Services.Items.Sprites
{
    [Serializable]
    public class ItemsSpriteService : PocoService<ItemsSpritesServiceSettings>
    {
        public Sprite GetSprite(ItemType type)
        {
            var data = Settings.Sprites.FirstOrDefault(temp => temp.ItemType == type);
            return data?.Sprite;
        }
    }
}
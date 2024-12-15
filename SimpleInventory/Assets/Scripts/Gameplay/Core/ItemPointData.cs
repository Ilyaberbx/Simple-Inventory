using System;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Core
{
    [Serializable]
    public sealed class ItemPointData
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Transform _point;

        public ItemType ItemType => _itemType;
        public Transform Point => _point;
    }
}
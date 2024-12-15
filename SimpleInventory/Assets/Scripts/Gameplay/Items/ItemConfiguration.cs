using System;
using Gameplay.Section;
using UnityEngine;

namespace Gameplay.Items
{
    public enum ItemType
    {
        None,
        Apple,
        Bottle,
        Book,
    }

    [Serializable]
    public class ItemConfiguration
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private BackpackSectionType _storeSectionType;
        [SerializeField] private AnimationCurve _snappingCurve;
        [SerializeField] private Vector3 _storeRotation;
        [SerializeField] private AnimationCurve _storeRotationCurve;
        [SerializeField] private float _storeRotationDuration;
        [SerializeField] private float _snappingJumpPower;
        [SerializeField] private int _snappingNumJumps;
        [SerializeField] private float _snappingDuration;
        [SerializeField] private float _weight;
        [SerializeField] private string _name;

        public float Weight => _weight;
        public string Name => _name;
        public BackpackSectionType StoreSectionType => _storeSectionType;
        public AnimationCurve SnappingCurve => _snappingCurve;
        public float SnappingDuration => _snappingDuration;
        public Vector3 StoreRotation => _storeRotation;
        public float StoreRotationDuration => _storeRotationDuration;
        public AnimationCurve StoreRotationCurve => _storeRotationCurve;
        public float SnappingJumpPower => _snappingJumpPower;
        public int SnappingNumJumps => _snappingNumJumps;
        public ItemType Type => _type;
    }
}
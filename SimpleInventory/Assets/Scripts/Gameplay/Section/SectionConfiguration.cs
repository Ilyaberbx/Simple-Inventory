using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Section
{
    [Serializable]
    public sealed class SectionConfiguration
    {
        [SerializeField] private RotateMode _rotateMode = RotateMode.Fast;
        [SerializeField] private float _toggleSpeed;
        [SerializeField] private Vector3 _openRotation;
        [SerializeField] private Vector3 _closeRotation;
        [SerializeField] private BackpackSectionType type;

        public BackpackSectionType Type => type;
        public Vector3 OpenRotation => _openRotation;
        public Vector3 CloseRotation => _closeRotation;
        public float ToggleSpeed => _toggleSpeed;

        public RotateMode RotateMode => _rotateMode;
    }
}
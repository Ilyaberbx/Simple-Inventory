using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Camera
{
    public sealed class CameraLookAroundBehaviour : MonoBehaviour
    {
        private const string CanNotFindCamera = "Can not find camera";

        [SerializeField] private float _rotationSpeed;

        private Transform _pivot;
        private UnityEngine.Camera _camera;

        public void SetPivot(Transform pivot)
        {
            _pivot = pivot;
        }

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }
    }
}
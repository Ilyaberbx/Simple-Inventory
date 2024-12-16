using Better.Locators.Runtime;
using Services;
using UnityEngine;

namespace Gameplay.Camera
{
    public sealed class CameraLookAroundBehaviour : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private KeyCode _leftKeyCode;
        [SerializeField] private KeyCode _rightKeyCode;

        private Transform _pivot;
        private Transform _cameraTransform;
        private InputService _inputService;

        public void SetPivot(Transform pivot)
        {
            _pivot = pivot;
        }

        private void Awake()
        {
            if (UnityEngine.Camera.main != null)
                _cameraTransform = UnityEngine.Camera.main.transform;
        }

        private void Start()
        {
            _inputService = ServiceLocator.Get<InputService>();
        }

        private void Update()
        {
            if (_cameraTransform == null)
            {
                return;
            }

            if (_pivot == null)
            {
                return;
            }

            if (_inputService.GetKey(_leftKeyCode))
            {
                RotateCamera(false);
                return;
            }

            if (_inputService.GetKey(_rightKeyCode))
            {
                RotateCamera(true);
            }
        }

        private void RotateCamera(bool isRight)
        {
            var multiplier = isRight ? -1 : 1;
            _cameraTransform.RotateAround(_pivot.position, Vector3.up, multiplier * _rotationSpeed * Time.deltaTime);
        }
    }
}
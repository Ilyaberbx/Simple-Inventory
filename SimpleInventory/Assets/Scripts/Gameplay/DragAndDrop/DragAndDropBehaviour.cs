using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.DragAndDrop
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class DragAndDropBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Camera _camera;
        private Rigidbody _rigidbody;
        private Vector3 _mousePosition;

        private Vector3 ScreenMousePosition => _camera.WorldToScreenPoint(transform.position);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _mousePosition = Input.mousePosition - ScreenMousePosition;
            _rigidbody.isKinematic = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = _camera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rigidbody.isKinematic = false;
        }
    }
}
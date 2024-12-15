using System;
using UnityEngine;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseTriggerObserver<TComponent> : MonoBehaviour
    {
        public event Action<TComponent> OnEntered;
        public event Action<TComponent> OnExited;
        public event Action<TComponent> OnStay;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnEntered?.Invoke(component);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnStay?.Invoke(component);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnExited?.Invoke(component);
            }
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace Services
{
    [Serializable]
    public sealed class InputService : PocoService
    {
        private bool _isLocked;
        public bool IsLocked => _isLocked;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public bool GetKeyDown(KeyCode keyCode)
        {
            return !_isLocked && Input.GetKeyDown(keyCode);
        }
    }
}
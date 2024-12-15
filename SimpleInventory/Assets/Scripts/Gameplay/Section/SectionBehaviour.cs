using System.Threading.Tasks;
using DG.Tweening;
using Gameplay.Backpack.Core;
using Gameplay.Extensions;
using UnityEngine;

namespace Gameplay.Section
{
    public sealed class SectionBehaviour : MonoBehaviour
    {
        [SerializeField] private SectionConfiguration _configuration;
        [SerializeField] private Transform _storePoint;
        [SerializeField] private Transform _clearPoint;
        [SerializeField] private Transform _cover;

        private SectionRuntimeData _runtimeData;
        public bool IsOpened { get; private set; }
        public bool IsOccupied => _runtimeData.IsOccupied;
        public BackpackSectionType Type => _configuration.Type;
        public Vector3 StorePosition => _storePoint.position;
        public Vector3 ClearPosition => _clearPoint.position;
        public IStorable Storable { get; private set; }

        public void SetRuntime(SectionRuntimeData runtimeData)
        {
            _runtimeData = runtimeData;
        }

        public void Store(IStorable storable)
        {
            Storable = storable;
            
            if (storable == null)
            {
                return;
            }

            Storable.Transform.SetParent(transform);
        }

        public async Task Open()
        {
            await RotateCover(_configuration.OpenRotation);
            IsOpened = true;
        }

        public async Task Close()
        {
            await RotateCover(_configuration.CloseRotation);
            IsOpened = false;
        }

        private Task RotateCover(Vector3 rotation)
        {
            return _cover
                .DORotate(rotation, _configuration.ToggleSpeed, _configuration.RotateMode)
                .SetSpeedBased()
                .SetRelative()
                .AsTask(destroyCancellationToken);
        }

        private void RotateImmediately(Vector3 rotation)
        {
            _cover.Rotate(rotation);
        }

        public void OpenImmediately()
        {
            RotateImmediately(_configuration.OpenRotation);
            IsOpened = true;
        }

        public void CloseImmediately()
        {
            RotateImmediately(_configuration.CloseRotation);
            IsOpened = false;
        }
    }
}
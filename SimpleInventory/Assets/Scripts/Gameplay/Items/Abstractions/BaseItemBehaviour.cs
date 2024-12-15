using System.Threading.Tasks;
using DG.Tweening;
using Gameplay.Backpack.Core;
using Gameplay.DragAndDrop;
using Gameplay.Extensions;
using Gameplay.Section;
using UnityEngine;

namespace Gameplay.Items
{
    public abstract class BaseItemBehaviour : MonoBehaviour, IStorable
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private DragAndDropBehaviour _dragAndDropBehaviour;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _interactionCollider;

        private ItemConfiguration _configuration;
        protected ItemConfiguration Configuration => _configuration;

        public ItemType ItemType => _itemType;
        public BackpackSectionType SectionType => _configuration.StoreSectionType;

        public Transform Transform => transform;

        public void Initialize(ItemConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DisableForStoring()
        {
            _dragAndDropBehaviour.enabled = false;
            _rigidbody.isKinematic = true;
            _interactionCollider.enabled = false;
        }

        public void EnableForStoring()
        {
            _dragAndDropBehaviour.enabled = true;
            _rigidbody.isKinematic = false;
            _interactionCollider.enabled = true;
        }

        public Task Jump(Vector3 to)
        {
            return transform
                .DOJump(to, _configuration.SnappingJumpPower, _configuration.SnappingNumJumps,
                    _configuration.SnappingDuration)
                .SetEase(_configuration.SnappingCurve)
                .AsTask(destroyCancellationToken);
        }

        public Task RotateToStore()
        {
            return transform
                .DORotate(_configuration.StoreRotation, _configuration.StoreRotationDuration)
                .SetEase(_configuration.StoreRotationCurve)
                .AsTask(destroyCancellationToken);
        }


        public void RotateToStoreImmediately()
        {
            transform.Rotate(_configuration.StoreRotation);
        }

        public abstract IStoreHandler[] CreateSavedHandlers(IBackpackContext backpackContext);

        public abstract IStoreHandler[] CreateStoreHandlers(IBackpackContext backpackContext);

        public abstract IStoreHandler[] CreateClearHandlers(IBackpackContext backpackContext);
    }
}
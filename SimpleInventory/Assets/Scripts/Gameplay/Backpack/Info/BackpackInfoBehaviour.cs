using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Backpack.Info
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class BackpackInfoBehaviour : MonoBehaviour, IBeginDragHandler
    {
        [SerializeField] private BackpackInfoPresenter _presenter;

        private void OnMouseDown()
        {
            var model = BackpackUIUtility.CreateInfoModel();
            _presenter.SetDerivedModel(model);
            _presenter.gameObject.SetActive(true);
        }

        private void OnMouseUp()
        {
            _presenter.gameObject.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var model = BackpackUIUtility.CreateInfoModel();
            _presenter.SetDerivedModel(model);
            _presenter.gameObject.SetActive(true);
        }
    }
}
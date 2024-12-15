using UI;
using UI.BackpackInfo;
using UI.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Backpack.Info
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class BackpackInfoBehaviour : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private BackpackInfoPresenter _presenter;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            var model = BackpackUIUtility.CreateInfoModel();
            _presenter.SetDerivedModel(model);
            _presenter.gameObject.SetActive(true);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _presenter.gameObject.SetActive(false);
        }
    }
}
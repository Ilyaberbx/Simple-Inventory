using UnityEngine;

namespace Core.MVP
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseView : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
        }

        public bool Interactable
        {
            get => CanvasGroup.interactable;
            set { CanvasGroup.interactable = value; }
        }

        public bool BlocksRayCasts
        {
            get => CanvasGroup.blocksRaycasts;
            set { CanvasGroup.blocksRaycasts = value; }
        }
    }
}
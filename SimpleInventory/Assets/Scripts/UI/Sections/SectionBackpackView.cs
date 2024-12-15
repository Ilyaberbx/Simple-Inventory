using System;
using Core.MVP;
using Gameplay.Items;
using Gameplay.Section;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Sections
{
    public sealed class SectionBackpackView : BaseView, IPointerUpHandler
    {
        public event Action<BackpackSectionType> OnUtilizeClick;
        private const string WeightFormat = "Weight: {0}";

        [SerializeField] private GameObject _itemContainer;
        [SerializeField] private BackpackSectionType _sectionType;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _weightText;
        [SerializeField] private TextMeshProUGUI _nameText;
        private SectionInfoModel _model;
        private bool _hovering;

        public BackpackSectionType SectionType => _sectionType;

        public void SetData(SectionInfoModel model)
        {
            if (model == null)
            {
                gameObject.SetActive(false);
                return;
            }

            if (model.ItemType == ItemType.None)
            {
                _itemContainer.gameObject.SetActive(false);
                return;
            }

            _model = model;
            _itemContainer.gameObject.SetActive(true);
            _itemIcon.sprite = model.ItemIcon;
            _weightText.text = string.Format(WeightFormat, model.Weight);
            _nameText.text = model.Name;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_model == null)
            {
                return;
            }

            var sectionType = _model.SectionType;
            OnUtilizeClick?.Invoke(sectionType);
        }
    }
}
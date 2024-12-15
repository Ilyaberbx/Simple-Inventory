using System.Linq;
using Better.Locators.Runtime;
using Core.MVP;
using Gameplay.Section;
using Gameplay.Services.Items;

namespace UI.BackpackInfo
{
    public sealed class BackpackInfoPresenter : BasePresenter<BackpackInfoView, BackpackInfoModel>
    {
        private ItemsStorageService _itemsStorageService;

        private void Start()
        {
            _itemsStorageService = ServiceLocator.Get<ItemsStorageService>();
        }

        private void OnEnable()
        {
            foreach (var sectionView in View.SectionViews)
            {
                sectionView.OnUtilizeClick += OnUtilizeButtonClicked;
            }
        }

        private void OnDisable()
        {
            foreach (var sectionView in View.SectionViews)
            {
                sectionView.OnUtilizeClick -= OnUtilizeButtonClicked;
            }
        }

        protected override void SetModel(BackpackInfoModel model)
        {
            base.SetModel(model);

            Rebuild();
        }

        public override void Rebuild()
        {
            View.CanvasGroup.alpha = 0;

            var sectionViews = View.SectionViews;

            foreach (var sectionModel in Model.SectionModels)
            {
                var sectionView = sectionViews.FirstOrDefault(temp => temp.SectionType == sectionModel.SectionType);

                if (sectionView == null)
                {
                    continue;
                }

                sectionView.SetData(sectionModel);
            }

            View.CanvasGroup.alpha = 1;
        }

        private void OnUtilizeButtonClicked(BackpackSectionType sectionType)
        {
            View.CanvasGroup.alpha = 0;
            _itemsStorageService.Clear(sectionType);
        }
    }
}
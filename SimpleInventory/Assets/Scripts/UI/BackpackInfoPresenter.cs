using System.Linq;
using Better.Locators.Runtime;
using Core.MVP;
using Gameplay.Section;
using Gameplay.Services.Items;
using Gameplay.Services.Items.Persistent;

namespace UI
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
        }

        private void OnUtilizeButtonClicked(BackpackSectionType sectionType)
        {
            _itemsStorageService.Clear(sectionType);
        }
    }
}
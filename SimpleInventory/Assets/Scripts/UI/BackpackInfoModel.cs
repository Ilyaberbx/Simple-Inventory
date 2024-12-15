using Core.MVP;
using Gameplay.Backpack.Info;
using UI.Sections;

namespace UI
{
    public class BackpackInfoModel : IModel
    {
        public SectionInfoModel[] SectionModels { get; }

        public BackpackInfoModel(SectionInfoModel[] sectionModels)
        {
            SectionModels = sectionModels;
        }
    }
}
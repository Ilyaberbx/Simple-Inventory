using Core.MVP;
using UI.Sections;

namespace UI.BackpackInfo
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
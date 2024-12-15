using Core.MVP;
using UI.Sections;
using UnityEngine;

namespace UI.BackpackInfo
{
    public sealed class BackpackInfoView : BaseView
    {
        [SerializeField] private SectionBackpackView[] _sectionViews;

        public SectionBackpackView[] SectionViews => _sectionViews;
    }
}
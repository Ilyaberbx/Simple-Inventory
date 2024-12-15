using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Gameplay.Backpack.Core;
using Gameplay.Items;
using Gameplay.Section;
using Services;

namespace Gameplay.Services.Items.Persistent
{
    [Serializable]
    public sealed class ItemsStorageService : PocoService
    {
        public event Action<BackpackSectionType> OnSectionCleared;

        private UserService _userService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _userService = ServiceLocator.Get<UserService>();
            return Task.CompletedTask;
        }

        public bool IsEmpty(BackpackSectionType sectionType)
        {
            var property = _userService.BackpackDataProperty;

            return property.Value.Sections.Any(temp => temp.SectionType == sectionType && temp.Item == ItemType.None);
        }

        public void Add(ItemType itemType, BackpackSectionType sectionType)
        {
            if (!IsEmpty(sectionType))
            {
                return;
            }

            var property = _userService.BackpackDataProperty;
            var sectionsData = property.Value.Sections;
            var targetSection = sectionsData.FirstOrDefault(temp => temp.SectionType == sectionType);

            if (targetSection == null)
            {
                sectionsData.Add(new SectionRuntimeData()
                {
                    Item = itemType,
                    SectionType = sectionType,
                });

                property.SetDirty();
                return;
            }

            targetSection.Item = itemType;
            property.SetDirty();
        }

        public void Clear(BackpackSectionType sectionType)
        {
            if (IsEmpty(sectionType))
            {
                return;
            }

            var property = _userService.BackpackDataProperty;
            var sectionsData = property.Value.Sections;
            var targetSection = sectionsData.FirstOrDefault(temp => temp.SectionType == sectionType);

            if (targetSection == null)
            {
                return;
            }

            targetSection.Item = ItemType.None;
            property.SetDirty();
            OnSectionCleared?.Invoke(sectionType);
        }
    }
}
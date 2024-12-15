using System.Threading;
using System.Threading.Tasks;
using Gameplay.Section;

namespace Gameplay.Backpack.Core.Implementations
{
    public class ToggleSectionImmediatelyHandler : IStoreHandler
    {
        private readonly bool _value;
        private readonly IBackpackContext _backpackContext;
        private readonly BackpackSectionType _sectionType;
        private bool IsSectionOpened => _backpackContext.IsOpened(_sectionType);

        public ToggleSectionImmediatelyHandler(bool value, IBackpackContext backpackContext,
            BackpackSectionType sectionType)
        {
            _value = value;
            _backpackContext = backpackContext;
            _sectionType = sectionType;
        }

        public Task<bool> TryHandle(CancellationToken token)
        {
            if (_value == IsSectionOpened)
            {
                return Task.FromResult(true);
            }

            if (_value)
            {
                _backpackContext.OpenImmediately(_sectionType);
            }
            else
            {
                _backpackContext.CloseImmediately(_sectionType);
            }

            return Task.FromResult(true);
        }
    }
}
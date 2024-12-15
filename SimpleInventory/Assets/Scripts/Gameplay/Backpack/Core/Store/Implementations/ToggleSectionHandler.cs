using System.Threading;
using System.Threading.Tasks;
using Gameplay.Section;

namespace Gameplay.Backpack.Core.Implementations
{
    public sealed class ToggleSectionHandler : IStoreHandler
    {
        private readonly bool _value;
        private readonly IBackpackContext _backpackContext;
        private readonly BackpackSectionType _sectionType;
        private bool IsSectionOpened => _backpackContext.IsOpened(_sectionType);

        public ToggleSectionHandler(bool value, IBackpackContext backpackContext, BackpackSectionType sectionType)
        {
            _value = value;
            _backpackContext = backpackContext;
            _sectionType = sectionType;
        }

        public async Task<bool> TryHandle(CancellationToken token)
        {
            if (_value == IsSectionOpened)
            {
                return true;
            }

            var task = _value
                ? _backpackContext.Open(_sectionType)
                : _backpackContext.Close(_sectionType);

            await task;
            return true;
        }
    }
}
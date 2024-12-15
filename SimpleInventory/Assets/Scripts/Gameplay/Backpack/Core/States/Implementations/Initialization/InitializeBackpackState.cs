using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Gameplay.Items;
using Gameplay.Services.Items;

namespace Gameplay.Backpack.Core.States
{
    public sealed class InitializeBackpackState : BaseBackpackState
    {
        private readonly InitializeBackpackData _data;

        public InitializeBackpackState(InitializeBackpackData data)
        {
            _data = data;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            var itemsCreationService = ServiceLocator.Get<ItemsRegisterService>();
            var sectionsData = _data.RuntimeData.Sections;
            var sectionBehaviours = _data.SectionBehaviours;

            for (var i = 0; i < sectionBehaviours.Length; i++)
            {
                var sectionBehaviour = sectionBehaviours[i];
                if (sectionsData.Count <= i)
                {
                    sectionsData.Add(new SectionRuntimeData()
                    {
                        SectionType = sectionBehaviour.Type,
                        Item = ItemType.None
                    });
                }

                var sectionData = sectionsData[i];
                sectionBehaviour.SetRuntime(sectionData);

                if (sectionData.Item == ItemType.None)
                {
                    continue;
                }

                var backpackContext = _data.BackpackContext;
                var storePosition = sectionBehaviour.StorePosition;
                var item = itemsCreationService.New(sectionData.Item, storePosition);
                var handlers = item.CreateSavedHandlers(backpackContext);

                item.DisableForStoring();

                foreach (var handler in handlers)
                {
                    if (!await handler.TryHandle(token))
                    {
                        return;
                    }
                }

                backpackContext.Store(sectionData.SectionType, item);
            }
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
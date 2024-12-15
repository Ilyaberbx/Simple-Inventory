using Gameplay.Section;

namespace Gameplay.Backpack.Core.States
{
    public sealed class InitializeBackpackData
    {
        public SectionBehaviour[] SectionBehaviours { get; }

        public BackpackRuntimeData RuntimeData { get; }

        public IBackpackContext BackpackContext { get; }

        public InitializeBackpackData(SectionBehaviour[] sectionBehaviours, BackpackRuntimeData runtimeData,
            IBackpackContext backpackContext)
        {
            SectionBehaviours = sectionBehaviours;
            RuntimeData = runtimeData;
            BackpackContext = backpackContext;
        }
    }
}
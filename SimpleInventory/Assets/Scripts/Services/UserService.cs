using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Saves.Runtime.Data;
using Better.Services.Runtime;
using Gameplay.Backpack.Core;

namespace Services
{
    [Serializable]
    public sealed class UserService : PocoService
    {
        public SavesProperty<BackpackRuntimeData> BackpackDataProperty { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            BackpackDataProperty = new SavesProperty<BackpackRuntimeData>(nameof(BackpackRuntimeData),new BackpackRuntimeData());
            return Task.CompletedTask;
        }
    }
}
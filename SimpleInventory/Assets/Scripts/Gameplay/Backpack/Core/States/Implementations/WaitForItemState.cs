using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Backpack.Core.States
{
    public sealed class WaitForItemState : BaseBackpackState
    {
        public override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Backpack.Core.Implementations
{
    public sealed class FailureHandler : IStoreHandler
    {
        public Task<bool> TryHandle(CancellationToken token)
        {
            return Task.FromResult(false);
        }
    }
}
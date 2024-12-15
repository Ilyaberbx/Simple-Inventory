using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Backpack.Core
{
    public interface IStoreHandler
    {
        Task<bool> TryHandle(CancellationToken token);
    }
}
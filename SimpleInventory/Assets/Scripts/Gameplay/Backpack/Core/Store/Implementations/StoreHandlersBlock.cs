using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;

namespace Gameplay.Backpack.Core.Implementations
{
    public sealed class StoreHandlersBlock : IStoreHandler
    {
        private readonly IStoreHandler[] _handlers;

        public StoreHandlersBlock(params IStoreHandler[] handlers)
        {
            _handlers = handlers;
        }

        public async Task<bool> TryHandle(CancellationToken token)
        {
            if (_handlers.IsNullOrEmpty())
            {
                return false;
            }

            var tasks = _handlers.Select(temp => temp.TryHandle(token));
            await Task.WhenAll(tasks);
            return true;
        }
    }
}
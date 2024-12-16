using System;
using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.States;

namespace Gameplay.Extensions
{
    public static class BetterPluginExtensions
    {
        /// <summary>
        /// This method catches and ignores any exceptions that occur during the state change,
        /// serving as a workaround for a potential plugin issue where exceptions need to be suppressed.
        /// </summary>
        /// <typeparam name="TState">The type of the state, which must be a subclass of BaseState.</typeparam>
        /// <param name="stateMachine">The state machine to change the state of.</param>
        /// <param name="state">The target state to transition to.</param>
        /// <param name="token">The cancellation token to cancel the operation.</param>
        /// <param name="suppress">A flag indicating whether exceptions should be suppressed (not used in the current implementation).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task ChangeStateAsync<TState>(this IStateMachine<TState> stateMachine, TState state,
            CancellationToken token, bool suppress = false) where TState : BaseState
        {
            try
            {
                await stateMachine.ChangeStateAsync(state, token);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
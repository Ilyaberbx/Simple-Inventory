using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

namespace Gameplay.Extensions
{
    public static class TweenExtensions
    {
        /// <summary>
        /// Extension method for converting a Tween into a Task that completes when the tween finishes or is canceled.
        /// The task is completed successfully when the tween finishes, and it is canceled if the tween is killed or the cancellation token is triggered.
        /// </summary>
        /// <param name="tween">The Tween instance to be converted into a Task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the task and kill the tween.</param>
        /// <returns>A Task that completes when the tween finishes, or is canceled if the tween is killed or the cancellation token is triggered.</returns>
        public static Task AsTask(this Tween tween, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            tween.OnComplete(() => { taskCompletionSource.TrySetResult(true); });
            tween.OnKill(() => { taskCompletionSource.TrySetCanceled(); });

            if (cancellationToken != CancellationToken.None)
            {
                tween.OnUpdate(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        tween.Kill();
                    }
                });
            }

            return taskCompletionSource.Task;
        }
    }
}
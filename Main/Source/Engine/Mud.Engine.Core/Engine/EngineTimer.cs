using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    public sealed class EngineTimer<T> : CancellationTokenSource, IDisposable
    {
        private Task timerTask;

        private Action<T, EngineTimer<T>> callback;

        public EngineTimer(Action<T, EngineTimer<T>> callback, T state)
        {
            this.callback = callback;
            this.StateData = state;
        }

        public T StateData { get; private set; }

        /// <summary>
        /// Starts the specified start delay.
        /// </summary>
        /// <param name="startDelay">The start delay in milliseconds.</param>
        /// <param name="interval">The interval in milliseconds.</param>
        /// <param name="callbackOnWorkerThread">if set to <c>true</c> the callback will be performed on a background thread.</param>
        public void Start(double startDelay, double interval, bool callbackOnWorkerThread = false)
        {
            this.timerTask = Task.Delay(TimeSpan.FromMilliseconds(startDelay), Token).ContinueWith(async (task, state) =>
            {
                Debug.WriteLine(string.Format(
                    "Starting engine timer for {0} with an interval of {1}ms",
                    typeof(T).Name,
                    interval));

                var tuple = (Tuple<Action<T, EngineTimer<T>>, T>)state;
                while (!IsCancellationRequested)
                {
                    if (callbackOnWorkerThread)
                    {
                        Task.Run(() => 
                        {
                            Debug.WriteLine(string.Format(
                                "Updating engine timer for {0} with Task Id: {1}",
                                typeof(T).Name,
                                task.Id));
                            tuple.Item1(tuple.Item2, this);
                        });
                    }
                    else
                    {
                        tuple.Item1(tuple.Item2, this);
                    }
                    await Task.Delay(TimeSpan.FromMilliseconds(interval), Token).ConfigureAwait(false);
                }
            }, 
            Tuple.Create(callback, StateData), 
            CancellationToken.None, 
            TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion, 
            TaskScheduler.Default);
        }

        public void Stop()
        {
            if (!this.IsCancellationRequested)
            {
                this.Cancel();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Cancel();

            base.Dispose(disposing);
        }
    }
}

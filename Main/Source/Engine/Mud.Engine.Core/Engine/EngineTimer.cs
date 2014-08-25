using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// The Engine  Timer provides a means to start a timer with a callback that can be repeated over a set interval.
    /// </summary>
    /// <typeparam name="T">A generic Type</typeparam>
    public sealed class EngineTimer<T> : CancellationTokenSource, IDisposable
    {
        /// <summary>
        /// The timer task
        /// </summary>
        private Task timerTask;

        /// <summary>
        /// The callback
        /// </summary>
        private Action<T, EngineTimer<T>> callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineTimer{T}"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        public EngineTimer(Action<T, EngineTimer<T>> callback, T state)
        {
            this.callback = callback;
            this.StateData = state;
        }

        /// <summary>
        /// Gets the state data.
        /// </summary>
        /// <value>
        /// The state data.
        /// </value>
        public T StateData { get; private set; }

        /// <summary>
        /// Starts the specified start delay.
        /// </summary>
        /// <param name="startDelay">The start delay in milliseconds.</param>
        /// <param name="interval">The interval in milliseconds.</param>
        /// <param name="isOneShot">if set to <c>true</c> [is one shot].</param>
        /// <param name="callbackOnWorkerThread">if set to <c>true</c> the callback will be performed on a background thread.</param>
        public void Start(double startDelay, double interval, bool isOneShot = false, bool callbackOnWorkerThread = false)
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

                    if (isOneShot)
                    {
                        this.Cancel();
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(interval), Token).ConfigureAwait(false);
                }
            }, 
            Tuple.Create(callback, StateData), 
            CancellationToken.None, 
            TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion, 
            TaskScheduler.Default);
        }

        /// <summary>
        /// Stops the timer for this instance.
        /// </summary>
        public void Stop()
        {
            if (!this.IsCancellationRequested)
            {
                this.Cancel();
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Threading.CancellationTokenSource" /> class and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Cancel();

            base.Dispose(disposing);
        }
    }
}

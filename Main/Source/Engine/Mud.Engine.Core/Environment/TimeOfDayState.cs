using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public abstract class TimeOfDayState : ITimeOfDayState
    {
        /// <summary>
        /// Occurs when the state's time is changed.
        /// </summary>
        public event EventHandler<TimeOfDay> TimeUpdated;

        /// <summary>
        /// Gets the name of this state.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the time of day in the game that this state begins in hours.
        /// </summary>
        /// <value>
        /// The state start time.
        /// </value>
        public abstract TimeOfDay StateStartTime { get; set; }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public TimeOfDay CurrentTime { get; protected set; }

        /// <summary>
        /// Initializes the time of day state with the supplied in-game to real-world hours factor.
        /// </summary>
        /// <param name="worldTimeFactor">The world time factor.</param>
        /// <param name="hoursPerDay">The hours per day.</param>
        public virtual void Initialize(double worldTimeFactor, int hoursPerDay)
        {
            // Calculate how many minutes in real-world it takes to pass 1 in-game hour.
            double hourInterval = 60 * worldTimeFactor;

            // Calculate how many seconds in real-world it takes to pass 1 minute in-game.
            double minuteInterval = 60 * worldTimeFactor;

            // Update the state every in-game hour, which represents n number of minutes in real-world.
            EngineTimer<TimeOfDay> timeOfDayClock = null;
            if (minuteInterval < 1)
            {
                // If the minute interval is less than 1 second,
                // then we increment by the hour to reduce excess update calls.
                timeOfDayClock = new EngineTimer<TimeOfDay>((state, clock) =>
                {
                    this.CurrentTime.IncrementByHour(1);
                    this.OnTimeUpdated();
                },
                this.CurrentTime);

                this.CurrentTime = this.StateStartTime;
                timeOfDayClock.Start(
                    TimeSpan.FromMinutes(hourInterval).TotalMilliseconds,
                    TimeSpan.FromMinutes(hourInterval).TotalMilliseconds);
            }
            else
            {
                timeOfDayClock = new EngineTimer<TimeOfDay>((state, clock) =>
                {
                    this.CurrentTime.IncrementByMinute(1);
                    this.OnTimeUpdated();
                },
                this.CurrentTime);

                this.CurrentTime = this.StateStartTime;
                timeOfDayClock.Start(
                    TimeSpan.FromSeconds(minuteInterval).TotalMilliseconds, 
                    TimeSpan.FromSeconds(minuteInterval).TotalMilliseconds);
            }
        }

        /// <summary>
        /// Called when the states time is updated.
        /// </summary>
        private void OnTimeUpdated()
        {
            EventHandler<TimeOfDay> handler = this.TimeUpdated;
            if (handler == null)
            {
                return;
            }

            handler(this, this.CurrentTime);
        }
    }
}

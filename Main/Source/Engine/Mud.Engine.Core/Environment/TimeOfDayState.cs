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
        private EngineTimer<TimeOfDay> timeOfDayClock;

        public TimeOfDayState()
        {
        }

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

            this.StateStartTime.HoursPerDay = hoursPerDay;
            this.Reset();

            // Update the state every in-game hour, which represents n number of minutes in real-world.
            if (minuteInterval < 0.4)
            {
                // If the minute interval is less than 1 second,
                // then we increment by the hour to reduce excess update calls.
                this.timeOfDayClock = new EngineTimer<TimeOfDay>((state, clock) =>
                {
                    this.CurrentTime.IncrementByHour(1);
                    this.OnTimeUpdated();
                },
                this.CurrentTime);
                this.timeOfDayClock.Start(
                    TimeSpan.FromMinutes(hourInterval).TotalMilliseconds,
                    TimeSpan.FromMinutes(hourInterval).TotalMilliseconds);
            }
            else
            {
                this.timeOfDayClock = new EngineTimer<TimeOfDay>((state, clock) =>
                {
                    this.CurrentTime.IncrementByMinute(1);
                    this.OnTimeUpdated();
                },
                this.CurrentTime);
                this.timeOfDayClock.Start(
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

        public void Reset()
        {
            if (this.timeOfDayClock != null)
            {
                this.timeOfDayClock.Stop();
            }

            this.CurrentTime = new TimeOfDay
            {
                Hour = this.StateStartTime.Hour,
                Minute = this.StateStartTime.Minute,
                HoursPerDay = this.StateStartTime.HoursPerDay
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0} starting at {1}:{2} with a curent time of {3}:{4}",
                this.Name,
                this.StateStartTime.Hour,
                this.StateStartTime.Minute,
                this.CurrentTime.Hour,
                this.CurrentTime.Minute);
        }
    }
}

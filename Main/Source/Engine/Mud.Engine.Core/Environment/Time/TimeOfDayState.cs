//-----------------------------------------------------------------------
// <copyright file="TimeOfDayState.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment.Time
{
    using System;
    using Mud.Engine.Core.Engine;

    /// <summary>
    /// Base class for all ITimeOfDayState implementations. 
    /// This handles starting the state clock and provides methods for resetting and disposing.
    /// </summary>
    public abstract class TimeOfDayState : ITimeOfDayState
    {
        /// <summary>
        /// The clock used to track the time of day.
        /// </summary>
        private EngineTimer<TimeOfDay> timeOfDayClock;

        /// <summary>
        /// Occurs when the state's time is changed.
        /// </summary>
        public event EventHandler<TimeOfDay> TimeUpdated;

        /// <summary>
        /// Gets the name of this state.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets or sets the time of day in the game that this state begins.
        /// </summary>
        public abstract TimeOfDay StateStartTime { get; set; }

        /// <summary>
        /// Gets or sets the current time.
        /// </summary>
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

            // Update the state every in-game hour or minute based on the ratio we have
            if (minuteInterval < 0.4)
            {
                this.StartStateClock(TimeSpan.FromSeconds(minuteInterval).TotalMilliseconds, (timeOfDay) => timeOfDay.IncrementByHour(1));
            }
            else
            {
                this.StartStateClock(TimeSpan.FromSeconds(minuteInterval).TotalMilliseconds, (timeOfDay) => timeOfDay.IncrementByMinute(1));
            }
        }

        /// <summary>
        /// Resets this instance current time to that if its start time.
        /// </summary>
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this.CurrentTime != null)
            {
                return string.Format(
                    "{0} starting at {1}:{2} with a curent time of {3}:{4}",
                    this.Name,
                    this.StateStartTime.Hour,
                    this.StateStartTime.Minute,
                    this.CurrentTime.Hour,
                    this.CurrentTime.Minute);
            }
            else
            {
                return string.Format(
                    "{0} starting at {1}:{2}",
                    this.Name,
                    this.StateStartTime.Hour,
                    this.StateStartTime.Minute);
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            TimeOfDayState secondState = (TimeOfDayState)obj;

            return secondState.StateStartTime.Hour == this.StateStartTime.Hour && secondState.StateStartTime.Minute == this.StateStartTime.Minute;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.StateStartTime.Hour.GetHashCode() * this.StateStartTime.Minute.GetHashCode() * this.Name.GetHashCode();
        }

        /// <summary>
        /// Starts the state clock at the specified interval, firing the callback provided.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="callback">The callback.</param>
        private void StartStateClock(double interval, Action<TimeOfDay> callback)
        {
            // If the minute interval is less than 1 second,
            // then we increment by the hour to reduce excess update calls.
            this.timeOfDayClock = new EngineTimer<TimeOfDay>(
                (state, clock) =>
                {
                    callback(state);
                    this.OnTimeUpdated();
                },
                this.CurrentTime);
            this.timeOfDayClock.Start(interval, interval);
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

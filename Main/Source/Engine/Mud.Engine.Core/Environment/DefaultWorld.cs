using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    public class DefaultWorld : IWorld
    {
        private List<ITimeOfDayState> timeOfDayStates;

        public DefaultWorld()
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
            this.HoursPerDay = 24;
            this.HoursFactor = 0.25;

            this.timeOfDayStates = new List<ITimeOfDayState>{new MorningState(), new AfternoonState(), new NightState()};
            this.Realms = new List<IRealm>();

            // Must be in the constructor. If assigned within the Initialization method
            // the property could potentially never be restored properly from the data store.
            this.CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Occurs when [time of day changed].
        /// </summary>
        public event EventHandler<ITimeOfDayState> TimeOfDayChanged;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        /// <value>
        /// The time from creation.
        /// </value>
        public double TimeFromCreation { get { return this.CreationDate.Subtract(DateTime.Now).TotalSeconds; } }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the current time of day.
        /// </summary>
        /// <value>
        /// The current time of day.
        /// </value>
        public ITimeOfDayState CurrentTimeOfDay { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used for the time of day.
        /// </summary>
        /// <value>
        /// The time of day states.
        /// </value>
        public IEnumerable<ITimeOfDayState> TimeOfDayStates
        {
            get
            {
                return this.timeOfDayStates;
            }

            set
            {
                this.timeOfDayStates.Clear();
                this.timeOfDayStates.AddRange(value);
            }
        }

        /// <summary>
        /// Gets or sets hour many hours it takes in-game to complete 1 day.
        /// </summary>
        /// <value>
        /// The hours per day.
        /// </value>
        public int HoursPerDay { get; set; }

        /// <summary>
        /// Gets or sets the hours ratio. If set to 4, it takes 4 in-game hours to equal 1 real-world hour.
        /// </summary>
        /// <value>
        /// The hours ratio.
        /// </value>
        public double HoursFactor { get; set; }

        /// <summary>
        /// Gets or sets the realms in this world.
        /// </summary>
        /// <value>
        /// The realms.
        /// </value>
        public IEnumerable<IRealm> Realms { get; set; }

        /// <summary>
        /// Initializes the world by starting the world clock and the associated Realm clocks.
        /// </summary>
        public virtual void Initialize()
        {
            // Set up our time of day clock.
            if (this.timeOfDayStates.Count == 0)
            {
                return;
            }
            else if (this.timeOfDayStates.Count == 1)
            {
                this.CurrentTimeOfDay = this.TimeOfDayStates.FirstOrDefault();
            }
            else
            {
                this.CurrentTimeOfDay = this.GetTimeOfDayState(new DateTime(2014, 1, 1, 10, 0, 0));
                this.CurrentTimeOfDay.TimeUpdated += this.CurrentTimeOfDay_TimeUpdated;
                this.CurrentTimeOfDay.Initialize(this.HoursFactor / this.HoursPerDay, this.HoursPerDay);
                this.CurrentTimeOfDay.CurrentTime.Hour = 10;
            }
        }

        protected virtual void OnTimeOfDayChanged(ITimeOfDayState timeOfDayChanged)
        {
            EventHandler<ITimeOfDayState> handler = this.TimeOfDayChanged;
            if (handler == null)
            {
                return;
            }

            handler(this, timeOfDayChanged);
        }

        private void CurrentTimeOfDay_TimeUpdated(object sender, TimeOfDay e)
        {
            Debug.WriteLine("Minute incremented.");

            ITimeOfDayState timeOfDay = this.GetTimeOfDayState();
            if (this.CurrentTimeOfDay == timeOfDay)
            {
                return;
            }

            this.CurrentTimeOfDay = timeOfDay;
            this.OnTimeOfDayChanged(timeOfDay);
        }


        private ITimeOfDayState GetTimeOfDayState(DateTime? initialTime = null)
        {
            ITimeOfDayState inProgressState = null;
            ITimeOfDayState nextState = null;

            TimeOfDay time = new TimeOfDay();
            if (initialTime == null && this.CurrentTimeOfDay != null)
            {
                time.Hour = this.CurrentTimeOfDay.CurrentTime.Hour;
                time.Minute = this.CurrentTimeOfDay.CurrentTime.Minute;
            }
            else if (initialTime != null)
            {
                time.Hour = initialTime.Value.Hour;
                time.Minute = initialTime.Value.Minute;
            }

            foreach (ITimeOfDayState state in this.TimeOfDayStates)
            {
                // If this state is a future state, then preserve it as a possible next state.
                if (state.StateStartTime.Hour > time.Hour ||
                    (state.StateStartTime.Hour >= time.Hour && state.StateStartTime.Minute > time.Minute))
                {
                    // If we do not have a next state, set it.
                    if (nextState == null)
                    {
                        nextState = state;
                        continue;
                    }
                    else
                    {
                        // We have a next state, so we must check which is sooner.
                        if (nextState.StateStartTime.Hour > state.StateStartTime.Hour &&
                            nextState.StateStartTime.Minute >= state.StateStartTime.Minute)
                        {
                            nextState = state;
                        }
                    }
                }

                if (state.StateStartTime.Hour <= time.Hour ||
                    (state.StateStartTime.Hour <= time.Hour && state.StateStartTime.Minute <= time.Minute))
                {
                    if (inProgressState == null)
                    {
                        inProgressState = state;
                        continue;
                    }
                    else
                    {
                        if (inProgressState.StateStartTime.Hour <= time.Hour &&
                            inProgressState.StateStartTime.Minute <= time.Minute)
                        {
                            inProgressState = state;
                        }
                    }
                }
            }

            if (inProgressState != null)
            {
                return inProgressState;
            }
            else if (nextState != null)
            {
                 return nextState;
            }

            return null;
        }
    }
}

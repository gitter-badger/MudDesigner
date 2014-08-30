//-----------------------------------------------------------------------
// <copyright file="DefaultRealm.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Engine;

    /// <summary>
    /// The Default Realm class for the engine.
    /// </summary>
    public class DefaultRealm : IRealm
    {
        private List<IWeatherState> weatherStates;

        private IWorld world;

        /// <summary>
        /// The time of day state manager
        /// </summary>
        private TimeOfDayStateManager timeOfDayStateManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRealm"/> class.
        /// </summary>
        public DefaultRealm()
        {
            this.weatherStates = new List<IWeatherState>();

            this.Zones = new List<IZone>();
            this.CreationDate = DateTime.Now;
            this.Id = Guid.NewGuid();

            // By default we update the weather every 15 minutes in the game.
            this.WeatherUpdateFrequency = 15;
        }

        /// <summary>
        /// Occurs when the Realms weather has changed.
        /// </summary>
        public event EventHandler<WeatherStateChangedEventArgs> WeatherChanged;

        /// <summary>
        /// Gets or sets the offset from the World's current time for the Realm.
        /// </summary>
        /// <value>
        /// The time zone offset.
        /// </value>
        public TimeOfDay TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the time of day for the Realm.
        /// </summary>
        /// <value>
        /// The time of day.
        /// </value>
        public TimeOfDay CurrentTimeOfDay { get; set; }

        /// <summary>
        /// Gets or sets the current weather.
        /// </summary>
        /// <value>
        /// The current weather.
        /// </value>
        public IWeatherState CurrentWeather { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used to determine the current weather.
        /// </summary>
        /// <value>
        /// The weather states.
        /// </value>
        public IEnumerable<IWeatherState> WeatherStates
        {
            get
            {
                return this.weatherStates;
            }

            set
            {
                this.weatherStates.Clear();

                if (value != null)
                {
                    this.weatherStates.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the weather update frequency.
        /// When the frequency is hit, the new weather will be determined based on the weathers probability. It is not guaranteed to change.
        /// This value is represented as in-game minutes
        /// </summary>
        /// <value>
        /// The weather update frequency.
        /// </value>
        public int WeatherUpdateFrequency { get; set; }

        /// <summary>
        /// Gets or sets the zones within this Realm.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        public IEnumerable<IZone> Zones { get; set; }

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
        public double TimeFromCreation
        {
            get
            {
                return this.CreationDate.Subtract(DateTime.Now).TotalSeconds;
            }
        }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Initializes the realm. Weather states are set up and time zone offset validation.
        /// </summary>
        /// <param name="world">The world this realm belongs to.</param>
        /// <param name="worldTimeOfDay">The world time of day. The Realm offset is applied on-top of this time by the realm itself.</param>
        /// <exception cref="System.NullReferenceException">
        /// A valid world instance must be supplied.
        /// or
        /// A valid TImeOfDay instance is required to initialize a realm.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">You can not have a negative time-zone for realms. They must all be forward offsets from the world's current time.</exception>
        public virtual void Initialize(IWorld world, TimeOfDay worldTimeOfDay)
        {
            if (world == null)
            {
                throw new NullReferenceException("A valid world instance must be supplied.");
            }

            if (worldTimeOfDay == null)
            {
                throw new NullReferenceException("A valid TImeOfDay instance is required to initialize a realm.");
            }

            this.world = world;
            this.timeOfDayStateManager = new TimeOfDayStateManager(world.TimeOfDayStates);

            this.ApplyTimeZoneOffset(worldTimeOfDay);

            if (this.weatherStates.Count > 0)
            {
                // Set up our weather clock and start performing weather changes.
                var weatherClock = new EngineTimer<IWeatherState>((state, clock) => this.SetupWeather(), this.CurrentWeather);

                // Convert the minutes specified with WeatherUpdateFrequency to in-game minutes using the GameTimeRatio.
                weatherClock.Start(0, TimeSpan.FromMinutes(this.WeatherUpdateFrequency * this.world.GameTimeAdjustmentFactor).TotalMilliseconds);
            }
        }

        public void ApplyTimeZoneOffset(TimeOfDay timeOfDay)
        {
            if (this.TimeZoneOffset == null)
            {
                this.TimeZoneOffset = new TimeOfDay { Hour = 0, Minute = 0, HoursPerDay = this.world.HoursPerDay };
            }
            else if (this.TimeZoneOffset.Hour < 0 || this.TimeZoneOffset.Minute < 0)
            {
                throw new ArgumentOutOfRangeException("You can not have a negative time-zone for realms. They must all be forward offsets from the world's current time.");
            }

            if (this.CurrentTimeOfDay == null)
            {
                this.CurrentTimeOfDay = new TimeOfDay();
            }

            this.CurrentTimeOfDay.Hour = timeOfDay.Hour;
            this.CurrentTimeOfDay.Minute = timeOfDay.Minute;
            this.CurrentTimeOfDay.HoursPerDay = timeOfDay.HoursPerDay;

            this.CurrentTimeOfDay.DecrementByHour(this.TimeZoneOffset.Hour);
            this.CurrentTimeOfDay.DecrementByMinute(this.TimeZoneOffset.Minute);
        }

        public ITimeOfDayState GetCurrentTimeOfDayState()
        {
            ITimeOfDayState state = this.timeOfDayStateManager.GetTimeOfDayState(this.CurrentTimeOfDay);

            if (state == null)
            {
                return this.world.CurrentTimeOfDay;
            }
            else
            {
                return state;
            }
        }

        /// <summary>
        /// Setups the weather up.
        /// </summary>
        private void SetupWeather()
        {
            // Set the current weather based on the probability of it changing.
            IWeatherState nextWeatherState = this.weatherStates.AnyOrDefaultFromWeight(weather => weather.OccurrenceProbability);
            if (nextWeatherState != this.CurrentWeather)
            {
                this.CurrentWeather = nextWeatherState;
                this.OnWeatherChanged(null, this.CurrentWeather);
            }
        }

        protected virtual void OnWeatherChanged(IWeatherState oldWeather, IWeatherState newWeather)
        {
            EventHandler<WeatherStateChangedEventArgs> handler = this.WeatherChanged;
            if (handler == null)
            {
                return;
            }

            handler(this, new WeatherStateChangedEventArgs(oldWeather, newWeather));
        }
    }
}

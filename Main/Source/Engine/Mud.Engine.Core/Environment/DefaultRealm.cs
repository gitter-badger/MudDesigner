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
        /// Gets or sets the offset from the World's current time for the Realm in hours.
        /// </summary>
        /// <value>
        /// The time zone offset.
        /// </value>
        public double TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the time of day for the Realm.
        /// </summary>
        /// <value>
        /// The time of day.
        /// </value>
        public ITimeOfDayState TimeOfDay { get; set; }

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
        public double TimeFromCreation { get { return this.CreationDate.Subtract(DateTime.Now).TotalSeconds; } }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        public virtual void Initialize(IWorld world)
        {
            this.world = world;
            if (this.weatherStates.Count > 0)
            {
                // Set up our weather clock and start performing weather changes.
                var weatherClock = new EngineTimer<IWeatherState>((state, clock) => this.SetupWeather(), this.CurrentWeather);

                // Convert the minutes specified with WeatherUpdateFrequency to in-game minutes using the GameTimeRatio.
                weatherClock.Start(0, TimeSpan.FromMinutes(this.WeatherUpdateFrequency * this.world.GameTimeAdjustmentFactor).TotalMilliseconds);
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

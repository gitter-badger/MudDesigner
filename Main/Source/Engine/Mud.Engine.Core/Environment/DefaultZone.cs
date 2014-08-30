using Mud.Engine.Core.Engine;
using Mud.Engine.Core.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class DefaultZone: IZone
    {
        /// <summary>
        /// The realm that owns this zone.
        /// </summary>
        private IRealm realm;

        /// <summary>
        /// The weather states backing field.
        /// </summary>
        private List<IWeatherState> weatherStates = new List<IWeatherState>();

        /// <summary>
        /// The rooms that this zone is responsible for managing.
        /// </summary>
        private List<IRoom> rooms = new List<IRoom>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultZone"/> class.
        /// </summary>
        public DefaultZone()
        {
            // By default we update the weather every 15 minutes in the game.
            this.WeatherUpdateFrequency = 15;
        }

        /// <summary>
        /// Occurs when the Realms weather has changed.
        /// </summary>
        public event EventHandler<WeatherStateChangedEventArgs> WeatherChanged;

        public event EventHandler<ICharacter> EnteredZone;

        public event EventHandler<ICharacter> LeftZone;

        public int NumberOfRooms
        {
            get
            {
                return this.rooms.Count;
            }
        }

        public ICollection<IZoneRule> Rules { get; set; }

        /// <summary>
        /// Gets or sets the current weather.
        /// </summary>
        public IWeatherState CurrentWeather { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used to determine the current weather.
        /// </summary>
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
        public int WeatherUpdateFrequency { get; set; }

        /// <summary>
        /// Gets or sets the rooms within this Zone.
        /// </summary>
        public IEnumerable<IRoom> Rooms
        {
            get
            {
                return this.rooms;
            }

            set
            {
                this.rooms.Clear();
                if (value != null)
                {
                    this.rooms.AddRange(value);
                }
            }
        }

        public IRealm Realm { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        public double TimeFromCreation { get; private set; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Initializes the zone with the supplied realm.
        /// </summary>
        /// <param name="realm">The realm.</param>
        public virtual void Initialize(IRealm realm)
        {
            this.realm = realm;

            if (this.weatherStates.Count > 0)
            {
                // Set up our weather clock and start performing weather changes.
                var weatherClock = new EngineTimer<IWeatherState>((state, clock) => this.SetupWeather(), this.CurrentWeather);

                // Convert the minutes specified with WeatherUpdateFrequency to in-game minutes using the GameTimeRatio.
                weatherClock.Start(0, TimeSpan.FromMinutes(this.WeatherUpdateFrequency * this.realm.World.GameTimeAdjustmentFactor).TotalMilliseconds);
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

        /// <summary>
        /// Called when the zones weather has changed.
        /// </summary>
        /// <param name="oldWeather">The old weather prior to the change taking place.</param>
        /// <param name="newWeather">The new weather once the change is completed.</param>
        protected virtual void OnWeatherChanged(IWeatherState oldWeather, IWeatherState newWeather)
        {
            EventHandler<WeatherStateChangedEventArgs> handler = this.WeatherChanged;
            if (handler == null)
            {
                return;
            }

            handler(this, new WeatherStateChangedEventArgs(oldWeather, newWeather));
        }

        protected virtual void OnEnteredZone(ICharacter character)
        {
            EventHandler<ICharacter> handler = this.EnteredZone;
            if (handler == null)
            {
                return;
            }

            handler(this, character);
        }

        protected virtual void OnLeftZone(ICharacter character)
        {
            EventHandler<ICharacter> handler = this.LeftZone;
            if (handler == null)
            {
                return;
            }

            handler(this, character);
        }
    }
}

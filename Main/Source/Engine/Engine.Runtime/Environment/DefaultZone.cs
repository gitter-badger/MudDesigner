//-----------------------------------------------------------------------
// <copyright file="DefaultZone.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Runtime.Environment
{
    using System;
    using System.Collections.Generic;
    using Mud.Engine.Runtime.Character;
    using Mud.Engine.Runtime.Core;
    using Mud.Engine.Shared.Core;
    using Mud.Engine.Runtime.Environment;
    using Mud.Engine.Shared.Environment;
    using Mud.Engine.Shared.Environment;
    using Mud.Engine.Shared.Character;

    /// <summary>
    /// The default IZone Type for the engine.
    /// </summary>
    public class DefaultZone : IZone
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

        /// <summary>
        /// Occurs when a character enters a zone.
        /// </summary>
        public event EventHandler<ICharacter> EnteredZone;

        /// <summary>
        /// Occurs when a character left a zone.
        /// </summary>
        public event EventHandler<ICharacter> LeftZone;

        /// <summary>
        /// Gets the number of rooms.
        /// </summary>
        public int NumberOfRooms
        {
            get
            {
                return this.rooms.Count;
            }
        }

        /// <summary>
        /// Gets or sets the rules that must be applied to this zone.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.CollectionRelatedPersistedObject)]
        public ICollection<IZoneRule> Rules { get; set; }

        /// <summary>
        /// Gets or sets the current weather.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.RelatedPersistedObject)]
        public IWeatherState CurrentWeather { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used to determine the current weather.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.CollectionRelatedPersistedObject)]
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
        [PersistValue]
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        [PersistValue]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the realm that owns this zone.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.RelatedPersistedObject)]
        public IRealm Realm { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [PersistValue]
        public string Name { get; set; }

        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        [PersistValue]
        public double TimeFromCreation { get; private set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.StringRepresentation)]
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
        /// Add and initializes the given room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <exception cref="System.NullReferenceException">Attempted to add a null Zone to the Realm.</exception>
        public void AddRoomToZone(IRoom room)
        {
            if (room == null)
            {
                throw new NullReferenceException("Attempted to add a null Zone to the Realm.");
            }

            room.Initialize(this);
            this.rooms.Add(room);

            room.EnteredRoom += this.RoomOccupancyChanged;
            room.LeftRoom += this.RoomOccupancyChanged;
        }

        /// <summary>
        /// Removes the room from zone.
        /// </summary>
        /// <param name="room">The room.</param>
        public void RemoveRoomFromZone(IRoom room)
        {
            if (!this.rooms.Contains(room))
            {
                return;
            }

            this.rooms.Remove(room);
            room.EnteredRoom -= this.RoomOccupancyChanged;
            room.LeftRoom -= this.RoomOccupancyChanged;
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

        /// <summary>
        /// Called when a character enters an IRoom within this Zone for the first time.
        /// </summary>
        /// <param name="character">The character.</param>
        protected virtual void OnEnteredZone(ICharacter character)
        {
            EventHandler<ICharacter> handler = this.EnteredZone;
            if (handler == null)
            {
                return;
            }

            handler(this, character);
        }

        /// <summary>
        /// Called when a character leaves an IRoom within this Zone and enters an IRoom within a different Zone.
        /// </summary>
        /// <param name="character">The character.</param>
        protected virtual void OnLeftZone(ICharacter character)
        {
            EventHandler<ICharacter> handler = this.LeftZone;
            if (handler == null)
            {
                return;
            }

            handler(this, character);
        }

        /// <summary>
        /// Handles when an occupant moves around within a zone.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OccupancyChangedEventArgs" /> instance containing the event data.</param>
        private void RoomOccupancyChanged(object sender, OccupancyChangedEventArgs e)
        {
            // If the departure room is not null, then the character is moving from a room.
            if (e.DepartureRoom != null)
            {
                // Check if the old room was within our zone and the new room is not.
                if (this.HasRoom(e.DepartureRoom) && !this.HasRoom(e.ArrivalRoom))
                {
                    this.OnLeftZone(e.Occupant);
                }
                else if (!this.HasRoom(e.DepartureRoom) && this.HasRoom(e.ArrivalRoom))
                {
                    // We have left one zone and entered this Zone.
                    this.OnEnteredZone(e.Occupant);
                }

                // If none of the aboe criteria was met, it indicates that the occupant is moving
                // around within the zone, which so we don't need to push events.
                return;
            }

            // If the departure room is null, then we know that they are entering this zone for the first time.
            if (this.HasRoom(e.ArrivalRoom))
            {
                this.OnEnteredZone(e.Occupant);
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
    }
}

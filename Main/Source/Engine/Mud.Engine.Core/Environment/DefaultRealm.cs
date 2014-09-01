//-----------------------------------------------------------------------
// <copyright file="DefaultRealm.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System;
    using System.Collections.Generic;
    using Mud.Engine.Core.Environment.Time;

    /// <summary>
    /// The Default Realm class for the engine.
    /// </summary>
    public class DefaultRealm : IRealm
    {
        /// <summary>
        /// The time of day state manager
        /// </summary>
        private TimeOfDayStateManager timeOfDayStateManager;

        /// <summary>
        /// The collection of zones for this realm
        /// </summary>
        private List<IZone> zones = new List<IZone>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRealm"/> class.
        /// </summary>
        public DefaultRealm()
        {
            this.CreationDate = DateTime.Now;
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the offset from the World's current time for the Realm.
        /// </summary>
        public TimeOfDay TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the time of day for the Realm.
        /// </summary>
        public TimeOfDay CurrentTimeOfDay { get; set; }

        /// <summary>
        /// Gets or sets the zones within this Realm.
        /// </summary>
        public IEnumerable<IZone> Zones
        {
            get
            {
                return this.zones;
            }

            set
            {
                this.zones.Clear();

                if (value != null)
                {
                    this.zones.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Gets the number of zones.
        /// </summary>
        public int NumberOfZones
        {
            get
            {
                // This lets users not call the enumerator on the Zones property.
                return this.zones.Count;
            }
        }

        /// <summary>
        /// Gets or sets the World that owns this realm..
        /// </summary>
        public IWorld World { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        public double TimeFromCreation
        {
            get
            {
                return this.CreationDate.Subtract(DateTime.Now).TotalSeconds;
            }
        }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Initializes the realm. Weather states are set up and time zone offsets are applied.
        /// </summary>
        /// <param name="world">The world this realm belongs to.</param>
        /// <param name="worldTimeOfDay">The world time of day.</param>
        /// <exception cref="System.NullReferenceException">
        /// A valid world instance must be supplied.
        /// or
        /// A valid TimeOfDay instance is required to initialize a realm.
        /// </exception>
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

            this.World = world;
            this.timeOfDayStateManager = new TimeOfDayStateManager(world.TimeOfDayStates);

            this.ApplyTimeZoneOffset(worldTimeOfDay);
        }

        /// <summary>
        /// Adds the given zone to this instance.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <exception cref="System.NullReferenceException">Attempted to add a null Zone to the Realm.
        /// or
        /// Adding a Zone to a Realm with a null Rooms collection is not allowed.</exception>
        public void AddZoneToRealm(IZone zone)
        {
            if (zone == null)
            {
                throw new NullReferenceException("Attempted to add a null Zone to the Realm.");
            }

            if (zone.Rooms == null)
            {
                throw new NullReferenceException("Adding a Zone to a Realm with a null Rooms collection is not allowed.");
            }

            zone.Initialize(this);
            this.zones.Add(zone);
        }

        /// <summary>
        /// Updates the time for this realm, applying the realm's time zone offset to the given time.
        /// </summary>
        /// <param name="timeOfDay">The time of day.</param>
        /// <exception cref="System.NullReferenceException">
        /// ApplyTimeZoneOffset can not be given a null argument.
        /// or
        /// A Time Zone offset can not be applied when both the TimeZoneOffset and World properties are null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">You can not have a negative time-zone for realms. They must all be forward offsets from the world's current time.</exception>
        public void ApplyTimeZoneOffset(TimeOfDay timeOfDay)
        {
            if (timeOfDay == null)
            {
                throw new NullReferenceException("ApplyTimeZoneOffset can not be given a null argument.");
            }

            if (this.TimeZoneOffset == null)
            {
                if (this.World == null)
                {
                    throw new NullReferenceException("A Time Zone offset can not be applied when both the TimeZoneOffset and World properties are null.");
                }

                this.TimeZoneOffset = new TimeOfDay { Hour = 0, Minute = 0, HoursPerDay = this.World.HoursPerDay };
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

        /// <summary>
        /// Gets the state of the current time of day.
        /// </summary>
        /// <returns>Returns an instance representing the current time of day state.</returns>
        public ITimeOfDayState GetCurrentTimeOfDayState()
        {
            ITimeOfDayState state = this.timeOfDayStateManager.GetTimeOfDayState(this.CurrentTimeOfDay);

            if (state == null)
            {
                return this.World.CurrentTimeOfDay;
            }
            else
            {
                return state;
            }
        }
    }
}

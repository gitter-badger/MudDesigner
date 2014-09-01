//-----------------------------------------------------------------------
// <copyright file="IZone.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using Mud.Engine.Core.Character;
    using Mud.Engine.Core.Environment.Weather;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a contract for creating Zone objects.
    /// </summary>
    public interface IZone : IEnvironment
    {
        /// <summary>
        /// Occurs when the Realms weather has changed.
        /// </summary>
        event EventHandler<WeatherStateChangedEventArgs> WeatherChanged;

        /// <summary>
        /// Occurs when a mob enters a zone.
        /// </summary>
        event EventHandler<ICharacter> EnteredZone;

        /// <summary>
        /// Occurs when a mob left a zone.
        /// </summary>
        event EventHandler<ICharacter> LeftZone;

        /// <summary>
        /// Gets or sets the current weather.
        /// </summary>
        /// <value>
        /// The current weather.
        /// </value>
        IWeatherState CurrentWeather { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used to determine the current weather.
        /// </summary>
        /// <value>
        /// The weather states.
        /// </value>
        IEnumerable<IWeatherState> WeatherStates { get; set; }

        /// <summary>
        /// Gets or sets the weather update frequency.
        /// When the frequency is hit, the new weather will be determined based on the weathers probability. It is not guaranteed to change.
        /// </summary>
        /// <value>
        /// The weather update frequency.
        /// </value>
        int WeatherUpdateFrequency { get; set; }

        /// <summary>
        /// Gets or sets the rooms within this Zone.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        IEnumerable<IRoom> Rooms { get; set; }

        /// <summary>
        /// Gets the number of rooms.
        /// </summary>
        int NumberOfRooms { get; }

        /// <summary>
        /// Gets the realm that owns this zone.
        /// </summary>
        /// <value>
        /// The realm.
        /// </value>
        IRealm Realm { get; }

        /// <summary>
        /// Gets or sets the rules.
        /// </summary>
        ICollection<IZoneRule> Rules { get; set; }

        /// <summary>
        /// Initializes the zone with the supplied realm.
        /// </summary>
        /// <param name="realm">The realm.</param>
        void Initialize(IRealm realm);

        /// <summary>
        /// Add and initializes the given room.
        /// </summary>
        /// <param name="room">The room.</param>
        void AddRoomToZone(IRoom room);

        /// <summary>
        /// Removes the room from zone.
        /// </summary>
        /// <param name="room">The room.</param>
        void RemoveRoomFromZone(IRoom room);
    }
}

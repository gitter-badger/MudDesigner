//-----------------------------------------------------------------------
// <copyright file="IRealm.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using Mud.Engine.Core.Engine;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IRealm can be implemented to provide Realm support to an object.
    /// </summary>
    public interface IRealm : IEnvironment
    {
        /// <summary>
        /// Occurs when the Realms weather has changed.
        /// </summary>
        event EventHandler<WeatherStateChangedEventArgs> WeatherChanged;

        /// <summary>
        /// Gets or sets the offset from the World's current time for the Realm
        /// </summary>
        /// <value>
        /// The time zone offset.
        /// </value>
        TimeOfDay TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the time of day for the Realm.
        /// </summary>
        /// <value>
        /// The time of day.
        /// </value>
        TimeOfDay CurrentTimeOfDay { get; set; }

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
        /// Gets or sets the zones within this Realm.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        IEnumerable<IZone> Zones { get; set; }

        /// <summary>
        /// Initializes the realm. Weather states are set up and time zone offset validation.
        /// </summary>
        /// <param name="world">The world this realm belongs to.</param>
        /// <param name="worldTimeOfDay">The world time of day.</param>
        void Initialize(IWorld world, TimeOfDay worldTimeOfDay);

        /// <summary>
        /// Updates the time.
        /// </summary>
        /// <param name="timeOfDay">The time of day.</param>
        void ApplyTimeZoneOffset(TimeOfDay timeOfDay);

        ITimeOfDayState GetCurrentTimeOfDayState();
    }
}

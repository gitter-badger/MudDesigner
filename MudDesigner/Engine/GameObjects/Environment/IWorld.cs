//-----------------------------------------------------------------------
// <copyright file="IWorld.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.GameObjects.Environment
{
    /// <summary>
    /// Provides a contract for objects that want to implement a World
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Occurs after the World is loaded.
        /// </summary>
        event EventHandler<WorldEventArgs> Loaded;

        /// <summary>
        /// Occurs after the weather changes.
        /// </summary>
        event EventHandler<WorldEventArgs> WeatherChanged;

        /// <summary>
        /// Occurs after the day state has changed.
        /// </summary>
        event EventHandler<WorldEventArgs> DayStateChanged;

        /// <summary>
        /// Gets the number of hours since original creation that this world has been alive.
        /// </summary>
        int HoursAlive { get; }

        /// <summary>
        /// Gets or sets the in-game hours per in-game day.
        /// </summary>
        int HoursPerDay { get; set; }

        /// <summary>
        /// Gets or sets the ratio that hours are compared to real-world hours. If a ratio is set to 4, then 1 in-game hour is equal to 4 real-world hours.
        /// </summary>
        float HourRatio { get; set; }

        /// <summary>
        /// Gets or sets the state of the current weather.
        /// </summary>
        WeatherState CurrentWeatherState { get; set; }

        /// <summary>
        /// Gets or sets a collection of weather states that can be used at any time.
        /// </summary>
        List<WeatherState> WeatherStates { get; set; }

        /// <summary>
        /// Gets or sets the state of the current day.
        /// </summary>
        DayState CurrentDayState { get; set; }

        /// <summary>
        /// Gets or sets a collection of day states will be used to provide feedback on the current state of the day.
        /// </summary>
        List<DayState> DayStates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is safe.
        /// </summary>
        bool IsSafe { get; set; }

        /// <summary>
        /// Gets the occupants of the world.
        /// </summary>
        List<IMob> Occupants { get; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Updates this instance of the World.
        /// </summary>
        void Update();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    /// <summary>
    /// Defines properties that are used across various environments such as worlds, realms and zones.
    /// </summary>
    public interface IEnvironment
    {
        string Name { get; set; }

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
        /// Gets or sets the current time of day.
        /// </summary>
        /// <value>
        /// The current time of day.
        /// </value>
        ITimeOfDayState CurrentTimeOfDay { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used for the time of day.
        /// </summary>
        /// <value>
        /// The time of day states.
        /// </value>
        IEnumerable<ITimeOfDayState> TimeOfDayStates { get; set; }
    }
}

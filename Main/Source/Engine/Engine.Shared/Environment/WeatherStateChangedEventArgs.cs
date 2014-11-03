//-----------------------------------------------------------------------
// <copyright file="WeatherStateChangedEventArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Environment
{
    using System;

    /// <summary>
    /// Event Arguments for when the weather state has changed.
    /// </summary>
    public class WeatherStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherStateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="previousState">State of the previous.</param>
        /// <param name="newState">The new state.</param>
        public WeatherStateChangedEventArgs(IWeatherState previousState, IWeatherState newState)
        {
            this.PreviousState = previousState;
            this.CurrentState = newState;
        }

        /// <summary>
        /// Gets the previous weather state.
        /// </summary>
        public IWeatherState PreviousState { get; private set; }

        /// <summary>
        /// Gets the weather state being transitioned to.
        /// </summary>
        public IWeatherState CurrentState { get; private set; }
    }
}

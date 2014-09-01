//-----------------------------------------------------------------------
// <copyright file="IWeatherState.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment.Weather
{
    /// <summary>
    /// Defines a state that weather can be in.
    /// </summary>
    public interface IWeatherState
    {
        /// <summary>
        /// Gets the occurrence probability of this weather state occurring in an environment.
        /// The higher the probability relative to other weather states, the more likely it is going to occur.
        /// </summary>
        double OccurrenceProbability { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}

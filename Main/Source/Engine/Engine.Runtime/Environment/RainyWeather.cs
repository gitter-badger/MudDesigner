//-----------------------------------------------------------------------
// <copyright file="RainyWeather.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Shared.Environment;
namespace Mud.Engine.Runtime.Environment
{
    /// <summary>
    /// Represents rainy weather.
    /// </summary>
    public class RainyWeather : IWeatherState
    {
        /// <summary>
        /// Gets the occurrence probability of this weather state occurring in an environment.
        /// The higher the probability relative to other weather states, the more likely it is going to occur.
        /// </summary>
        public double OccurrenceProbability
        {
            get
            {
                return 30;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return "Rainy";
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ClearWeather.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Shared.Environment;
namespace Mud.Engine.Runtime.Environment
{
    /// <summary>
    /// Represents clear skies, with no weather effects.
    /// </summary>
    public class ClearWeather : IWeatherState
    {
        /// <summary>
        /// Gets the occurrence probability of this weather state occurring in an environment.
        /// The higher the probability relative to other weather states, the more likely it is going to occur.
        /// </summary>
        public double OccurrenceProbability 
        { 
            get 
            {
                return 80; 
            } 
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name 
        { 
            get 
            { 
                return "Clear"; 
            } 
        }
    }
}

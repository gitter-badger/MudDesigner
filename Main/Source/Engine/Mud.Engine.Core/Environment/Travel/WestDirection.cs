//-----------------------------------------------------------------------
// <copyright file="WestDirection.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment.Travel
{
    /// <summary>
    /// Represents traveling East.
    /// </summary>
    public class WestDirection : ITravelDirection
    {
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public string Direction
        {
            get 
            {
                return "West";
            }
        }

        /// <summary>
        /// Gets the opposite direction.
        /// </summary>
        /// <returns>Returns the direction required to travel in order to go in the opposite direction of this instance.</returns>
        public ITravelDirection GetOppositeDirection()
        {
            return new EastDirection();
        }
    }
}

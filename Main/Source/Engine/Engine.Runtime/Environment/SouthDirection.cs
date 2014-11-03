//-----------------------------------------------------------------------
// <copyright file="SouthDirection.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Shared.Environment;
namespace Mud.Engine.Runtime.Environment
{
    /// <summary>
    /// Represents traveling East.
    /// </summary>
    public class SouthDirection : ITravelDirection
    {
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public string Direction 
        {
            get
            {
                return "South";
            }
        }

        /// <summary>
        /// Gets the opposite direction.
        /// </summary>
        /// <returns>Returns the direction required to travel in order to go in the opposite direction of this instance.</returns>
        public ITravelDirection GetOppositeDirection()
        {
            return new NorthDirection();
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ITravelDirection.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Environment
{
    /// <summary>
    /// Allows an instance to be represented as a travel direction.
    /// </summary>
    public interface ITravelDirection
    {
        /// <summary>
        /// Gets the direction.
        /// </summary>
        string Direction { get; }

        /// <summary>
        /// Gets the opposite direction.
        /// </summary>
        /// <returns>Returns the direction required to travel in order to go in the opposite direction of this instance.</returns>
        ITravelDirection GetOppositeDirection();
    }
}

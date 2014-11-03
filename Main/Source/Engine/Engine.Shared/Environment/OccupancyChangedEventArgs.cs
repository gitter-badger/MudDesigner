//-----------------------------------------------------------------------
// <copyright file="OccupancyChangedEventArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Environment
{
    using System;
    using Mud.Engine.Shared.Character;

    /// <summary>
    /// Event arguments for when a rooms occupancy status changes.
    /// </summary>
    public class OccupancyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OccupancyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="occupant">The occupant.</param>
        /// <param name="departureRoom">The departure room.</param>
        /// <param name="arrivalRoom">The arrival room.</param>
        public OccupancyChangedEventArgs(ICharacter occupant, IRoom departureRoom, IRoom arrivalRoom)
        {
            this.Occupant = occupant;
            this.DepartureRoom = departureRoom;
            this.ArrivalRoom = arrivalRoom;
        }

        /// <summary>
        /// Gets the occupant that triggered this event.
        /// </summary>
        public ICharacter Occupant { get; private set; }

        /// <summary>
        /// Gets the departure room.
        /// </summary>
        public IRoom DepartureRoom { get; private set; }

        /// <summary>
        /// Gets the arrival room.
        /// </summary>
        public IRoom ArrivalRoom { get; private set; }
    }
}

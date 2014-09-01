using Mud.Engine.Core.Character;
//-----------------------------------------------------------------------
// <copyright file="IRoom.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
namespace Mud.Engine.Core.Environment
{
    /// <summary>
    /// The IRoom interface provides methods for interacting with Rooms
    /// </summary>
    public interface IRoom : IEnvironment
    {
        event EventHandler<OccupancyChangedEventArgs> EnteredRoom;

        event EventHandler<OccupancyChangedEventArgs> LeftRoom;

        IZone Zone { get; }

        ICollection<ICharacter> Occupants { get; }

        ICollection<IDoorway> Doorways { get; }

        void Initialize(IZone zone);

        /// <summary>
        /// Adds the occupant.
        /// </summary>
        /// <param name="character">The character.</param>
        void AddOccupantToRoom(ICharacter character);

        /// <summary>
        /// Removes the occupant from room.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="arrivalRoom">The arrival room.</param>
        void RemoveOccupantFromRoom(ICharacter character, IRoom arrivalRoom);
    }
}

//-----------------------------------------------------------------------
// <copyright file="IRoom.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System;
    using System.Collections.Generic;
    using Mud.Engine.Core.Character;

    /// <summary>
    /// The IRoom interface provides methods for interacting with Rooms
    /// </summary>
    public interface IRoom : IEnvironment
    {
        /// <summary>
        /// Occurs when a character enters the room.
        /// </summary>
        event EventHandler<OccupancyChangedEventArgs> EnteredRoom;

        /// <summary>
        /// Occurs when a character leaves the room.
        /// </summary>
        event EventHandler<OccupancyChangedEventArgs> LeftRoom;

        /// <summary>
        /// Gets the zone that owns this Room.
        /// </summary>
        IZone Zone { get; }

        /// <summary>
        /// Gets the occupants within this Room..
        /// </summary>
        ICollection<ICharacter> Occupants { get; }

        /// <summary>
        /// Gets the doorways that this Room has, linked to other IRooms.
        /// </summary>
        ICollection<IDoorway> Doorways { get; }

        /// <summary>
        /// Initializes the room with the given zone.
        /// </summary>
        /// <param name="zone">The zone that represents the owner of this room.</param>
        void Initialize(IZone zone);

        /// <summary>
        /// Adds the occupant to this instance.
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

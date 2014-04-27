//-----------------------------------------------------------------------
// <copyright file="IDoor.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The Doorway class links Rooms together, allowing player to traverse the game world.
    /// </summary>
    public interface IDoor : IGameObject
    {
        /// <summary>
        /// Gets if the doorway is locked, requiring a key to access.
        /// </summary>
        bool Locked { get; }

        /// <summary>
        /// Gets a reference to the key that will unlock a locked doorway
        /// </summary>
        IItem Key { get; }

        /// <summary>
        /// Gets the direction that the player must walk in order to travel through this doorway.
        /// </summary>
        AvailableTravelDirections FacingDirection { get; }

        /// <summary>
        /// Gets a reference to the Room that the player will arrive within when they travel through the doorway.
        /// </summary>
        IRoom Arrival { get; }

        /// <summary>
        /// Gets a reference to the ROom that the player will leave when they travel through the doorway.
        /// </summary>
        IRoom Departure { get; }

        /// <summary>
        /// Locks the doorway with the specified key, preventing access without the key.
        /// </summary>
        /// <param name="key">The key that is required to walk through the doorway.</param>
        void Lock(IItem key);

        /// <summary>
        /// Unlocks the doorway with the supplied key, allowing access for the character whom has the key
        /// </summary>
        /// <param name="key">The key that is required to walk through the doorway</param>
        /// <returns></returns>
        bool Unlock(IItem key);

        /// <summary>
        /// Sets the direction the player must walk in order to travel through this doorway
        /// </summary>
        /// <param name="directions"></param>
        void SetFacingDirection(AvailableTravelDirections direction);

        /// <summary>
        /// Sets the room that the player will enter when they walk through the doorway.
        /// </summary>
        /// <param name="room"></param>
        void SetArrivalRoom(IRoom room);

        /// <summary>
        /// Sets the room that the player will leave when they walk through the doorway.
        /// </summary>
        /// <param name="room"></param>
        void SetDepartingRoom(IRoom room);
    }
}
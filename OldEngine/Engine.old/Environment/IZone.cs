//-----------------------------------------------------------------------
// <copyright file="IZone.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Mobs;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The interface contract for Zone objects.
    /// </summary>
    public interface IZone : IEnvironment
    {
        /// <summary>
        /// Gets or Sets the Realm that this Zone belongs to
        /// </summary>
        [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        IRealm Realm { get; set; }

        /// <summary>
        /// Gets or Sets the Room collection that belongs to this Zone.
        /// </summary>
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        List<IRoom> Rooms { get; set; }

        // TODO - Add a general collection of monsters that populate the entire Zone.
        // Helps you not having to insert Monsters into every room you make
        // List<IMonster> Monsters {get;}

        /// <summary>
        /// Adds a Room to the Zone. This is the preferred method for adding Rooms. It ensures that a null Room is never added to the collection
        /// as well as provides the ability to overwrite a Room if it already exists.
        /// </summary>
        /// <param name="room">The Room that you want to add to the Realm</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Room if it already exists within the collection</param>
        void AddRoom(IRoom room, bool forceOverwrite = false);

        /// <summary>
        /// Adds a collection of Rooms to the Zone, with the option to overwrite any Rooms that already exists.
        /// </summary>
        /// <param name="rooms">The array of Rooms you want to add.</param>
        /// <param name="forceOverwrite">If true it will overwrite the Room if it already exists within the Zone collection.</param>
        void AddRooms(IRoom[] rooms, bool forceOverwrite = false);

        /// <summary>
        /// Gets the specified Room and returns a reference to it for use.
        /// </summary>
        /// <param name="roomName">The name of the Room you want to get a reference for.</param>
        /// <returns></returns>
        IRoom GetRoom(string roomName);

        /// <summary>
        /// Gets the rooms.
        /// </summary>
        /// <returns></returns>
        IRoom[] GetRooms();

        /// <summary>
        /// Removes the specified Room from the Zone collection of Rooms.
        /// </summary>
        /// <param name="room">The Room you want to remove.</param>
        void RemoveRoom(IRoom room);

        /// <summary>
        /// Deletes all of the Rooms from the Zone.
        /// </summary>
        void DeleteRooms();
    }
}

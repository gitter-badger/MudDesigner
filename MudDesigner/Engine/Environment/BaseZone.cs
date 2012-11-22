/* BaseZone
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The Base class for all Zone classes.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

//Newtonsoft JSon using statement
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The base class for all Zone classes.
    /// </summary>
    public abstract class BaseZone : GameObject, IZone
    {
        /// <summary>
        /// Gets or Sets the Realm that this Zone belongs to
        /// </summary>
        [Browsable(false), JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public IRealm Realm { get; set; }

        /// <summary>
        /// Gets or Sets the Room collection that belongs to this Zone.
        /// </summary>
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public List<IRoom> Rooms { get; set; }

        /// <summary>
        /// Gets or Sets if this Zone is accessible by admins only
        /// </summary>
        public bool IsAdminOnly { get; set; }

        /// <summary>
        /// Gets or Sets if combat can take place in this Zone
        /// </summary>
        public bool IsSafe { get; set; }

        public BaseZone()
        {
            Rooms = new List<IRoom>();

            Enabled = true;
        }

        public BaseZone(string name)
        {
            Rooms = new List<IRoom>();
            Name = name;

            Enabled = true;
        }

        public BaseZone(string name, IRealm realm)
        {
            Rooms = new List<IRoom>();

            if (realm != null)
            {
                Realm = realm;
            }

            Name = name;

            Enabled = true;
        }

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref IGameObject copyFrom, bool ignoreNonNullProperties = false)
        {
            base.CopyState(ref copyFrom);

            //Make sure we are dealing with a IRealm object
            if (copyFrom is IZone && Rooms != null)
            {
                //Loop through each Zone and update it's Realm property to reference the newObject instead.
                foreach (IRoom room in Rooms)
                {
                    room.Zone = this;
                }

            }
        }

        /// <summary>
        /// Adds a Room to the Zone. This is the preferred method for adding Rooms. It ensures that a null Room is never added to the collection
        /// as well as provides the ability to overwrite a Room if it already exists.
        /// </summary>
        /// <param name="room">The Room that you want to add to the Realm</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Room if it already exists within the collection</param>
        public virtual void AddRoom(IRoom room, bool forceOverwrite = true)
        {
            if (room == null)
                return;

            if (forceOverwrite)
            {
                if (Rooms.Contains(room))
                {
                    Rooms.Remove(room);
                }
            }

            room.Zone = this;
            Rooms.Add(room);
        }

        /// <summary>
        /// Adds a collection of Rooms to the Zone, with the option to overwrite any Rooms that already exists.
        /// </summary>
        /// <param name="rooms">The array of Rooms you want to add.</param>
        /// <param name="forceOverwrite">If true it will overwrite the Room if it already exists within the Zone collection.</param>
        public virtual void AddRooms(IRoom[] rooms, bool forceOverwrite = true)
        {
            //Loop through each Room provided and add it via our AddRoom() method.
            foreach (IRoom room in rooms)
            {
                AddRoom(room, forceOverwrite);
            }
        }

        /// <summary>
        /// Gets the specified Room and returns a reference to it for use.
        /// </summary>
        /// <param name="roomName">The name of the Room you want to get a reference for.</param>
        /// <returns></returns>
        public virtual IRoom GetRoom(string roomName)
        {
            //Loop through each Room until we find one that matches.
            foreach (IRoom room in Rooms)
            {
                //If it matches, return it
                if (room.Name == roomName)
                    return room;
            }

            return null;
        }

        public virtual IRoom[] GetRooms()
        {
            if (Rooms.Count == 0)
                return null;

            return Rooms.ToArray();
        }

        /// <summary>
        /// Removes the specified Room from the Zone collection of Rooms.
        /// </summary>
        /// <param name="room">The Room you want to remove.</param>
        public virtual void RemoveRoom(IRoom room)
        {
            if (Rooms.Contains(room))
                Rooms.Remove(room);
        }

        /// <summary>
        /// Deletes all of the Rooms from the Zone.
        /// </summary>
        public virtual void DeleteRooms()
        {
            //Loop through each Room in the collection.
            foreach (IRoom room in Rooms)
            {
                //Destroy it.
                room.Destroy();
            }
        }

        /// <summary>
        /// Broadcasts a message to all of the players within the Zone, including all Rooms.
        /// </summary>
        /// <param name="message">The message you want to broadcast</param>
        /// <param name="playersToOmmit">A list of players that you want to hide the message from.</param>
        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            //Loop through each Room in the Zone
            foreach (IRoom room in Rooms)
            {
                //Broadcast to the Room
                if (playersToOmmit == null)
                    room.BroadcastMessage(message);
                else
                    room.BroadcastMessage(message, playersToOmmit);
            }
        }

        public override string ToString()
        {
            return Realm.Name + ">" + Name;
        }

        public delegate void OnEnterHandler(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction);
        public event OnEnterHandler OnEnterEvent;
        public void OnEnter(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction)
        {
        }

        public delegate void OnLeavehandler(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction);
        public event OnLeavehandler OnLeaveEvent;
        public void OnLeave(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction)
        {
        }
    }
}

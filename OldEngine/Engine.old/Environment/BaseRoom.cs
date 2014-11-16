//-----------------------------------------------------------------------
// <copyright file="BaseRoom.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Mobs;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The Base class for all Room classes.
    /// </summary>
    public abstract class BaseRoom : GameObject, IRoom
    {
        /// <summary>
        /// Zone that this Room resides within
        /// </summary>
        [Browsable(false)]
        public IZone Zone { get; set; }

        /// <summary>
        /// Determins if this Room is a safe room where no attacks can be made.
        /// </summary>
        public bool IsSafe { get; set; }

        /// <summary>
        /// Gets or Sets if this Room is only accessible by an admin or not.
        /// </summary>
        public bool IsAdminOnly { get; set; }

        /// <summary>
        /// Gets or Sets information pertaining to a users 5-senses for the Room.
        /// </summary>
        [Browsable(false)]
        public Senses Sense { get; set; }

        /// <summary>
        /// Gets or Sets the players that currently occupy this Room.
        /// </summary>
        [Browsable(false)]
        [JsonIgnore()] 
        public List<IMob> Occupants { get; set; }

        /// <summary>
        /// Gets or Sets the collection of Doorways that this Room has, leading to other Rooms.
        /// </summary>
        [Browsable(false)]
        public Dictionary<AvailableTravelDirections, IDoor> Doorways { get; set; }

        /// <summary>
        /// Gets a reference to the Items collection within the Room.
        /// </summary>
        public List<IItem> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRoom"/> class.
        /// </summary>
        public BaseRoom() : base()
        {
            Doorways = new Dictionary<AvailableTravelDirections, IDoor>();
            Occupants = new List<IMob>();
            Items = new List<IItem>();

            OnEnterEvent += new OnEnterHandler(OnEnter);
            OnLeaveEvent += new OnLeaveHandler(OnLeave);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRoom"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="zone">The zone.</param>
        public BaseRoom(string name, IZone zone) : this()
        {
            Zone = zone;
            Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRoom"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="zone">The zone.</param>
        /// <param name="safe">if set to <c>true</c> [safe].</param>
        public BaseRoom(string name, IZone zone, bool safe = false) : this()
        {
            IsSafe = safe;
            Zone = zone;
            Name = name;
        }

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref IGameObject copyFrom, bool ignoreNonNullProperties = false)
        {
            base.CopyState(ref copyFrom);

            if (copyFrom is IRoom && Doorways != null)
            {
                foreach (IDoor door in Doorways.Values)
                {
                    PropertyInfo prop = door.GetType().GetProperty("Departure");
                    prop.SetValue(door, this, null);
                }
            }
        }

        /// <summary>
        /// Destroys the Room and 
        /// </summary>
        public override void Destroy()
        {
            Doorways.Clear();
            Doorways = null;

            List<IPlayer> playerCollection = new List<IPlayer>();

            foreach (IMob mob in Occupants)
            {
                if (mob is IPlayer)
                    playerCollection.Add((IPlayer)mob);
            }
            this.BroadcastMessage("Room is being destroyed!  You will be teleported to a new location.",
                playerCollection.ToList());

            // Trace back up through the environment path to get the World
            IWorld world = Zone.Realm.World;
            //Get the initial Room location, and split it up into an array so we can parse it
            string[] roomPath = EngineSettings.Default.InitialRoom.Split('>');

            // Make sure we have three entries, Realm, Zone and Room
            if (roomPath.Length != 3)
                return;

            // Get the Realm
            IRealm realm = world.GetRealm(roomPath[0]);
            if (realm == null)
                return;

            // Get our Zone
            IZone zone = realm.GetZone(roomPath[1]);
            if (zone == null)
                return;

            // Get the initial Room
            IRoom room = zone.GetRoom(roomPath[2]);
            if (room == null)
                return;

            // Loop through each player in this Room and move them to the initial Room.
            foreach (IPlayer player in Occupants)
            {
                player.Move(room);
                player.SendMessage("You have been moved to " + room.Name);
            }
        }

        /// <summary>
        /// Adds the character.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="entryDirection">The entry direction.</param>
        public virtual void AddCharacter(IMob character, AvailableTravelDirections entryDirection)
        {
            if (Occupants.Contains(character))
                return;
            else
            {
                Occupants.Add(character);
                OnEnterEvent(character, entryDirection);
            }
        }

        /// <summary>
        /// Removes the character.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="leavingDirection">The leaving direction.</param>
        public virtual void RemoveCharacter(IMob character, AvailableTravelDirections leavingDirection)
        {
            if (Occupants.Contains(character))
            {
                Occupants.Remove(character);

                OnLeaveEvent(character, leavingDirection);
            }
        }

        /// <summary>
        /// Adds a doorway to the Room, linking it to another Room in the world.
        /// </summary>
        /// <param name="direction">The direction that the player must travel in order to move through the doorway</param>
        /// <param name="arrivalRoom">The room that the player will enter, once they walk through the doorway</param>
        /// <param name="autoAddReverseDirection">If true, the arrival room will have the opposite doorway automatically linked back to this Room</param>
        /// <param name="forceOverwrite">If true, if a doorway already exists for the specified direction, it will overwrite it.</param>
        public virtual void AddDoorway(AvailableTravelDirections direction, IRoom arrivalRoom, bool autoAddReverseDirection = true, bool forceOverwrite = true)
        {
            // Check if room is null.
            if (arrivalRoom == null)
                return; // No null references within our collections!

            // If this direction already exists, overwrite it
            // but only if 'forceOverwrite' is true
            if (Doorways.ContainsKey(direction))
            {
                // Remove the old door
                RemoveDoorway(direction);
                // Get a scripted Door instance to add back to the collection
                var door = (Door)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.DoorScript);
                door.SetArrivalRoom(arrivalRoom);
                door.SetDepartingRoom(this);
                door.SetFacingDirection(direction);

                Doorways.Add(direction, door);
            }
            // Direction does not exist, so lets add a new doorway
            else
            {
                // Get a scripted instance of a Door.
                var door = (Door)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.DoorScript);
                door.SetFacingDirection(direction);
                door.SetArrivalRoom(arrivalRoom);
                door.SetDepartingRoom(this);


                // Add the new doorway to this rooms collection.
                Doorways.Add(direction, door);

                // If autoreverse is enabled, add the doorway to the arrival room too.
                if (autoAddReverseDirection)
                {
                    arrivalRoom.AddDoorway(TravelDirections.GetReverseDirection(direction), this, false, forceOverwrite);
                }
            }
        }

        /// <summary>
        /// Gets a reference to the doorway that matches the specified direction, if it exists
        /// </summary>
        /// <param name="direction">The direction that you want the doorway for.</param>
        /// <returns></returns>
        public IDoor GetDoorway(AvailableTravelDirections direction)
        {
            // Check if the doorway collection has the specified direction
            if (Doorways.ContainsKey(direction))
                return Doorways[direction]; // return it
            else
                return null;
        }

        /// <summary>
        /// Returns an array of all doorways within the Room.
        /// </summary>
        /// <returns></returns>
        public IDoor[] GetDoorways()
        {
            List<IDoor> doorways = new List<IDoor>();
            foreach (IDoor door in Doorways.Values)
            {
                doorways.Add(door);
            }

            return doorways.ToArray();
        }

        /// <summary>
        /// Checks if the Room has a door in the direction requested.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool DoorwayExists(AvailableTravelDirections direction)
        {
            foreach (IDoor door in Doorways.Values)
            {
                if (door.FacingDirection == direction)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Allows for removal of a doorway.
        /// </summary>
        /// <param name="direction">The direction of travel that the doorway should be removed.</param>
        /// <param name="autoRemoveReverseDirection">If true, the room on the otherside of the doorway will have it's door removed as well.</param>
        public virtual void RemoveDoorway(AvailableTravelDirections direction, bool autoRemoveReverseDirection = true)
        {
            if (Doorways.ContainsKey(direction))
            {
                if (autoRemoveReverseDirection)
                {
                    //When removig the reverse direction, always set "autoRemoveReverseDirection" within the Arrival room
                    //to false, otherwise a circular loop will start.
                    Doorways[direction].Arrival.RemoveDoorway(TravelDirections.GetReverseDirection(direction), false);
                }
                Doorways.Remove(direction);
            }
        }

        /// <summary>
        /// Adds a game item to the Room.
        /// </summary>
        /// <param name="item">The item you want to add.</param>
        public virtual void AddItem(IItem item)
        {
            //Don't allow duplicate entries.
            if (Items.Contains(item))
                Items.Remove(item);

            //Insert new item.
            Items.Add(item);
        }

        /// <summary>
        /// Removes a game item from the Room.
        /// </summary>
        /// <param name="item">The item you want to remove</param>
        public virtual void RemoveItem(IItem item)
        {
            if (Items.Contains(item))
                Items.Remove(item);
        }

        /// <summary>
        /// Clears the Room of all items.
        /// </summary>
        public virtual void ClearItems()
        {
            foreach (IItem item in Items)
            {
                item.Destroy();
            }

            Items.Clear();
        }

        /// <summary>
        /// Broadcasts a message to all of the players within the Room.
        /// </summary>
        /// <param name="message">The message you want to send.</param>
        /// <param name="playersToOmmit">A list of players you want to hide the message from.</param>
        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            foreach (IPlayer player in Occupants)
            {
                if (playersToOmmit != null)
                {
                    if (playersToOmmit.Contains(player))
                        continue; // Skip this player if it's in the list.
                    else
                        // Send the message
                        player.SendMessage(message);
                }
                else
                {
                    // Send the message
                    player.SendMessage(message);
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Zone.Realm.Name + ">" + Zone.Name + ">" + Name;
        }

        /// <summary>
        /// Adds the decoration.
        /// </summary>
        /// <param name="decoration">The decoration.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void AddDecoration(string decoration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IItem[] GetItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IItem GetItem(string itemName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the decorations.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string[] GetDecorations()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the decoration.
        /// </summary>
        /// <param name="decoration">The decoration.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RemoveDecoration(string decoration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the decorations.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ClearDecorations()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="enteredDirection">The entered direction.</param>
        public delegate void OnEnterHandler(IMob character, AvailableTravelDirections enteredDirection);

        /// <summary>
        /// Occurs when [on enter event].
        /// </summary>
        public event OnEnterHandler OnEnterEvent;

        /// <summary>
        /// Called when [enter].
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="enteredDirection">The entered direction.</param>
        public virtual void OnEnter(IMob character, AvailableTravelDirections enteredDirection)
        {
            List<IPlayer> ommitList = new List<IPlayer>();
            ommitList.Add((IPlayer)character);

            if (enteredDirection == AvailableTravelDirections.None)
                BroadcastMessage(character.Name + " has arrived.", ommitList);
            else
                BroadcastMessage(character.Name + " has entered from the " + enteredDirection.ToString(), ommitList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="leavingDirection">The leaving direction.</param>
        public delegate void OnLeaveHandler(IMob character, AvailableTravelDirections leavingDirection);

        /// <summary>
        /// Occurs when [on leave event].
        /// </summary>
        public event OnLeaveHandler OnLeaveEvent;

        /// <summary>
        /// Called when [leave].
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="direction">The direction.</param>
        public void OnLeave(IMob character, AvailableTravelDirections direction)
        {
            List<IPlayer> ommitList = new List<IPlayer>();
            ommitList.Add((IPlayer)character);
            if (direction != AvailableTravelDirections.None)
                BroadcastMessage(character.Name + " has left going " + direction.ToString(), ommitList);
            else
                BroadcastMessage(character.Name + " has left.", ommitList);
        }
    }
}

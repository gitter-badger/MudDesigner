/* Door
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The Doorway class links Rooms together, allowing players to traverse the game world.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements.
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The Doorway class links Rooms together, allowing player to traverse the game world.
    /// </summary>
    public abstract class Door : GameObject,  IDoor
    {
        /// <summary>
        /// Gets if the doorway is locked, requiring a key to access.
        /// </summary>
        public bool Locked { get; protected set; }

        /// <summary>
        /// Gets a reference to the key that will unlock a locked doorway
        /// </summary>
        public IItem Key { get; protected set; }

        /// <summary>
        /// Gets the direction that the player must walk in order to travel through this doorway.
        /// </summary>
        public AvailableTravelDirections FacingDirection { get;  protected set; }

        /// <summary>
        /// Gets a reference to the Room that the player will arrive within when they travel through the doorway.
        /// </summary>
        public IRoom Arrival { get; protected set; }

        /// <summary>
        /// Gets a reference to the ROom that the player will leave when they travel through the doorway.
        /// </summary>
        public IRoom Departure { get; protected  set; }

        public Door()
        {

        }

        public Door(AvailableTravelDirections direction, IRoom departingRoom, IRoom arrivingRoom)
        {
            FacingDirection = direction;
            Arrival = arrivingRoom;
            Departure = departingRoom;
        }

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref dynamic copyTo)
        {
            if (copyTo is Door)
            {
                Scripting.ScriptObject newObject = new Scripting.ScriptObject(copyTo);

                newObject.SetProperty("Locked", Locked, null);
                newObject.SetProperty("Key", Key, null);
                newObject.SetProperty("FacingDirection", FacingDirection, null);
                newObject.SetProperty("Arrival", Arrival, null);
                newObject.SetProperty("Departure", Departure, null);
            }

            base.CopyState(ref copyTo);
        }

        /// <summary>
        /// Locks the doorway with the specified key, preventing access without the key.
        /// </summary>
        /// <param name="key">The key that is required to walk through the doorway.</param>
        public virtual void Lock(IItem key)
        {
            Key = key;
            Locked = true;
        }

        /// <summary>
        /// Unlocks the doorway with the supplied key, allowing access for the character whom has the key
        /// </summary>
        /// <param name="key">The key that is required to walk through the doorway</param>
        /// <returns></returns>
        public virtual bool Unlock(IItem key)
        {
            if (key == Key)
                Locked = false;

            return Locked;
        }

        /// <summary>
        /// Sets the direction the player must walk in order to travel through this doorway
        /// </summary>
        /// <param name="directions"></param>
        public void SetFacingDirection(AvailableTravelDirections directions)
        {
            FacingDirection = directions;
        }

        /// <summary>
        /// Sets the room that the player will enter when they walk through the doorway.
        /// </summary>
        /// <param name="room"></param>
        public void SetArrivalRoom(IRoom room)
        {
            Arrival = room;
        }

        /// <summary>
        /// Sets the room that the player will leave when they walk through the doorway.
        /// </summary>
        /// <param name="room"></param>
        public void SetDepartingRoom(IRoom room)
        {
            Departure = room;
        }
    }

}

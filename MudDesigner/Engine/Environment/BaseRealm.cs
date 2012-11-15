/* BaseRealm
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The Base class for all Realm classes.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// The Base class for all Realm classes.
    /// </summary>
    public abstract class BaseRealm : GameObject, IRealm
    {
        /// <summary>
        /// Gets or Sets the collection of Zones that belong to this Realm.
        /// </summary>
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public List<IZone> Zones { get; set; }

        /// <summary>
        /// Gets or Sets a reference to the game World
        /// </summary>
        public IWorld World { get; set; }

        /// <summary>
        /// Gets or Sets if this Realm is only accessible by an admin or not.
        /// </summary>
        public bool IsAdminOnly { get; set; }

        public BaseRealm()
        {
            Zones = new List<IZone>();
        }

        public BaseRealm(string name)
            : base()
        {
            Zones = new List<IZone>();
            Name = name;
        }

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref dynamic copyTo)
        {
            //Make sure we are dealing with a IRealm object
            if (copyTo is IRealm)
            {
                ScriptObject newObject = new ScriptObject(copyTo);

                newObject.SetProperty("IsAdminOnly", IsAdminOnly, null);

                //Make sure the object has a Zones properties
                if (newObject.GetProperty("Zones") != null)
                {
                    //Set the newObjects Zones to reference our current Zone collection.
                    copyTo.Zones = Zones;

                    //Loop through each Zone and update it's Realm property to reference the newObject instead.
                    foreach (IZone zone in copyTo.Zones)
                    {
                        zone.Realm = this;
                    }
                }
                
            }
            base.CopyState(ref copyTo);
        }

        /// <summary>
        /// Adds a Zone to the Realm. This is the preferred method for adding Zones. It ensures that a null Zone is never added to the collection
        /// as well as provides the ability to overwrite a Zone if it already exists.
        /// </summary>
        /// <param name="zone">The Zone that you want to add to the Realm</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Zone if it already exists within the collection</param>
        public virtual void AddZone(IZone zone, bool forceOverwrite = true)
        {
            if (zone == null)
                return;

            if (forceOverwrite)
            {
                if (Zones.Contains(zone))
                {
                    Zones.Remove(zone);
                }
            }

            zone.Realm = this;
            Zones.Add(zone);
        }

        /// <summary>
        /// Adds a collection of Zones to the Realm, with the option to overwrite any Zones that already exists.
        /// </summary>
        /// <param name="zones">THe array of Zones you want to add</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Zone if it already exists within the collection.</param>
        public virtual void AddZones(IZone[] zones, bool forceOverwrite = true)
        {
            //Loop through each Zone provided and add it via our AddZone() method.
            foreach (IZone zone in zones)
            {
                AddZone(zone, forceOverwrite);
            }
        }

        /// <summary>
        /// Gets the specified Zone and returns a reference to it for use.
        /// </summary>
        /// <param name="zoneName">The name of the Zone you want to get a reference for.</param>
        /// <returns></returns>
        public virtual IZone GetZone(string zoneName)
        {
            //Loop through each Zone until we find one that matches.
            foreach (IZone zone in Zones)
            {
                //If it matches, return it.
                if (zone.Name == zoneName)
                    return zone;
            }
            return null;
        }

        public virtual IZone[] GetZones()
        {
            if (Zones.Count == 0)
                return null;

            return Zones.ToArray();
        }

        /// <summary>
        /// Removes the specified Zone from the Realms collection of Zones.
        /// </summary>
        /// <param name="zone">The Zone you want to remove.</param>
        public virtual void RemoveZone(IZone zone)
        {
            if (Zones.Contains(zone))
                Zones.Remove(zone);
        }

        /// <summary>
        /// Broadcasts a message to all of the players within the Realm, including all Zones and Rooms.
        /// </summary>
        /// <param name="message">The message you want to broadcast.</param>
        /// <param name="playersToOmmit">A list of players that you want to hide the message from.</param>
        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            //Loop through each zone in the Realm.
            foreach (IZone zone in Zones)
            {
                //Broadcast to the Zone.
                if (playersToOmmit == null)
                    zone.BroadcastMessage(message);
                else
                    zone.BroadcastMessage(message, playersToOmmit);
            }
        }

        public delegate void OnEnterHandler(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections directions);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction)
        {
        }

        public delegate void OnLeaveHandler(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections directions);
        public event OnLeaveHandler OnLeaveEvent;
        public void OnLeave(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction)
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

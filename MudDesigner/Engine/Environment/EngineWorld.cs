/* EngineWorld
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The EngineWorld class contains the game world. All Realms, Zones and Rooms are held within the World..
 */
//Microsoft .NET using statements
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;

//Newtonsoft JSon using statements
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The EngineWorld class contains the game world.  All Realms, Zones and Rooms are held within the World.
    /// </summary>
    public class EngineWorld : GameObject, IWorld
    {
        /// <summary>
        /// Gets or Sets a collection of Realms that belong to the game world
        /// </summary>
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public List<IRealm> Realms { get; set; }

        /// <summary>
        /// Gets or Sets if combat can take place in the world
        /// </summary>
        public bool IsSafe { get; set; }

        public EngineWorld()
        {
            Realms = new List<IRealm>();
            Name = "World";
        }

        /// <summary>
        /// Adds the supplied Realm to the game world.
        /// </summary>
        /// <param name="realm">The Realm you want to add to the world</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Realm if it already exists within the World</param>
        public void AddRealm(IRealm realm, bool forceOverwrite = false)
        {
            if (realm == null)
                return;

            if (forceOverwrite)
            {
                if (Realms.Contains(realm))
                {
                    Realms.Remove(realm);
                }
            }

            realm.World = this;
            Realms.Add(realm);
        }

        /// <summary>
        /// Retrieves a realm based on the realm name.
        /// Returns null if the realm doesn't exist. 
        /// </summary>
        /// <param name="realm">The name of the Realm you want to get</param>
        /// <returns></returns>
        public IRealm GetRealm(string realm)
        {
            foreach (IRealm r in Realms)
            {
                if (r.Name.ToLower() == realm.ToLower())
                    return r;
            }

            return null;
        }

        /// <summary>
        /// Gets an array referencing all of the games Realms.
        /// </summary>
        /// <returns></returns>
        public IRealm[] GetRealms()
        {
            return Realms.ToArray();
        }

        /// <summary>
        /// Removes the specified Realm from the game world
        /// </summary>
        /// <param name="realm"></param>
        public void RemoveRealm(IRealm realm)
        {
            if (Realms.Contains(realm))
                Realms.Remove(realm);
        }

        /// <summary>
        /// Removes the specified Realm from the game world.
        /// </summary>
        /// <param name="realmName"></param>
        public void RemoveRealm(string realmName)
        {
            foreach (IRealm realm in Realms)
            {
                if (realm.Name.ToLower() == realmName.ToLower())
                {
                    Realms.Remove(realm);
                    break;
                }
            }
        }

        /// <summary>
        /// Broadcasts the specified message to all players in the game world
        /// </summary>
        /// <param name="message">The message you want to send</param>
        /// <param name="playersToOmit">A list of players you want to hide the message from.</param>
        public void BroadcastMessage(string message, List<Mobs.IPlayer> playersToOmit = null)
        {
            foreach (IRealm realm in Realms)
            {
                if (playersToOmit == null)
                    realm.BroadcastMessage(message);
                else
                    realm.BroadcastMessage(message, playersToOmit);
            }
        }
    }
}

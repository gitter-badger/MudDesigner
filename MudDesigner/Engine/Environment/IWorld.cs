/* IWorld
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The EngineWorld interface contains the methods and properties needed to implement the game world. All Realms, Zones and Rooms are held within the World.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Mobs;

//Newtonsoft JSon using statements
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The EngineWorld interface contains the methods and properties needed to implement the game world. All Realms, Zones and Rooms are held within the World
    /// </summary>
    public interface IWorld : IGameObject
    {
        /// <summary>
        /// Gets or Sets if combat can take place in the world
        /// </summary>
        bool IsSafe { get; set; }

        /// <summary>
        /// Gets or Sets a collection of Realms that belong to the game world
        /// </summary>
        List<IRealm> Realms { get; set; }

        /// <summary>
        /// Adds the supplied Realm to the game world.
        /// </summary>
        /// <param name="realm">The Realm you want to add to the world</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Realm if it already exists within the World</param>
        void AddRealm(IRealm realm, bool overwrite = false);

        /// <summary>
        /// Retrieves a realm based on the realm name.
        /// Returns null if the realm doesn't exist. 
        /// </summary>
        /// <param name="realm">The name of the Realm you want to get</param>
        /// <returns></returns>
        IRealm GetRealm(string realmname);

        /// <summary>
        /// Gets an array referencing all of the games Realms.
        /// </summary>
        /// <returns></returns>
        IRealm[] GetRealms();

        /// <summary>
        /// Removes the specified Realm from the game world
        /// </summary>
        /// <param name="realm"></param>
        void RemoveRealm(IRealm realm);

        /// <summary>
        /// Removes the specified Realm from the game world.
        /// </summary>
        /// <param name="realmName"></param>
        void RemoveRealm(string realmName);

        /// <summary>
        /// Broadcasts the specified message to all players in the game world
        /// </summary>
        /// <param name="message">The message you want to send</param>
        /// <param name="playersToOmit">A list of players you want to hide the message from.</param>
        void BroadcastMessage(string message, List<IPlayer> playersToOmit = null);
    }
}

/* IEnvironment
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Used to implement members that are shared across all environments.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// Used to implement members that are shared across all environments
    /// </summary>
    public interface IEnvironment : IGameObject
    {
        /// <summary>
        /// Gets or Sets if this Realm is only accessible by an admin or not.
        /// </summary>
        bool IsAdminOnly { get; set; }

        /// <summary>
        /// Broadcasts a message to all of the players within the Realm, including all Zones and Rooms.
        /// </summary>
        /// <param name="message">The message you want to broadcast.</param>
        /// <param name="playersToOmmit">A list of players that you want to hide the message from.</param>
        void BroadcastMessage(string message, List<IPlayer> playersToOmit = null);

        void OnEnter(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction);
        void OnLeave(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction);
    }
}

//-----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}

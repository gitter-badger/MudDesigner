//-----------------------------------------------------------------------
// <copyright file="IGame.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using MudEngine.Engine.GameObjects.Environment;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Provides a contract for Game objects.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Gets a value indicating whether this instance is multiplayer.
        /// </summary>
        public bool IsMultiplayer { get; }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets the storage source.
        /// </summary>
        IPersistedStorage StorageSource { get; }

        /// <summary>
        /// Gets or Sets the name of the game being played.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or Sets the current version of the game.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Gets or Sets the website that users can visit to get information on the game.
        /// </summary>
        string Website { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide room names].
        /// </summary>
        bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable automatic save].
        /// </summary>
        bool EnableAutoSave { get; set; }

        /// <summary>
        /// Gets or sets the automatic save frequency.
        /// </summary>
        int AutoSaveFrequency { get; set; }

        /// <summary>
        /// Gets or sets the last saved.
        /// </summary>
        DateTime LastSaved { get; set; }

        /// <summary>
        /// Gets or Sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        List<IWorld> Worlds { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        IProgress<IMessage> Logger { get; set; }

        /// <summary>
        /// Initializes the specified storage source and server.
        /// </summary>
        /// <param name="storageSource">The storage source.</param>
        void Initialize<T>(IPersistedStorage storageSource) where T : class, IPlayer, new();

        /// <summary>
        /// Broadcasts the specified message to the user.
        /// </summary>
        /// <param name="target">The target the message is intended for.</param>
        /// <param name="message">The message.</param>
        void BroadcastToPlayer(IMob target, IMessage message);

        /// <summary>
        /// Formats the message for broadcasting.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        string FormatMessageForBroadcasting(IMessage message);
    }
}

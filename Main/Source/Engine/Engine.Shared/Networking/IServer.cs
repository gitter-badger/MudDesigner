//-----------------------------------------------------------------------
// <copyright file="IServer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Networking
{
    using System;
    using System.Collections.Generic;
    using Mud.Engine.Shared.Character;
using Mud.Engine.Shared.Commanding;
    using Mud.Engine.Shared.Core;

    /// <summary>
    /// Provides a contract for objects wanting to implement a server.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Occurs when a player connects to the server.
        /// </summary>
        event EventHandler<ServerConnectionEventArgs> PlayerConnected;

        /// <summary>
        /// Occurs when a player disconnects from the server.
        /// </summary>
        event EventHandler<ServerConnectionEventArgs> PlayerDisconnected;

        IPlayerConnectCommand ConnectionCommand { get; set; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        IGame Game { get; }

        /// <summary>
        /// Gets a collection of current user connections.
        /// </summary>
        ICollection<IPlayer> ConnectedPlayers { get; }

        /// <summary>
        /// Gets or sets the port that the server is running on.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets the maximum connections.
        /// </summary>
        int MaxConnections { get; set; }

        /// <summary>
        /// Gets or sets the maximum queued connections.
        /// </summary>
        int MaxQueuedConnections { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of the password.
        /// </summary>
        int MinimumPasswordSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the password.
        /// </summary>
        int MaximumPasswordSize { get; set; }

        /// <summary>
        /// Gets or sets the message of the day.
        /// </summary>
        ICollection<string> MessageOfTheDay { get; set; }

        /// <summary>
        /// Gets or sets the server owner.
        /// </summary>
        string Owner { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IServer"/> is enabled.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Gets the current server status.
        /// </summary>
        ServerStatus Status { get; }

        /// <summary>
        /// Starts the server using the specified game.
        /// </summary>
        /// <typeparam name="TPlayer">The type of the player.</typeparam>
        /// <param name="game">The game.</param>
        void Start<TPlayer>(IGame game)
            where TPlayer : class, IPlayer, new();

        /// <summary>
        /// Stops the server.
        /// </summary>
        void Stop();

        /// <summary>
        /// Disconnects the specified IServerPlayer object.
        /// </summary>
        /// <param name="player">The player.</param>
        void Disconnect(IPlayer player);

        /// <summary>
        /// Disconnects everyone from the server..
        /// </summary>
        void DisconnectAll();
    }
}

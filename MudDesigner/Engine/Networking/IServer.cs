//-----------------------------------------------------------------------
// <copyright file="IServer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// Provides a contract for objects that want to implement a game server.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets a collection of current user connections.
        /// </summary>
        List<IServerObject> Connections { get; }

        /// <summary>
        /// Gets the port that the server is running on.
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
        List<string> MessageOfTheDay { get; set; }

        /// <summary>
        /// Gets or sets the server owner.
        /// </summary>
        /// <value>
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
        /// <param name="game">The game.</param>
        void Start<T>() where T : class, IServerObject, new();

        /// <summary>
        /// Stops server, shutting down the network connection.
        /// All IServerConnectionState objects will be disconnected.
        /// </summary>
        void Stop();

        /// <summary>
        /// Disconnects the specified IServerConnectionState object.
        /// </summary>
        /// <param name="connection">The connection.</param>
        void Disconnect(IServerObject connection);

        /// <summary>
        /// Disconnects everyone from the server..
        /// </summary>
        void DisconnectAll();
    }
}

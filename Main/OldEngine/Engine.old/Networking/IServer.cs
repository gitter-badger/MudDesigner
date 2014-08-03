//-----------------------------------------------------------------------
// <copyright file="IServer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Sockets;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Networking
{
    /// <summary>
    /// An enumerator with the various server status states.
    /// </summary>
    public enum ServerStatus
    {
        /// <summary>
        /// The stopped
        /// </summary>
        Stopped = 0,

        /// <summary>
        /// The starting
        /// </summary>
        Starting = 1,

        /// <summary>
        /// The running
        /// </summary>
        Running = 2
    }

    /// <summary>
    /// Provides an interface contract for creating a Server with all the required properties and methods.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets the port that the server is running on
        /// </summary>
        int Port { get; }
        
        /// <summary>
        /// Gets or Sets the maximum connections this server can have
        /// </summary>
        int MaxConnections { get; set; }

        /// <summary>
        /// Gets the maximum number of queued players this server can have
        /// </summary>
        int MaxQueuedConnections { get; }

        /// <summary>
        /// Gets or Sets the minimum password size for players creating a new account
        /// </summary>
        int MinimumPasswordSize { get; set; }

        /// <summary>
        /// Gets if the Server is enabled or not
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Gets a reference to the current status of the server.
        /// </summary>
        ServerStatus Status { get; }

        /// <summary>
        /// The Message Of The Day for the server. Printed to players upon connection.
        /// </summary>
        string MOTD { get; }

        /// <summary>
        /// Gets the owner of the server.
        /// </summary>
        string ServerOwner { get; }

        /// <summary>
        /// Gets a reference to the Game that the server belongs to.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        /// Starts the server. Once completed, it will listen for incoming connections
        /// </summary>
        /// <param name="maxConnections">Maximum connections this server will allow</param>
        /// <param name="maxQueueSize">Maximum queue size this server will allow</param>
        /// <param name="game">The game that the server will reference.</param>
        void Start(Int32 maxConnections, Int32 maxQueueSize, IGame game);

        /// <summary>
        /// Stops the server if it is running
        /// </summary>
        void Stop();

        /// <summary>
        /// Performs tasks related to the running of the server.
        /// </summary>
        void Running();
    }
}
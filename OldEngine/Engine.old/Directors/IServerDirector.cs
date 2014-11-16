//-----------------------------------------------------------------------
// <copyright file="IServerDirector.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.States;

namespace MudDesigner.Engine.Directors
{
    /// <summary>
    /// Server Director is responsible for managing the user connections.
    /// </summary>
    public interface IServerDirector
    {
        /// <summary>
        /// Gets a reference to the collection of players that are currently connected
        /// </summary>
        Dictionary<IPlayer, Thread> ConnectedPlayers { get; }

        /// <summary>
        /// The server that is running, allowing players to connect
        /// </summary>
        IServer Server { get; set; }

        /// <summary>
        /// The initial state a player is in when he initially connects
        /// </summary>
        IState InitialConnectionState { get; }

        /// <summary>
        /// Adds a connected player to the server
        /// </summary>
        /// <param name="connection">Connected player using a .NET Socket</param>
        void AddConnection(Socket connection);

        /// <summary>
        /// Receives data from the connected player. Should be run on its own thread.
        /// </summary>
        /// <param name="index"></param>
        void ReceiveDataThread(Object index);

        /// <summary>
        /// Disconnects all players from the server
        /// </summary>
        void DisconnectAll();


    }
}
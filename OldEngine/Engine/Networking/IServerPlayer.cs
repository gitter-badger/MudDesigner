//-----------------------------------------------------------------------
// <copyright file="IServerPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// Provides a contract to objects wanting to communicate with the server.
    /// </summary>
    public interface IServerPlayer
    {
        /// <summary>
        /// Occurs when the player is connected.
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// Occurs when the player is disconnected.
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        Socket Connection { get; set; }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        List<byte> Buffer { get; }

        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        int BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the received input.
        /// </summary>
        string ReceivedInput { get; set; }

        /// <summary>
        /// Gets the player.
        /// </summary>
        IPlayer Player { get; }

        /// <summary>
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="player">The player.</param>
        void Connect(Socket socket, IPlayer player);

        /// <summary>
        /// Receives the data.
        /// </summary>
        /// <param name="result">The result.</param>
        void ReceiveData(IAsyncResult result);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();
    }
}

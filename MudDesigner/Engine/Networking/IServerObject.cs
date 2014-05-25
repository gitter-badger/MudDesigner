//-----------------------------------------------------------------------
// <copyright file="IServerConnectionState.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// Provides a contract to objects wanting to communicate with the server.
    /// </summary>
    public interface IServerObject : IPlayer
    {
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
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        void Connect(Socket socket);

        void ReceiveData(IAsyncResult result);

        /// <summary>
        /// Called when the server establishes a connection between the server socket and the client socket.
        /// This is called automatically by the Servers Async socket object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Disconnect(IAsyncResult e);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();
    }
}

//-----------------------------------------------------------------------
// <copyright file="IServerConnectionState.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Sockets;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Provides a contract to objects wanting to communicate with the server.
    /// </summary>
    public interface IServerConnectionState
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        Socket Connection { get; set; }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        byte[] Buffer { get; }

        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        int BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the received input.
        /// </summary>
        string ReceivedInput { get; set; }

        /// <summary>
        /// Receives the data.
        /// </summary>
        /// <param name="result">The result.</param>
        void ReceiveData(IAsyncResult result);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="newLine">if set to <c>true</c> [new line].</param>
        void SendMessage(string message, bool newLine = true);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="data">The data.</param>
        void SendMessage(byte[] data);

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

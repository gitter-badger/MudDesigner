//-----------------------------------------------------------------------
// <copyright file="EnginePlayer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MudEngine.Engine.Mob
{
    /// <summary>
    /// The engine player. This class can be used in any game.
    /// </summary>
    public class EnginePlayer : EngineMob, IPlayer
    {
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public byte[] Buffer { get; protected set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public System.Net.Sockets.Socket Connection { get; set; }

        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the received input.
        /// </summary>
        public string ReceivedInput { get; set; }

        /// <summary>
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Connect(System.Net.Sockets.Socket socket)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the data.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ReceiveData(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="newLine">if set to <c>true</c> [new line].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SendMessage(string message, bool newLine = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SendMessage(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when the server establishes a connection between the server socket and the client socket.
        /// This is called automatically by the Servers Async socket object.
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Disconnect(IAsyncResult e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}

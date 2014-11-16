//-----------------------------------------------------------------------
// <copyright file="ServerPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.GameObjects.Mob.States;
using MudEngine.Engine.GameObjects.Mob.States.MultiplayerStates;

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// Exposes networking functionality to objects that need to interact with an implementation of IPlayer over the network.
    /// </summary>
    public class ServerPlayer : IServerPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerPlayer"/> class.
        /// </summary>
        public ServerPlayer()
        {
            this.Buffer = new List<byte>();
        }

        /// <summary>
        /// Occurs when the user connects to the server.
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Occurs when disconnected from the server.
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public System.Net.Sockets.Socket Connection { get; set; }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        public List<byte> Buffer { get; private set; }

        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the received input.
        /// </summary>
        public string ReceivedInput { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public IPlayer Player { get; protected set; }

        /// <summary>
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="player">The player that will needs to have networking functionality</param>
        public virtual void Connect(System.Net.Sockets.Socket socket, IPlayer player)
        {
            this.Connection = socket;
            this.Player = player;

            // Set the users initial state.
            this.Player.StateManager.SwitchState<ConnectState>();

            this.OnConnect();
        }

        /// <summary>
        /// Receives the data from the network.
        /// </summary>
        /// <param name="result">The result.</param>
        public virtual void ReceiveData(IAsyncResult result)
        {
            // The input s tring
            string input = string.Empty;
            this.ReceivedInput = string.Empty;

            // This loop will forever run until we have received \n from the player
            while (true && this.Connection != null && this.Connection.Connected)
            {
                try
                {
                    byte[] buf = new byte[1];

                    // Make sure we are still connected
                    if (!this.Connection.Connected)
                    {
                        this.Disconnect();
                    }

                    // Receive input from the socket connection
                    int recved = this.Connection.Receive(buf);

                    // If we have received data, prep it for use
                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && this.Buffer.Count > 0)
                        {
                            if (this.Buffer[this.Buffer.Count - 1] == '\r')
                            {
                                this.Buffer.RemoveAt(this.Buffer.Count - 1);
                            }

                            // Format the input
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                            // Convert the bytes into a s tring
                            input = enc.GetString(this.Buffer.ToArray());

                            // Clear out our buffer
                            this.Buffer.Clear();

                            // Return a trimmed string.
                            this.Player.ReceiveInput(new ReceivedInputMessage(input));
                        }
                        else
                        {
                            // otherwise keep adding the input to our bufer
                            this.Buffer.Add(buf[0]);
                        }
                    }
                    else if (recved == 0)
                    {
                        // Disconnected
                        this.Disconnect();
                    }
                }
                catch (Exception)
                {
                    this.Disconnect();
                }
            }
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            if (this.Connection != null && this.Connection.Connected)
            {
                this.Connection.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                this.OnDisconnect();
            }
        }

        /// <summary>
        /// Called when a user connects to the server.
        /// </summary>
        protected virtual void OnConnect()
        {
            EventHandler handler = this.Connected;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Called when the player is disconnected.
        /// </summary>
        /// <param name="result">The result.</param>
        protected virtual void OnDisconnect(IAsyncResult result = null)
        {
            EventHandler handler = this.Disconnected;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}

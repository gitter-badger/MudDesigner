using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerPlayer : IServerPlayer
    {
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
        /// Gets the player.
        /// </summary>
        public IPlayer Player { get; protected set; }

        /// <summary>
        /// Occurs when disconnected from the server.
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="player"></param>
        public virtual void Connect(System.Net.Sockets.Socket socket, IPlayer player)
        {
            this.Connection = socket;
            this.Player = player;
            this.Buffer = new List<byte>();
        }

        /// <summary>
        /// Receives the data from the network.
        /// </summary>
        /// <param name="result">The result.</param>
        public virtual void ReceiveData(IAsyncResult result)
        {
            // The input s tring
            string input = String.Empty;
            ReceivedInput = String.Empty;

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
                    Int32 recved = this.Connection.Receive(buf);

                    // If we have received data, prep it for use
                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && this.Buffer.Count > 0)
                        {
                            if (this.Buffer[Buffer.Count - 1] == '\r')
                                this.Buffer.RemoveAt(Buffer.Count - 1);

                            // Format the input
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                            // Convert the bytes into a s tring
                            input = enc.GetString(this.Buffer.ToArray());

                            // Clear out our buffer
                            this.Buffer.Clear();

                            // Return a trimmed string.
                            this.Player.ReceiveInput(new InputMessage(input));
                        }
                        else
                            // otherwise keep adding the input to our bufer
                            this.Buffer.Add(buf[0]);
                    }
                    else if (recved == 0) // Disconnected
                    {
                        this.Disconnect();
                    }
                }
                catch (Exception e)
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
            }

            this.OnDisconnect();
        }

        /// <summary>
        /// Called when the player is disconnected.
        /// </summary>
        /// <param name="result">The result.</param>
        protected virtual void OnDisconnect(IAsyncResult result = null)
        {
            EventHandler handler = Disconnected;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}

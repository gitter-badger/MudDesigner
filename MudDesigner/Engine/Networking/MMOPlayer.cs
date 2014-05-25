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
    public class MMOPlayer : DefaultPlayer, IServerObject
    {
        public System.Net.Sockets.Socket Connection { get; set; }

        public List<byte> Buffer { get; private set; }

        public int BufferSize { get; set; }

        public string ReceivedInput { get; set; }

        public override void Initialize(IGame game)
        {
            base.Initialize(game);

            this.Buffer = new List<byte>();
        }

        public void Connect(System.Net.Sockets.Socket socket)
        {
            this.Connection = socket;
        }

        public void ReceiveData(IAsyncResult result)
        {
            // The input s tring
            string input = String.Empty;
            ReceivedInput = String.Empty;

            // This loop will forever run until we have received \n from the player
            while (true && Connection != null)
            {
                try
                {
                    byte[] buf = new byte[1];

                    // Make sure we are still connected
                    if (!Connection.Connected)
                    {
                        // TODO: Throw a MudConnectionException
                        throw new Exception("disconnected.");
                    }

                    // Receive input from the socket connection
                    Int32 recved = Connection.Receive(buf);

                    // If we have received data, prep it for use
                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && this.Buffer.Count > 0)
                        {
                            if (Buffer[Buffer.Count - 1] == '\r')
                                Buffer.RemoveAt(Buffer.Count - 1);

                            // Format the input
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                            // Convert the bytes into a s tring
                            input = enc.GetString(Buffer.ToArray());

                            // Clear out our buffer
                            Buffer.Clear();

                            // Return a trimmed string.
                            this.ReceiveInput(new InputMessage(input));
                        }
                        else
                            // otherwise keep adding the input to our bufer
                            Buffer.Add(buf[0]);
                    }
                    else if (recved == 0) // Disconnected
                    {
                        // TODO: Throw a MudConnectionException
                        throw new Exception("disconnected.");
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Disconnect(IAsyncResult e)
        {
        }

        public void Disconnect()
        {
        }
    }
}

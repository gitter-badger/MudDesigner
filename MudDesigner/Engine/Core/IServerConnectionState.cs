using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    public interface IServerConnectionState
    {
        Socket Connection { get; set; }
        byte[] Buffer { get; }
        int BufferSize { get; set; }
        string ReceivedInput { get; set; }

        void ReceiveData(IAsyncResult result);
        void SendMessage(string message, bool newLine = true);
        void SendMessage(byte[] data);

        /// <summary>
        /// Called when the server establishes a connection between the server socket and the client socket.
        /// This is called automatically by the Servers Async socket object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Disconnect(IAsyncResult e);
        void Disconnect();
    }
}

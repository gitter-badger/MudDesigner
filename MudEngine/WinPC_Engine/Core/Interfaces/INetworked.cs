using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace MudEngine.Core.Interfaces
{
    /// <summary>
    /// Public API for scripts that take advantage of the engines Networking
    /// </summary>
    public interface INetworked
    {
        /// <summary>
        /// Method used for sending messages from the server to the object.
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(String message);

        /// <summary>
        /// Method for disconnecting the object from the server.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Method for connecting a object to the server.
        /// </summary>
        void Connect();
    }
}

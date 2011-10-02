using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MudEngine.Core
{
    public interface ICommunicate : IGameComponent
    {
        int Port { get; set; }
        ICommand LoginCommand { get; set; }

        void OnConnect(object client);
        void OnDisconnect(object client);
        void RecieveData(string data);
        void SendData(string data);
    }
}

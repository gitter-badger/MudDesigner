using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudEngine.Networking.Socket;

// TODO: everything D:

namespace MudEngine.Networking
{
    class ClientSocket : Socket
    {
        public ClientSocket()
        {
        }
        public ~ClientSocket()
        {
        }

        public string ip;
    }
}

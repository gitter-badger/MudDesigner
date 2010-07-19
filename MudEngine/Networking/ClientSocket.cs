using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// TODO: everything D:

namespace MudEngine.Networking
{
    class ClientSocket : Socket
    {
        public ClientSocket()
        {
        }
        
        ~ClientSocket()
        {
        }

        public string ip;
    }
}

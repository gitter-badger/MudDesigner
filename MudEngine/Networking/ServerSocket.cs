using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudEngine.Networking.Socket;

// TODO: everything D:

namespace MudEngine.Networking
{
    enum SocketType
    {
        TCP = 1,
        UDP = 2
    };
    class ServerSocket : Socket
    {
        public ServerSocket()
        {
            port = 0;
            stage = 0;
            type = 0;
        }
        public ~ServerSocket()
        {
            port = 0;
            stage = 0;
            type = 0;
        }
        // all methods return int, 1 = success, -1 = error, > 1 are just extra info w/ success
        //  < -1, known error find it out with: (string)getError(er_code);

        public int init(int p, SocketType st)
        {
            return 0;
        }
        public int start()
        {
            return 0;
        }
        public int bind()
        {
            return 0;
        }
        public int listen()
        {
            return 0;
        }
        public int accept()
        {
            return 0;
        }
        public int send()
        {
            return 0;
        }
        public int recv()
        {
            return 0;
        }
        public int end()
        {
            return 0;
        }
        public string getError(int er_code)
        {
            if (er_code > 0)
                return "No Error";
            switch (er_code)
            {

            default:
                return "Unknown Error";
            }
        }

        private int stage;
        private int port;
        private SocketType type;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

// TODO: Make ClientSocket a friend of ServerSocket so I can make sock and type private
//        other, havn't thought of what else I need

namespace MudEngine.Networking
{
    public class ClientSocket
    {
        public ClientSocket()
        {
            type = 0;
            used = false;
        }
        ~ClientSocket()
        {
            type = 0;
            used = false;
        }
        public int Send(byte[] ba)
        {
            try
            {
                sock.Send(ba);
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Receive(byte[] ba)
        {
            try
            {
                sock.Receive(ba);
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int CleanUp()
        {
            try
            {
                sock.Disconnect(true);
                sock.Close();
                sock.Dispose();
                type = 0;
                used = false;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }

        public ProtocolType type;
        public IPAddress ip;
        public Socket sock;
        public bool used;
    }
}

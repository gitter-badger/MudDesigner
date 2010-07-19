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
    class ClientSocket
    {
        public ClientSocket()
        {
            type = 0;
            ip = 0;
        }
        ~ClientSocket()
        {
            type = 0;
            ip = 0;
        }
        public int Send(byte[] ba,int size,SocketFlags sf)
        {
            try
            {
                sock.Send(ba, size, sf);
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Receive(ref byte[] ba, int size, SocketFlags sf)
        {
            try
            {
                sock.Receive(ba, size, sf);
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
                ip = 0;
                type = 0;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }

        public ProtocolType type;
        public long ip;
        public Socket sock;
    }
}

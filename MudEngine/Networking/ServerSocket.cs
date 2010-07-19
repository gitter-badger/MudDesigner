using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

// TODO: ln 167 warning, catch more errors and don't return all of em as unknown :o

namespace MudEngine.Networking
{
    class ServerSocket
    {
        public ServerSocket()
        {
            port = 0;
            stage = 0;
            type = 0;
        }
        
        ~ServerSocket()
        {
            port = 0;
            stage = 0;
            type = 0;
        }
        public int Initialize(int p,ProtocolType pt)
        {
            try
            {
                if (stage == 0)
                {
                    if (pt != ProtocolType.Tcp && pt != ProtocolType.Udp)
                        return -3;
                    port = p;
                    type = pt;
                    stage++;
                }
                else
                    return -2;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Start()
        {
            try
            {
                if (stage == 1)
                {
                    if (type == ProtocolType.Tcp)
                        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, type);
                    else
                        sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, type);
                    stage++;
                }
                else
                    return -2;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Bind()
        {
            try
            {
                if (stage == 2)
                {
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
                    sock.Bind(ipep);
                    stage++;
                }
                else
                    return -2;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Listen()
        {
            try
            {
                if (stage == 3 && type == ProtocolType.Udp)
                    return -4;
                if (stage == 3)
                {
                    sock.Listen(10);
                    stage++;
                }
                else
                    return -2;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Accept(ref ClientSocket cs)
        {
            try
            {
                if (type == ProtocolType.Udp)
                    return -2;
                if (stage == 4)
                    stage++;

                if (stage == 5)
                {
                    cs.sock = sock.Accept();
                    cs.type = ProtocolType.Tcp;
                }
                else
                    return -2;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int SendTo(byte[] ba, int size, SocketFlags sf, ref ClientSocket rcs)
        {
            try
            {
                if (type != ProtocolType.Udp)
                    return -3;

                if (stage == 3)
                    stage += 2;
                if (stage == 5)
                {
                    IPEndPoint ipep = new IPEndPoint(rcs.ip, port);
                    sock.SendTo(ba, size, sf, ipep);
                }
                else
                    return -2;
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int ReceiveFrom(ref byte[] ba, int size, SocketFlags sf, ref ClientSocket rcs)
        {
            try
            {
                if (type != ProtocolType.Udp)
                    return -3;

                if (stage == 3)
                    stage += 2;
                if (stage == 5)
                {
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint ep = (EndPoint)ipep;
                    sock.ReceiveFrom(ba, size, sf, ref ep);
                    rcs.ip = ipep.Address.Address; // Why am I getting this warning?
                }
                else
                    return -2;
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
                sock.Close();
                sock.Dispose();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public string GetError(int er_code)
        {
            if (er_code > 0)
                return "No Error";
            switch (er_code)
            {
                case -2:
                    return "Method cannot be called yet.";
                case -3:
                    return "ProtocolType not supported.";
                case -4:
                    return "The ProtocolType does not support that method.";
                default:
                    return "Unknown Error";
            }
        }

        public int stage { get; private set; }
        public int port { get; private set; }
        public ProtocolType type { get; private set; }
        private Socket sock;
    }
}

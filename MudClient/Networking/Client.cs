using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MudClient.Networking
{
    class Client
    {
        public Client()
        {
        }
        ~Client()
        {
        }
        public Boolean Initialize(string i, int p)
        {
            try
            {
                if (i.Length <= 0)
                    return false;
                ip = i;
                if (p <= 0)
                    return false;
                port = p;
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public Boolean Connect()
        {
            try
            {
                sock.Connect(ip, port);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public Boolean Send(string data,Boolean newLine)
        {
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                if(newLine)
                    data += "\n";
                sock.Send(encoding.GetBytes(data));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public Boolean Receive(out string rs,int size)
        {
            try
            {
                byte[] data = new byte[size];
                sock.Receive(data);
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                rs = enc.GetString(data.ToArray());
            }
            catch (Exception)
            {
                rs = "";
                return false;
            }
            return true;
        }
        public Boolean End()
        {
            try
            {
                sock.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private string ip;
        private int port;

        private Socket sock;
    }
}

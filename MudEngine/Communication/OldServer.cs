using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

using MudEngine.Core;

namespace MudEngine.Communication
{
    public class OldServer
    {
        /*
        public OldServer()
        {
            stage = 0;
            port = 0;
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        ~OldServer()
        {
            stage = 0;
            port = 0;
        }
        public Boolean Initialize(Int32 p, ref BaseCharacter[] pbs)
        {
            if (stage != 0)
                return false;
            if (p <= 0)
                return false;
            port = p;
            players = pbs;
            clientThreads = new Thread[players.Length];
            stage++;
            return true;
        }
        public Boolean Start()
        {
            try
            {
                if (stage != 1)
                    return false;
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
                server.Bind(ipep);
                server.Listen(10);
                stage++;
                serverThread = new Thread(ServerThread);
                serverThread.Start();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void EndServer()
        {
            stage = 0;
            serverThread.Abort();
            server.Close();
        }
        private void ServerThread()
        {
            while (stage == 2)
            {
                Int32 sub = -1;
                do
                {
                    for (Int32 i = 0; i < players.Length; i++)
                    {
                        if (!players[i].IsActive)
                        {
                            sub = i;
                            break;
                        }
                    }
                } while (sub < 0);
                players[sub].client = server.Accept();
                players[sub].IsActive = true;
                players[sub].IsControlled = true;
                clientThreads[sub] = new Thread(ReceiveThread);
                clientThreads[sub].Start((object)sub);
            }
        }
        private void ReceiveThread(object obj)
        {
            Int32 sub = (Int32)obj;
            players[sub].Initialize();
            while (stage == 2 && players[sub].IsActive)
            {
                players[sub].Receive(players[sub].ReadInput());
            }
        }
        public void Disconnect(Int32 sub)
        {
            if (sub > 0 && sub < players.Length)
            {
                clientThreads[sub].Abort();
                if (players[sub].IsActive)
                    players[sub].Disconnect();
            }
        }

        private Thread serverThread;
        private Socket server;
        private Int32 stage;
        private Int32 port;

        BaseCharacter[] players;

        private Thread[] clientThreads;
         */
    }
}
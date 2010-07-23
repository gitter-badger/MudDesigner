using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/* Usage:
 *  Server MUDServer = new Server();
 *  MUDServer.InitializeUDP(666); or MUDServer.InitializeTCP(666);
 *  MUDServer.Start();
 */

namespace MudEngine.Networking
{
    public class Server
    {
        public Server()
        {
            stage = 0;
            server = new ServerSocket();
        }
        ~Server()
        {
            stage = 0;
            if (server.type == ProtocolType.Tcp)
            {
                for (int i = 0; i < numberOfClients; i++)
                    clients[i].CleanUp();
                numberOfClients = 0;
            }
            server.CleanUp();
        }
        public bool InitializeTCP(int port, ref MudEngine.GameObjects.Characters.BaseCharacter[] pbs)
        {
            if (stage != 0)
                return false;
            if (server.Initialize(port, ProtocolType.Tcp) < 0)
                return false;

            numberOfClients = pbs.Length;
            clients = new ClientSocket[pbs.Length];
            clientThreads = new Thread[pbs.Length];
            players = pbs;

            stage++;
            return true;
        }
        public bool InitializeUDP(int port, ref MudEngine.GameObjects.Characters.BaseCharacter[] pbs)
        {
            if (stage != 0)
                return false;
            if (server.Initialize(port, ProtocolType.Udp) < 0)
                return false;

            players = pbs;
            
            stage++;
            return true;
        }
        public bool Start()
        {
            if (stage != 1)
                return false;
            if (server.Start() < 0)
                return false;
            if (server.Bind() < 0)
                return false;
            if (server.type == ProtocolType.Tcp)
                if (server.Listen() < 0)
                    return false;
            stage++;
            serverThread = new Thread(ServerThread);
            serverThread.Start();
            return true;
        }
        public void EndServer()
        {
            stage = 0;
            if (server.type == ProtocolType.Tcp)
            {
                for (int i = 0; i < numberOfClients; i++)
                    clients[i].CleanUp();
                numberOfClients = 0;
            }
            server.CleanUp();
        }
        /*
         * ServerThread, if UDP: Accepts messages(ReceiveFrom) and sends in correspondence to the correct player
         * if TCP: Accepts connection and opens a separate thread to receive a data stream between the clien
         */
        private void ServerThread()
        {
            if (server.type == ProtocolType.Udp)
            {

            }
            else
            {
                while (stage == 2)
                {
                    int sub = -1;
                    do
                    {
                        for (int i = 0; i < numberOfClients; i++)
                        {
                            if (!clients[i].used)
                            {
                                sub = i;
                                break;
                            }
                        }
                    } while (sub == -1);
                    server.Accept(clients[sub]);
                    clients[sub].used = true;
                    players[sub].Initialize(ref clients[sub]);
                    ParameterizedThreadStart start = new ParameterizedThreadStart(ReceiveThread);
                    clientThreads[sub] = new Thread(start);
                    clientThreads[sub].Start();
                }
            }
        }
        private void ReceiveThread(object obj)
        {
            int sub = (int)obj;
            while (stage == 2 && clients[sub].used)
            {
                byte[] buf = new byte[256];
                clients[sub].Receive(buf);
            }
        }

        private Thread serverThread;
        private ServerSocket server;
        private int stage;

        MudEngine.GameObjects.Characters.BaseCharacter[] players;

        // TCP Stuff:
        private ClientSocket[] clients;
        private Thread[] clientThreads;
        private int numberOfClients;
    }
}
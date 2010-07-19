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
    class Server
    {
        public Server()
        {
            stage = 0;
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
        public bool InitializeTCP(int port, int max_cons)
        {
            if (stage != 0)
                return false;
            if (server.Initialize(port, ProtocolType.Tcp) < 0)
                return false;
            numberOfClients = max_cons;
            clients = new ClientSocket[max_cons];
            stage++;
            return true;
        }
        public bool InitializeUDP(int port)
        {
            if (stage != 0)
                return false;
            if (server.Initialize(port, ProtocolType.Udp) < 0)
                return false;
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
            }
        }
        private Thread serverThread;
        private ServerSocket server;
        private int stage;
        // TCP Stuff:
        private ClientSocket[] clients;
        private int numberOfClients;
    }
}

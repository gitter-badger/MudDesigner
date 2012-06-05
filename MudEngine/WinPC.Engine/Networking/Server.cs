using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Core;
using WinPC.Engine.Directors;

namespace WinPC.Engine.Networking
{
    public class Server : IServer
    {
        public Socket Socket { get; private set; }
        public ServerStatus Status { get; private set; }

        public int Port { get; private set; }

        public ServerDirector ServerDirector { get; private set; }

        public int MaxConnections {get; private set; }

        public int MaxQueuedConnections { get; private set; }

        public bool Enabled { get; private set; }

        public string MOTD { get; private set; }

        public string ServerOwner { get; private set; }

        private Thread ServerThread { get; set; }


        public Server()
        {
            
        }

        [Category("Networking")]
        public Server(int port)
        {
            Port = port;
            Status = ServerStatus.Stopped;
            MaxConnections = 100;
            MaxQueuedConnections = 10;

            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        public void Start(Int32 maxConnections, Int32 maxQueueSize)
        {
            Logger.WriteLine("Game Server System Starting...");
            if (Status != ServerStatus.Stopped)
                return;

            Status = ServerStatus.Starting;

            
            ServerDirector = new ServerDirector(this);

            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, this.Port);
                Socket.Bind(ip);
                Socket.Listen(this.MaxQueuedConnections);

                this.Status = ServerStatus.Running;
                this.Enabled = true;

                ServerThread = new Thread(Running);
                ServerThread.Start();

                Logger.WriteLine("Server status: Running");
            }
            catch
            {
                Logger.WriteLine("Failed to star the Engines Networking Server!");
                this.Status = ServerStatus.Stopped;
                Logger.WriteLine("Server status: Stopped");
            }
        }

        public void Stop()
        {
            ServerDirector.DisconnectAll();
            ServerThread.Abort();
            Socket.Close();
            Socket = null;

            Enabled = false;
            Status = ServerStatus.Stopped;

        }

        public void Running()
        {
            while (Status == ServerStatus.Running)
            {
                ServerDirector.AddConnection(Socket.Accept());
            }
        }

      

       
    }
}
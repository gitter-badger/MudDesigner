using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Networking
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

        public IGame Game { get; protected set; }

        private Thread ServerThread { get; set; }

        public Server() : this(4000) { }

        [Category("Networking")]
        public Server(int port)
        {
            Port = port;
            Status = ServerStatus.Stopped;
            MaxConnections = 100;
            MaxQueuedConnections = 10;
            MOTD = "MUD Designer based game.";
            ServerOwner = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(Int32 maxConnections, Int32 maxQueueSize, IGame game)
        {
            Logger.WriteLine("Game Server System Starting...");
            if (Status != ServerStatus.Stopped)
                return;

            Status = ServerStatus.Starting;

            Game = game;
            
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

                // Let's add the Auto-Save feature while the server is running. - MC
                var eGame = Game as EngineGame;
                if(eGame != null)
                {
                    if(eGame.LastSave.CompareTo((DateTime.Now.Subtract(TimeSpan.FromMinutes(30.0)))) < 0)
                    {
                    //    eGame.Save();
                        
                    }
                }
            }
        }
    }
}
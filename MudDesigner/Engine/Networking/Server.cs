using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Scripting;
 
namespace MudDesigner.Engine.Networking
{
    public class Server : IServer
    {
        [Browsable(false)]
        public Socket Socket { get; private set; }

        [Browsable(false)]
        public ServerStatus Status { get; private set; }

        public int Port { get; private set; }

        [Browsable(false)]
        public ServerDirector ServerDirector { get; private set; }

        [DisplayName("Maximum Connections")]
        public int MaxConnections {get; set; }

        [DisplayName("Maximum Queued Connections")]
        public int MaxQueuedConnections { get; private set; }

        [DisplayName("Minimum New Character Password Size")]
        public int MinimumPasswordSize { get; set; }

        [Browsable(false)]
        public bool Enabled { get; private set; }

        [DisplayName("Message of the Day")]
        public string MOTD { get; private set; }

        [DisplayName("Server Owner")]
        public string ServerOwner { get; private set; }

        [Browsable(false)]
        public IGame Game { get; protected set; }

        [Browsable(false)]
        private Thread ServerThread { get; set; }

        public Server() : this(4000) { }

        [Category("Networking")]
        public Server(int port)
        {
            
            Port = port;
            Enabled = false;
            Status = ServerStatus.Stopped;
            MaxConnections = 100;
            MaxQueuedConnections = 10;
            MOTD = "MUD Designer based game.";
            ServerOwner = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        public void Start(Int32 maxConnections, Int32 maxQueueSize, IGame game)
        {
            
            Logger.WriteLine("Game Server System Starting on port " + Port.ToString());
            if (Status != ServerStatus.Stopped)
                return;

            Status = ServerStatus.Starting;

            Game = game;
            
            ServerDirector = new ServerDirector(this);

            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
            Status = ServerStatus.Stopped;
            Enabled = false;

            ServerDirector.DisconnectAll();
            Socket.Close();
            //Socket = null;

            ServerThread.Abort();
        }

        public void Running()
        {
            while (Status == ServerStatus.Running)
            {
                try
                {
                    //Don't allow anymore connections if we have hit our limit.
                    if (ServerDirector.ConnectedPlayers.Count < MaxConnections)
                        ServerDirector.AddConnection(Socket.Accept());
                }
                catch
                {
                    //Swallow for now.
                }
                // Let's add the Auto-Save feature while the server is running. - MC
                //TODO I removed this due to being hard-coded to a internal engine Type.
                //If a developer creates a custom copy (as we will for our game as well) then the game won't ever have this called. - JS
                //var eGame = Game as EngineGame;

                if(Game != null)
                {
                    if (Game.LastSave.CompareTo((DateTime.Now.Subtract(TimeSpan.FromMinutes(30.0)))) < 0)
                    {
                    //    eGame.Save();
                        
                    }
                }
            }
        }
    }
}
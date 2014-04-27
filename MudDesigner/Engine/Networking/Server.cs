//-----------------------------------------------------------------------
// <copyright file="Server.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Scripting;
using log4net;
using log4net.Config;

namespace MudDesigner.Engine.Networking
{
    /// <summary>
    /// The game engines server. The Server listens for incoming connections and then hands them off to the Server Director
    /// </summary>
    public class Server : IServer
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Server)); 

        /// <summary>
        /// Gets a reference to the current status of the server.
        /// </summary>
        [Browsable(false)]
        public ServerStatus Status { get; private set; }

        /// <summary>
        /// Gets the port that the server is running on
        /// </summary>
        public int Port { get; private set; }

        [Browsable(false)]
        public ServerDirector ServerDirector { get; private set; }

        /// <summary>
        /// Gets or Sets the maximum connections this server can have
        /// </summary>
        [DisplayName("Maximum Connections")]
        public int MaxConnections {get; set; }

        /// <summary>
        /// Gets the maximum number of queued players this server can have
        /// </summary>
        [DisplayName("Maximum Queued Connections")]
        public int MaxQueuedConnections { get; private set; }

        /// <summary>
        /// Gets or Sets the minimum password size for players creating a new account
        /// </summary>
        [DisplayName("Minimum New Character Password Size")]
        public int MinimumPasswordSize { get; set; }

        /// <summary>
        /// Gets if the Server is enabled or not
        /// </summary>
        [Browsable(false)]
        public bool Enabled { get; private set; }

        /// <summary>
        /// The Message Of The Day for the server. Printed to players upon connection.
        /// </summary>
        [DisplayName("Message of the Day")]
        public string MOTD { get; private set; }

        /// <summary>
        /// Gets the owner of the server.
        /// </summary>
        [DisplayName("Server Owner")]
        public string ServerOwner { get; set; }

        /// <summary>
        /// Gets a reference to the Game that the server belongs to.
        /// </summary>
        [Browsable(false)]
        public IGame Game { get; protected set; }

        /// <summary>
        /// The server thread that will run independently of the game thread.
        /// </summary>
        [Browsable(false)]
        private Thread ServerThread { get; set; }

        /// <summary>
        /// The socket that is used when a new player connects
        /// </summary>
        [Browsable(false)]
        private Socket Socket { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server() : this(4000) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        [Category("Networking")]
        public Server(int port)
        {   
            Port = port;
            Enabled = false;
            Status = ServerStatus.Stopped;
            MaxConnections = 100;
            MaxQueuedConnections = 10;
            MOTD = "MUD Designer based game.";

            var file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(),"log4net.config"));
            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else
            {
                BasicConfigurator.Configure();
            }
            
        }

        /// <summary>
        /// Starts the server. Once completed, it will listen for incoming connections
        /// </summary>
        /// <param name="maxConnections">Maximum connections this server will allow</param>
        /// <param name="maxQueueSize">Maximum queue size this server will allow</param>
        /// <param name="game">The game that the server will reference.</param>
        public void Start(Int32 maxConnections, Int32 maxQueueSize, IGame game)
        {
            Log.Info(string.Format("Game Server System Starting on port {0}",Port));
//            Logger.WriteLine("Game Server System Starting on port " + Port.ToString());

            // If the server is already running, abort.
            if (Status != ServerStatus.Stopped)
                return;

            // Set the status to starting
            Status = ServerStatus.Starting;

            // Store our reference to the Game
            Game = game;
            
            // Instance a new copy of ServerDirector
            ServerDirector = new ServerDirector(this);

            try
            {
                // Setup our network socket.
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, this.Port);

                // Bind to the host and listen for incoming connections
                Socket.Bind(ip);
                Socket.Listen(this.MaxQueuedConnections);

                // Set the status to running
                this.Status = ServerStatus.Running;
                this.Enabled = true;

                // Pass the new client off onto a new thread and allow it to run.
                ServerThread = new Thread(Running);
                ServerThread.Start();
                Log.Info("Server status: Running");
                // Logger.WriteLine("Server status: Running");
            }
            catch
            {
             
                Log.Fatal("Failed to start the Engines Networking Server!");
                // Logger.WriteLine("Failed to star the Engines Networking Server!");
                this.Status = ServerStatus.Stopped;
                Log.Fatal("Server status: Stopped");
                // Logger.WriteLine("Server status: Stopped");
            }
        }

        /// <summary>
        /// Stops the server if it is running
        /// </summary>
        public void Stop()
        {
            Status = ServerStatus.Stopped;
            Enabled = false;

            // Disconnect all of the players
            ServerDirector.DisconnectAll();

            // Close our network connection
            Socket.Close();
            // Socket = null;

            // Kill the server thread
            ServerThread.Abort();
        }

        /// <summary>
        /// Performs tasks related to the running of the server.
        /// </summary>
        public void Running()
        {
            while (Status == ServerStatus.Running)
            {
                try
                {
                    // Don't allow anymore connections if we have hit our limit.
                    if (ServerDirector.ConnectedPlayers.Count < MaxConnections)
                        ServerDirector.AddConnection(Socket.Accept());
                }
                catch
                {
                    // Swallow for now.
                }
                // Let's add the Auto-Save feature while the server is running. - MC
                // TODO I removed this due to being hard-coded to a internal engine Type.
                // If a developer creates a custom copy (as we will for our game as well) then the game won't ever have this called. - JS
                // var eGame = Game as EngineGame;

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
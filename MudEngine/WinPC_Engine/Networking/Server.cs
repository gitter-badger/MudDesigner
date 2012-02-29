using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MudEngine.Core;
using MudEngine.Core.Interfaces;
using MudEngine.Game;
using MudEngine.Game.Characters;

namespace MudEngine.Networking
{
    [Category("Networking")]
    public class Server
    {
        [Category("Networking")]
        [Description("The name of this server")]
        public String Name { get; set; }

        [Category("Networking")]
        public Int32 Port { get; set; }

        [Category("Networking")]
        [Description("The Message Of The Day that is presented to users when they connect.")]
        public String MOTD { get; set; }

        [Category("Networking")]
        [Description("Maximum number of people that can connect and play on this server at any time.")]
        public Int32 MaxConnections { get; set; }

        [Category("Networking")]
        [Description("Maximum number of poeple that can be queued for connection.")]
        public Int32 QueuedConnectionLimit { get; set; }

        [Browsable(false)]
        public Boolean Enabled { get; private set; }

        /// <summary>
        /// Sets up a server on the specified port.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="maxConnections"></param>
        public Server(Int32 port)
        {
            Logger.WriteLine("Initializing Mud Server Settings");
            this.Port = port;
            this.MaxConnections = 50;
            this.QueuedConnectionLimit = 20;
            this.Name = "New Server";
            this.MOTD = "Welcome to a sample Mud Engine game server!";

            this._Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._Server.Bind(new IPEndPoint(IPAddress.Any, this.Port));
        }

        /// <summary>
        /// Starts the game server.  It will listen on a different thread of incoming connections
        /// </summary>
        public void Start(StandardGame game)
        {
            //Start the server.
            Logger.WriteLine("Starting Mud Server.");

            //Start listening for connections
            this._Server.Listen(this.QueuedConnectionLimit);

            //Launch the new connections listener on a new thread.
            this._ConnectionPool = new Thread(AcceptConnection);
            this._ConnectionPool.Start();

            //Flag the server as enabled.
            this.Enabled = true;

            //Save a reference to the currently active game
            this._Game = game;

            Logger.WriteLine("Server started.");
        }

        public void Stop()
        {
            //Flag the server as disabled.
            this.Enabled = false;

            //Stop the thread from listening for accepting new conections
            this._ConnectionPool.Abort();

            //Disconnect all of the currently connected clients.
            ConnectionManager.DisconnectAll();

            //Stop the server.
            this._Server.Close();
        }

        private void AcceptConnection()
        {
            //While the server is enabled, constantly check for new connections.
            while (this.Enabled)
            {
                //Grab the new connection.
                Socket incomingConnection = this._Server.Accept();
                
                //Send it to the Connection Manager so a Character can be instanced.
                ConnectionManager.AddConnection(this._Game, incomingConnection);
            }
        }

        private Socket _Server;
        private Thread _ConnectionPool;
        private StandardGame _Game;
    }
}

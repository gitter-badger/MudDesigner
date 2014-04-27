// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Mud Designer Framework
using Mud.Core;
using Mud.Models;
using Mud.Models.Mobs;

namespace Mud.Networking
{
    /// <summary>
    /// Various states that the server can be in.
    /// </summary>
    public enum ServerStatus
    {
        Stopped = 0,
        Starting = 1,
        Running = 2
    }

    /// <summary>
    /// The Server class handles the network connections between the players and the host. It provides a asynchronous TCP/IP based Telnet server for players to connect to and play.
    /// </summary>
    public class Server : IServer
    {
        /// <summary>
        /// Gets a collection of players that are connected to the server.
        /// </summary>
        public List<IConnectionState> Connections { get; protected set; }

        /// <summary>
        /// Gets the port that the Server is currently running on.
        /// </summary>
        [DefaultValue(23)]
        public int Port { get; protected set; }

        /// <summary>
        /// Gets or Sets the maximum number of concurrent connections that the server can accept.
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of queued connections that the server can hold before rejecting connections.
        /// </summary>
        public int MaxQueuedConnections { get; set; }

        /// <summary>
        /// Gets or Sets the minimum password size for player accounts.
        /// </summary>
        public int MinimumPasswordSize { get; set; }

        /// <summary>
        /// Gets or Sets the maximum password size for a player account.
        /// </summary>
        public int MaximumPasswordSize { get; set; }

        /// <summary>
        /// Gets or Sets if the server is enabled and running or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets the current state of the server.
        /// </summary>
        public ServerStatus Status { get; protected set; }

        /// <summary>
        /// Gets or Sets theMessage of the Day broadcasted to players upon connection.
        /// </summary>
        public string MOTD { get; set; }

        /// <summary>
        /// Gets or Sets the owner of the server.
        /// </summary>
        public string Owner { get; set; }

        public IGame Game { get; protected set; }

        private Socket serverSocket;

        private Progress<EngineLogger> logger;

        /// <summary>
        /// Initializes the server with a default port.
        /// </summary>
        public Server(Progress<EngineLogger> logger = null)
        {
            this.Port = 5555;
            this.MaxConnections = 500;
            this.MaxQueuedConnections = 15;
            this.MinimumPasswordSize = 8;
            this.MaximumPasswordSize = 16;
            this.Enabled = false;
            this.Status = ServerStatus.Stopped;
            this.MOTD = "Mud Designer Toolkit Server";
            this.Owner = "No owner specified.";
            this.Connections = new List<IConnectionState>();
            this.logger = logger;
        }

        /// <summary>
        /// Initializes the server on the port number specifed.
        /// </summary>
        /// <param name="port">The port that the server will listen to for connections.</param>
        /// <param name="maxConnections">The maximum number of connections that the server can have.</param>
        /// <param name="motd">The Message of the Day broadcasted to players upon connection.</param>
        public Server(Progress<EngineLogger> logger, int port, int maxConnections = 500, string motd = "Mud Designer Toolkit Server")
            : this(logger)
        {
            this.Port = port;
            this.MaxConnections = maxConnections;
            this.MOTD = motd;
        }

        public void Start(IGame game)
        {
            this.Game = game;
            
            // Get our server address information.
            IPHostEntry serverHost = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, this.Port);

            // Instance the server socket, bind it to a port and begin listening for connections.
            this.serverSocket = new Socket(serverEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(serverEndPoint);
            this.serverSocket.Listen(this.MaxQueuedConnections);

            IAsyncResult result = this.serverSocket.BeginAccept(new AsyncCallback(this.Connect), this.serverSocket);
        }

        public void Stop()
        {
            this.Status = ServerStatus.Stopped;
            this.Enabled = false;

            foreach (IConnectionState connection in this.Connections)
            {
                // Disconnect synchronously so that the server is not shut down prior to users disconnect code running.
                connection.Disconnect();
            }

            // Cloe the server connection.
            this.serverSocket.Shutdown(SocketShutdown.Both);
        }

        private void Connect(IAsyncResult result)
        {
            var player = new Player();
            try
            {
                player.Game = this.Game;
                player.Connection = this.serverSocket.EndAccept(result);

                lock (this.Connections)
                {
                    this.Connections.Add(player);
                }

                // Pass all of the data handling for the player to itself.
                player.Connection.BeginReceive(player.Buffer, 0, player.BufferSize, SocketFlags.None, new AsyncCallback(player.ReceiveData), player);

                // Fetch the next incoming connection.
                this.serverSocket.BeginAccept(new AsyncCallback(this.Connect), this.serverSocket);
            }
            catch (Exception)
            {
            }
        }

        public void Disconnect(IConnectionState connection)
        {
            throw new NotImplementedException();
        }

        public void DisconnectAll()
        {
            throw new NotImplementedException();
        }
    }
}

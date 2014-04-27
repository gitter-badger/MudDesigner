//-----------------------------------------------------------------------
// <copyright file="CoreServer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MudEngine.Engine.Core
{
    public class EngineServer : IServer
    {
        /// <summary>
        /// The Server properties object.
        /// </summary>
        private readonly ServerProperties properties = ServerProperties.Default;

        private Socket serverSocket;

        public EngineServer()
        {
            this.Status = ServerStatus.Stopped;
            this.Connections = new List<IServerConnectionState>();
            
            // The server is enabled for use. Does not indicate that it is running.
            this.IsEnabled = false;
        }

        /// <summary>
        /// Gets a collection of current user connections.
        /// </summary>
        public List<IServerConnectionState> Connections { get; protected set; }

        /// <summary>
        /// Gets or Sets the port that the server is running on.
        /// </summary>
        public int Port
        {
            get
            {
                return properties.Port;
            }
            set
            {
                properties.Port = value;
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets or sets the maximum connections.
        /// </summary>
        public int MaxConnections
        {
            get
            {
                return properties.MaxConnections;
            }
            set
            {
                properties.MaxConnections = value;
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets or sets the maximum queued connections.
        /// </summary>
        public int MaxQueuedConnections
        {
            get
            {
                return properties.MaxQueuedConnections;
            }
            set
            {
                properties.MaxQueuedConnections = value;
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets or sets the minimum size of the password.
        /// </summary>
        public int MinimumPasswordSize
        {
            get
            {
                return properties.MinimumPasswordSize;
            }
            set
            {
                properties.MinimumPasswordSize = value;
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets or sets the maximum size of the password.
        /// </summary>
        public int MaximumPasswordSize
        {
            get
            {
                return properties.MaximumPasswordSize;
            }
            set
            {
                properties.MaximumPasswordSize = value;
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets or sets the message of the day.
        /// </summary>
        public List<string> MessageOfTheDay
        {
            get
            {
                return properties.MessageOfTheDay.Cast<string>().ToList();
            }
            set
            {
                properties.MessageOfTheDay.Clear();
                properties.MessageOfTheDay.AddRange(value.ToArray());
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public string Owner
        {
            get
            {
                return properties.Owner;
            }
            set
            {
                properties.Owner = value;
                ServerProperties.Synchronized(properties);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IServer" /> is enabled.
        /// </summary>
        public bool IsEnabled { get; protected set; }

        /// <summary>
        /// Gets the current server status.
        /// </summary>
        public ServerStatus Status { get; protected set; }

        /// <summary>
        /// Gets the currently running game.
        /// </summary>
        public IGame Game { get; protected set; }

        /// <summary>
        /// Starts the server using the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Start(IGame game)
        {
            this.Status = ServerStatus.Starting;

            // Validate our settings.
            if (this.Port <= 0)
            {
                throw new Exception("Invalid Port number used. Recommended number is 23 or 4000");
            }

            if (this.MaxConnections < 2)
            {
                throw new Exception("Invalid MaxConnections number used. Must be greater than 1.");
            }

            if (game == null)
            {
                throw new NullReferenceException("The game parameter can not be null. Provide a valid IGame object.");
            }

            this.Game = game;

            // Get our server address information
            IPHostEntry serverHost = Dns.GetHostEntry(Dns.GetHostName());
            var serverEndPoint = new IPEndPoint(IPAddress.Any, this.Port);

            // Instance the server socket, bind it to a port and begin listening for connections.
            this.serverSocket = new Socket(serverEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(serverEndPoint);
            this.serverSocket.Listen(this.MaxQueuedConnections);

            IAsyncResult result = this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient), this.serverSocket);

            this.Status = ServerStatus.Running;
        }

        /// <summary>
        /// Stops server, shutting down the network connection.
        /// All IServerConnectionState objects will be disconnected.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Stop()
        {
            foreach(IServerConnectionState connection in this.Connections)
            {
                connection.Disconnect();
            }

            if (this.serverSocket.Connected)
            {
                this.serverSocket.Shutdown(SocketShutdown.Both);
            }

            this.Status = ServerStatus.Stopped;
            this.IsEnabled = false;
        }

        /// <summary>
        /// Disconnects the specified IServerConnectionState object.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Disconnect(IServerConnectionState connection)
        {
            // Ensure the connection is still valid.
            if (connection != null && connection.Connection != null && connection.Connection.Connected)
            {
                connection.Connection.Disconnect(true);
            }
        }

        /// <summary>
        /// Disconnects everyone from the server.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DisconnectAll()
        {
            // Disconnect every client from the server.
            foreach(IServerConnectionState connection in this.Connections.AsParallel())
            {
                if (connection != null && connection.Connection != null && connection.Connection.Connected)
                {
                    connection.Connection.Disconnect(false);
                }
            }
        }

        /// <summary>
        /// Connects the client to the server and then passes the connection responsibilities to the client object.
        /// </summary>
        /// <param name="result">The async result.</param>
        private void ConnectClient(IAsyncResult result)
        {
            // Set up a default player object if one is not done so already.
            if (Factories.MobFactory.DefaultPlayer == null)
            {
                var enginePlayer = new Mob.EnginePlayer();
                enginePlayer.Game = this.Game;
                Factories.MobFactory.DefaultPlayer = enginePlayer;
            }

            // Fetch the defualt player class.
            var player = Factories.MobFactory.GetDefaultPlayer();
            player.Game = this.Game;

            try
            {
                // Connect the player to the server.
                player.Connect(this.serverSocket.EndAccept(result));

                lock (this.Connections)
                {
                    this.Connections.Add(player);
                }
            }
            catch(Exception)
            {
                throw;
            }

            // Pass all of the incoming data handling for the players connection, to the player object itself.
            player.Connection.BeginReceive(player.Buffer, 0, player.BufferSize, SocketFlags.None, new AsyncCallback(player.ReceiveData), player);

            // Fetch the next incoming connection.
            this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient), this.serverSocket);
        }
    }
}

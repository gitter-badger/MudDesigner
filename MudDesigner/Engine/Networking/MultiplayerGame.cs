//-----------------------------------------------------------------------
// <copyright file="MultiplayerGame.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Mob.States;
using MudEngine.Engine.GameObjects.Mob.States.MultiplayerStates;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// An implementation of IGame and IServer, providing a game that supports networking with more than one player.
    /// </summary>
    public class MultiplayerGame : DefaultGame, IServer
    {
        /// <summary>
        /// The server socket
        /// </summary>
        private Socket serverSocket;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplayerGame"/> class.
        /// </summary>
        public MultiplayerGame()
        {
            this.Connections = new List<IServerPlayer>();
            this.MessageOfTheDay = new List<string>();

            // The server is enabled for use. Does not indicate that it is running.
            this.IsEnabled = true;
            this.IsMultiplayer = true;
            this.Port = 23;
            this.Status = ServerStatus.Stopped;
            this.MaxConnections = 100;
            this.Owner = "Sully";

            // Defaults
            this.Version = new Version(3, 0, 0);
            this.Name = "Sample Multiplayer MUD Game";
            this.Description = "This game provides a simple example of what can be done using the Mud Designer game engine.";
            this.MessageOfTheDay.Add("Welcome to the MUD Designer Multiplayer Sample Game!");
            this.MessageOfTheDay.Add(string.Format("We are currently working on version {0}.", this.Version.ToString()));
        }

        /// <summary>
        /// Gets or sets a collection of current user connections.
        /// </summary>
        public List<IServerPlayer> Connections { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IServer" /> is enabled.
        /// </summary>
        public bool IsEnabled { get; protected set; }

        /// <summary>
        /// Gets or sets the current server status.
        /// </summary>
        public ServerStatus Status { get; protected set; }

        /// <summary>
        /// Gets or sets the port that the server is running on.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the maximum connections.
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        /// Gets or sets the maximum queued connections.
        /// </summary>
        public int MaxQueuedConnections { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of the password.
        /// </summary>
        public int MinimumPasswordSize { get; set; }
        /// <summary>
        /// Gets or sets the maximum size of the password.
        /// </summary>
        public int MaximumPasswordSize { get; set; }

        /// <summary>
        /// Gets or sets the message of the day.
        /// </summary>
        public List<string> MessageOfTheDay { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Initializes the specified storage source and server.
        /// </summary>
        /// <typeparam name="T">A Type that implements IPlayer</typeparam>
        /// <param name="storageSource">The storage source.</param>
        /// <exception cref="System.NullReferenceException">The storageSource parameter can not be null.</exception>
        public override void Initialize<T>(IPersistedStorage storageSource)
        {
            if (storageSource == null)
            {
                this.LogMessage("initializing the persisted storage system failed! No data will be restored.");
            }
            else
            {
                this.StorageSource = storageSource;
                this.StorageSource.InitializeStorage();
            }

            try
            {
                this.SetupWorlds();
            }
            catch (Exception e)
            {
                this.LogMessage(string.Format("Error setting up the worlds.\n{0}", e.Message));
            }

            // If a server exists and is running, we are good to go. If no server, then we default to Running = true;
            this.IsRunning = this.Worlds != null; // && this.Worlds.Count > 0;
        }

        /// <summary>
        /// Broadcasts the specified message to the user.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="message">The message.</param>
        public override void BroadcastToPlayer(IMob sender, IMessage message)
        {
            if ((sender != null && sender is IServerPlayer) || sender != null && this.Connections.Any(serverPlayer => serverPlayer.Player == sender))
            {
                var player = sender as IServerPlayer;

                // We know we have one in our connection collection thanks to our if condition check above.
                if (player == null)
                {
                    player = this.Connections.FirstOrDefault(connection => connection.Player == sender) as IServerPlayer;
                }

                // When printing properties that don't have values, they'll
                // be null.
                if (message == null || string.IsNullOrWhiteSpace(message.Message))
                {
                    return;
                }

                // Make sure we are still connected
                try
                {
                    string formattedMessage = message.FormatMessage();

                    if (player.Connection.Connected)
                    {
                        player.Connection.Send(new ASCIIEncoding().GetBytes(formattedMessage));
                    }
                }
                catch (Exception ex)
                {
                    // No connection was made, so make sure we clean up
                    if (!player.Connection.Connected)
                    {
                        player.Disconnect();
                    }
                }
            }
        }

        /// <summary>
        /// Starts the server with support for the supplied IPlayer object. 
        /// The IPlayer object will be wrapped within the supplied IServerPlayer object.
        /// </summary>
        /// <typeparam name="TServerObject">A Type that implements IServerPlayer</typeparam>
        /// <typeparam name="UPlayerObject">A Type that implements IPlayer</typeparam>
        /// <exception cref="System.Exception">Invalid Port number used. Recommended number is 23 or 4000
        /// or
        /// Invalid MaxConnections number used. Must be greater than 1.</exception>
        /// <exception cref="System.NullReferenceException">The game parameter can not be null. Provide a valid IGame object.</exception>
        public void Start<TServerObject, UPlayerObject>() where TServerObject : class, IServerPlayer, new() where UPlayerObject : class, IPlayer, new()
        {
            this.Status = ServerStatus.Starting;
            this.LogMessage("Starting network server.");

            // Validate our settings.
            if (this.Port <= 0)
            {
                throw new Exception("Invalid Port number used. Recommended number is 23 or 4000");
            }

            if (this.MaxConnections < 2)
            {
                throw new Exception("Invalid MaxConnections number used. Must be greater than 1.");
            }

            // Get our server address information
            IPHostEntry serverHost = Dns.GetHostEntry(Dns.GetHostName());
            var serverEndPoint = new IPEndPoint(IPAddress.Any, this.Port);

            // Instance the server socket, bind it to a port.
            this.serverSocket = new Socket(serverEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(serverEndPoint);
            this.serverSocket.Listen(this.MaxQueuedConnections);

            // Begin listening for connections.
            IAsyncResult result = this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient<TServerObject, UPlayerObject>), this.serverSocket);

            this.Status = ServerStatus.Running;
        }

        /// <summary>
        /// Stops the server, shutting down the network connection.
        /// All IServerPlayer objects will be disconnected.
        /// </summary>
        public void Stop()
        {
            this.LogMessage("Stopping the network server.");

            // Loop through each connection in parallel and disconnect them.
            foreach (IServerPlayer connection in this.Connections.AsParallel())
            {
                // Hold a locally scoped reference to avoid parallel issues.
                IServerPlayer client = connection;
                client.Disconnect();
            }

            // If the server socket is still connected, we shut it down.
            if (this.serverSocket.Connected)
            {
                this.serverSocket.Shutdown(SocketShutdown.Both);
            }

            this.Status = ServerStatus.Stopped;
            this.IsEnabled = false;
        }

        /// <summary>
        /// Disconnects the specified server player.
        /// </summary>
        /// <param name="serverPlayer">The server player.</param>
        public void Disconnect(IServerPlayer serverPlayer)
        {
            // Ensure the connection is still valid.
            if (serverPlayer != null && serverPlayer.Connection != null && serverPlayer.Connection.Connected)
            {
                serverPlayer.Disconnect();
            }
            
            if (this.Connections.Contains(serverPlayer))
            {
                this.Connections.Remove(serverPlayer);
            }
        }

        /// <summary>
        /// Disconnects everyone from the server.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Throws an exception if the player is null</exception>
        public void DisconnectAll()
        {
            // Disconnect every client from the server.
            foreach (IServerPlayer serverPlayer in this.Connections.AsParallel())
            {
                IServerPlayer player = serverPlayer;
                if (player != null && player.Connection != null && player.Connection.Connected)
                {
                    player.Disconnect();
                }
            }

            this.Connections.Clear();
        }

        /// <summary>
        /// Connects the client to the server and then passes the connection responsibilities to the client object.
        /// </summary>
        /// <typeparam name="TServerObject">The type of the server object.</typeparam>
        /// <typeparam name="UPlayerObject">The type of the player object.</typeparam>
        /// <param name="result">The async result.</param>
        private void ConnectClient<TServerObject, UPlayerObject>(IAsyncResult result) where TServerObject : class, IServerPlayer, new() where UPlayerObject : class, IPlayer, new()
        {
            // Fetch the next incoming connection.
            this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient<TServerObject, UPlayerObject>), this.serverSocket);

            // Fetch the defualt player class.
            IServerPlayer serverObject = new TServerObject();

            try
            {
                // Connect the player to the server.
                var player = new UPlayerObject();    
        
                // Register for the events we need to now of and initialize the player.
                player.SendMessage += (sender, message) => this.BroadcastToPlayer(sender as IMob, message);
                player.Initialize(this);

                lock (this.Connections)
                {
                    this.Connections.Add(serverObject);
                    player.Name = string.Format("Player {0}", this.Connections.Count);
                }

                // Connect and register for network related events.
                serverObject.Connect(this.serverSocket.EndAccept(result), player);
                serverObject.Disconnected += (sender, args) => this.LogMessage(string.Format("{0} disconnected. ", (sender as IServerPlayer).Player.Name)); 
            }
            catch (Exception)
            {
                throw;
            }

            this.LogMessage(string.Format("{0} connected.", serverObject.Player.Name));

            // Pass all of the incoming data handling for the players connection, to the player object itself.
            serverObject.Connection.BeginReceive(serverObject.Buffer.ToArray(), 0, serverObject.BufferSize, SocketFlags.None, new AsyncCallback(serverObject.ReceiveData), serverObject);
        }
    }
}

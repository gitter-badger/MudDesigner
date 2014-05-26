using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MudEngine.Engine.GameObjects.Environment;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Networking
{
    public class MultiplayerGame : DefaultGame, IServer
    {
        /// <summary>
        /// The Server properties object.
        /// </summary>
        private readonly ServerProperties properties = ServerProperties.Default;

        /// <summary>
        /// The server socket
        /// </summary>
        private Socket serverSocket;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineServer"/> class.
        /// </summary>
        public MultiplayerGame()
        {
            this.Status = ServerStatus.Stopped;
            this.Connections = new List<IServerPlayer>();

            // The server is enabled for use. Does not indicate that it is running.
            this.IsEnabled = true;
        }

        /// <summary>
        /// Gets a collection of current user connections.
        /// </summary>
        public List<IServerPlayer> Connections { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IServer" /> is enabled.
        /// </summary>
        public bool IsEnabled { get; protected set; }

        /// <summary>
        /// Gets the current server status.
        /// </summary>
        public ServerStatus Status { get; protected set; }

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
        /// Initializes the specified storage source and server.
        /// </summary>
        /// <param name="storageSource">The storage source.</param>
        /// <exception cref="System.NullReferenceException">The storageSource parameter can not be null.</exception>
        public override void Initialize<T>(IPersistedStorage storageSource)
        {
            // We don't invoke our base.Initialize(storageSource) because we want to
            // handle setting up the player using our server.

            // Check if the storage source is null.
            if (storageSource == null)
            {
                // throw new NullReferenceException("The storageSource parameter can not be null.");
            }

            //this.StorageSource = storageSource;
            //this.StorageSource.InitializeStorage();

            this.SetupWorlds();

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
            if (sender != null && sender is ServerPlayer)
            {
                var player = sender as ServerPlayer;

                // When printing properties that don't have values, they'll
                // be null.
                if (message == null || string.IsNullOrWhiteSpace(message.Message))
                {
                    return;
                }

                // Make sure we are still connected
                try
                {
                    string formattedMessage = this.FormatMessageForBroadcasting(message);

                    if (player.Connection.Connected)
                    {
                        player.Connection.Send(new ASCIIEncoding().GetBytes(string.Format("Message => {0}", formattedMessage)));
                    }
                }
                catch (Exception ex)
                {
                    // No connection was made, so make sure we clean up
                    if (!player.Connection.Connected)
                        player.Disconnect();
                }
            }
        }

        /// <summary>
        /// Starts the server using the specified game.
        /// </summary>
        /// <typeparam name="T">The IPlayer Type used to instance new objects upon a player connecting to the server.</typeparam>
        /// <param name="game">The game.</param>
        /// <exception cref="System.Exception">
        /// Invalid Port number used. Recommended number is 23 or 4000
        /// or
        /// Invalid MaxConnections number used. Must be greater than 1.
        /// </exception>
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
        /// All IServerConnectionState objects will be disconnected.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
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
        /// Disconnects the specified IServerConnectionState object.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Disconnect(IServerPlayer connection)
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
            foreach (IServerPlayer connection in this.Connections.AsParallel())
            {
                IServerPlayer client = connection;
                if (client != null && client.Connection != null && client.Connection.Connected)
                {
                    client.Connection.Disconnect(false);
                }
            }
        }

        /// <summary>
        /// Connects the client to the server and then passes the connection responsibilities to the client object.
        /// </summary>
        /// <typeparam name="T">The IPlayer Type used to instance new objects upon a player connecting to the server.</typeparam>
        /// <param name="result">The async result.</param>
        private void ConnectClient<TServerObject, UPlayerObject>(IAsyncResult result) where TServerObject : class, IServerPlayer, new() where UPlayerObject : class, IPlayer, new()
        {
            var factory = new EngineFactory<TServerObject>();

            // Fetch the defualt player class.
            IServerPlayer serverObject = factory.GetObject();

            try
            {
                // Connect the player to the server.
                serverObject.Connect(this.serverSocket.EndAccept(result), new UPlayerObject());

                lock (this.Connections)
                {
                    this.Connections.Add(serverObject);
                }
            }
            catch (Exception)
            {
                throw;
            }

            serverObject.Player.Name = string.Format("Player {0}", this.Connections.Count);
            this.LogMessage(string.Format("{0} connected.", serverObject.Player.Name));

            // IPlayer player                 
            serverObject.Player.SendMessage += (sender, message) => this.BroadcastToPlayer(sender as IMob, message);
            serverObject.Disconnected += (sender, args) => this.LogMessage(string.Format("{0} disconnected. ", (sender as IServerPlayer).Player.Name)); 
            serverObject.Player.Initialize(this);

            // Pass all of the incoming data handling for the players connection, to the player object itself.
            serverObject.Connection.BeginReceive(serverObject.Buffer.ToArray(), 0, serverObject.BufferSize, SocketFlags.None, new AsyncCallback(serverObject.ReceiveData), serverObject);

            // Fetch the next incoming connection.
            this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient<TServerObject, UPlayerObject>), this.serverSocket);
        }
    }
}

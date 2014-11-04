//-----------------------------------------------------------------------
// <copyright file="DefaultServer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Components.WindowsServer.Networking
{
    using Mud.Engine.Runtime.Core;
    using Mud.Engine.Shared.Character;
    using Mud.Engine.Shared.Commanding;
    using Mud.Engine.Shared.Core;
    using Mud.Engine.Shared.Networking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// The Default Desktop game Server
    /// </summary>
    public class DefaultServer : IServer
    {
        /// <summary>
        /// The user connection buffer size
        /// </summary>
        private const int UserConnectionBufferSize = 1024;

        /// <summary>
        /// The server socket
        /// </summary>
        private Socket serverSocket;

        /// <summary>
        /// The player connections
        /// </summary>
        private Dictionary<IPlayer, Socket> playerConnections;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServer"/> class.
        /// </summary>
        public DefaultServer()
        {
            this.ConnectedPlayers = new List<IPlayer>();
            this.MessageOfTheDay = new List<string>();
            this.Status = new ServerStatus();

            // TODO: 11/3/14 - Change the Type to ConcurrentDictionary for thread-safety.
            this.playerConnections = new Dictionary<IPlayer, Socket>();
        }

        /// <summary>
        /// Occurs when a player connects to the server.
        /// </summary>
        public event EventHandler<ServerConnectionEventArgs> PlayerConnected;

        /// <summary>
        /// Occurs when a player is disconnected from the server.
        /// </summary>
        public event EventHandler<ServerConnectionEventArgs> PlayerDisconnected;

        /// <summary>
        /// Gets the game.
        /// </summary>
        public IGame Game { get; private set; }

        /// <summary>
        /// Gets a collection of users currently connected.
        /// </summary>
        public ICollection<IPlayer> ConnectedPlayers { get; private set; }

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
        public ICollection<string> MessageOfTheDay { get; set; }

        /// <summary>
        /// Gets or sets the server owner.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IServer" /> is enabled.
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// Gets the current server status.
        /// </summary>
        public ServerStatus Status { get; private set; }

        public IPlayerConnectCommand ConnectionCommand { get; set; }

        /// <summary>
        /// Starts the server for the specified game.
        /// </summary>
        /// <typeparam name="TPlayer">The type of the player to instance when a new user connects.</typeparam>
        /// <param name="game">The game.</param>
        /// <exception cref="System.NullReferenceException">Server can not start with a null Game.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Invalid Port number used. Recommended number is 23 or 4000
        /// or
        /// Invalid MaxConnections number used. Must be greater than 1.
        /// </exception>
        public void Start<TPlayer>(IGame game)
            where TPlayer : class, IPlayer, new()
        {
            // Ensure we have a valid game.
            ExceptionFactory
                .ThrowExceptionIf<ArgumentNullException>(game == null, () => new ArgumentNullException("game", "Game must not be null!"))
                .ElseDo(() => this.Game = game);

            ////if (this.ConnectionCommand == null)
            ////{
            ////    throw new NullReferenceException("ConnectionCommand can not be null. A command must be given for execution upon player connection");
            ////}
            ////else
            ////{
            ////    this.ConnectionCommand.Initialize<TPlayer>();
            ////}

            this.Status = ServerStatus.Starting;
            //// this.Logger("Starting network server.");

            // Validate our settings.
            ExceptionFactory
                .ThrowExceptionIf<InvalidOperationException>(this.Port <= 0, "Invalid Port number used. Recommended number is 23 or 4000");
            ExceptionFactory
                .ThrowExceptionIf<InvalidOperationException>(this.MaxConnections < 2, "Invalid MaxConnections number used. Must be greater than 1.");

            // Get our server address information
            IPHostEntry serverHost = Dns.GetHostEntry(Dns.GetHostName());
            var serverEndPoint = new IPEndPoint(IPAddress.Any, this.Port);

            // Instance the server socket, bind it to a port.
            this.serverSocket = new Socket(serverEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(serverEndPoint);
            this.serverSocket.Listen(this.MaxQueuedConnections);

            // Begin listening for connections.
            IAsyncResult result = this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient<TPlayer>), this.serverSocket);

            this.Status = ServerStatus.Running;
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void Stop()
        {
            // this.LogMessage("Stopping the network server.");
            this.DisconnectAll();

            // If the server socket is still connected, we shut it down.

            // We test to ensure the server socket is still connected and active.
            this.serverSocket.Blocking = false;
            try
            {
                this.serverSocket.Send(new byte[1], 0, 0);

                // Message was received meaning it's still receiving, so we can safely shut it down.
                this.serverSocket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException e)
            {
                // Error code 10035 indicates it works, but will block the socket.
                // This means it is still receiving and we can safely shut it down.
                // Otherwise, it's not receiving anything and we don't need to shut down.
                if (e.NativeErrorCode.Equals(10035))
                {
                    this.serverSocket.Shutdown(SocketShutdown.Both);
                }
            }
            finally
            {
                this.Status = ServerStatus.Stopped;
                this.IsEnabled = false;
            }
        }

        /// <summary>
        /// Disconnects the specified IServerPlayer object.
        /// </summary>
        /// <param name="player">The Player to disconnect.</param>
        public void Disconnect(IPlayer player)
        {
            Socket connection = this.playerConnections.FirstOrDefault(c => c.Key == player).Value;
            if (connection != null && connection.Connected)
            {
                connection.Shutdown(SocketShutdown.Both);
                this.playerConnections.Remove(player);
                this.ConnectedPlayers.Remove(player);

                this.OnPlayerDisconnected(player);
            }
        }

        /// <summary>
        /// Disconnects everyone from the server.
        /// </summary>
        public void DisconnectAll()
        {
            // Loop through each connection in parallel and disconnect them.
            foreach (KeyValuePair<IPlayer, Socket> playerConnection in this.playerConnections.AsParallel())
            {
                // Hold a locally scoped reference to avoid parallel issues.
                Socket connection = playerConnection.Value;
                IPlayer player = playerConnection.Key;

                if (connection != null && connection.Connected)
                {
                    connection.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    this.OnPlayerDisconnected(player);
                }
            }

            this.playerConnections.Clear();
            this.ConnectedPlayers.Clear();
        }

        /// <summary>
        /// Called when a player connects.
        /// </summary>
        /// <param name="newPlayer">The new player.</param>
        protected virtual void OnPlayerConnected(IPlayer newPlayer)
        {
            EventHandler<ServerConnectionEventArgs> handler = this.PlayerConnected;
            if (handler == null)
            {
                return;
            }

            handler(this, new ServerConnectionEventArgs(newPlayer));
        }

        /// <summary>
        /// Called when a player disconnects.
        /// </summary>
        /// <param name="player">The player.</param>
        protected virtual void OnPlayerDisconnected(IPlayer player)
        {
            EventHandler<ServerConnectionEventArgs> handler = this.PlayerDisconnected;
            if (handler == null)
            {
                return;
            }

            handler(this, new ServerConnectionEventArgs(player));
        }

        /// <summary>
        /// Connects the client to the server and then passes the connection responsibilities to the client object.
        /// </summary>
        /// <typeparam name="TPlayer">The type of the player.</typeparam>
        /// <param name="result">The async result.</param>
        private void ConnectClient<TPlayer>(IAsyncResult result)
            where TPlayer : class, IPlayer, new()
        {
            // Fetch the next incoming connection.
            this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectClient<TPlayer>), this.serverSocket);

            ////IPlayerConnectCommand command = new TCommand();
            ////command.Executed += this.OnConnectionCommandExecuted;

            ////try
            ////{
            ////    command.Execute(null, null);
            ////}
            ////catch (Exception)
            ////{
            ////    throw;
            ////}

            // this.LogMessage(string.Format("{0} connected.", serverObject.Player.Name));
        }

        private void OnConnectionCommandExecuted(object sender, CommandEventHandler args)
        {
            if (args.Handled)
            {
                // Cast the invoker to IPlayer. If the Invoker is not an IPlayer, we want to throw an exception.
                IPlayer player = (IPlayer)args.Invoker;

                // Register for the events we need to now of and initialize the player.
                player.Initialize(this.Game);

                lock (this.ConnectedPlayers)
                {
                    this.ConnectedPlayers.Add(player);
                    player.Name = string.Format("Player {0}", this.ConnectedPlayers.Count);
                }

                // Connect and register for network related events.
                Socket connection = null; // this.serverSocket.EndAccept(result);

                lock (this.playerConnections)
                {
                    this.playerConnections.Add(player, connection);
                }

                connection.BeginReceive(new byte[UserConnectionBufferSize], 0, UserConnectionBufferSize, SocketFlags.None, new AsyncCallback(this.ReceiveData), player);
                this.OnPlayerConnected(player);
            }
        }

        /// <summary>
        /// Receives the input data from the user.
        /// </summary>
        /// <param name="result">The result.</param>
        private void ReceiveData(IAsyncResult result)
        {
            // The input s tring
            string input = string.Empty;
            var buffer = new List<byte>();

            IPlayer player = result.AsyncState as IPlayer;
            Socket connection = this.playerConnections.FirstOrDefault(c => c.Key == player).Value;

            // This loop will forever run until we have received \n from the player
            while (true && connection != null && connection.Connected)
            {
                try
                {
                    byte[] buf = new byte[1];

                    // Make sure we are still connected
                    if (!connection.Connected)
                    {
                        this.OnPlayerDisconnected(player);
                    }

                    // Receive input from the socket connection
                    int recved = connection.Receive(buf);

                    // If we have received data, prep it for use
                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && buffer.Count > 0)
                        {
                            if (buffer[buffer.Count - 1] == '\r')
                            {
                                buffer.RemoveAt(buffer.Count - 1);
                            }

                            // Format the input
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                            // Convert the bytes into a s tring
                            input = enc.GetString(buffer.ToArray());

                            // Clear out our buffer
                            buffer.Clear();

                            // Return a trimmed string.
                            player.SendMessage(new SystemMessage(input));
                        }
                        else
                        {
                            // otherwise keep adding the input to our bufer
                            buffer.Add(buf[0]);
                        }
                    }
                    else if (recved == 0)
                    {
                        // Disconnected
                        this.Disconnect(player);
                    }
                }
                catch (Exception)
                {
                    this.Disconnect(player);
                }
            }
        }
    }
}

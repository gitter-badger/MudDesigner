using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Apps.Engine.Networking
{
    public interface IServer
    {
        /// <summary>
        /// Gets a collection of current user connections.
        /// </summary>
        List<IServerPlayer> Connections { get; }

        /// <summary>
        /// Gets or sets the port that the server is running on.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets the maximum connections.
        /// </summary>
        int MaxConnections { get; set; }

        /// <summary>
        /// Gets or sets the maximum queued connections.
        /// </summary>
        int MaxQueuedConnections { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of the password.
        /// </summary>
        int MinimumPasswordSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the password.
        /// </summary>
        int MaximumPasswordSize { get; set; }

        /// <summary>
        /// Gets or sets the message of the day.
        /// </summary>
        List<string> MessageOfTheDay { get; set; }

        /// <summary>
        /// Gets or sets the server owner.
        /// </summary>
        string Owner { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IServer"/> is enabled.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Gets the current server status.
        /// </summary>
        ServerStatus Status { get; }

        /// <summary>
        /// Starts the server using the specified game.
        /// </summary>
        /// <typeparam name="TServerObject">The type of the server player object.</typeparam>
        /// <typeparam name="UPlayerObject">The type of the player object.</typeparam>
        void Start<TServerObject, UPlayerObject>()
            where TServerObject : class, IServerPlayer, new()
            where UPlayerObject : class, IPlayer, new();

        /// <summary>
        /// Stops the server.
        /// </summary>
        void Stop();

        /// <summary>
        /// Disconnects the specified IServerPlayer object.
        /// </summary>
        /// <param name="connection">The connection.</param>
        void Disconnect(IServerPlayer connection);

        /// <summary>
        /// Disconnects everyone from the server..
        /// </summary>
        void DisconnectAll();
    }
}

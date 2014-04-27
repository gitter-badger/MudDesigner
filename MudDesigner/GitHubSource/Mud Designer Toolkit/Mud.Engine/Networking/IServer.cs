// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Mud Designer Framework
using Mud.Core;
using Mud.DataAccess;
using Mud.Models.Mobs;

namespace Mud.Networking
{
    /// <summary>
    /// IServer based objects runs the game over the network allowing multiple people to connect and play at the same time.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets a collection of (presumably IPlayer) objects implementing IConnectionState.
        /// </summary>
        List<IConnectionState> Connections { get; }

        int Port { get; }
        int MaxConnections { get; set; }
        int MaxQueuedConnections { get; set; }

        int MinimumPasswordSize { get; set; }
        int MaximumPasswordSize { get; set; }

        bool Enabled { get; set; }
        ServerStatus Status { get; }

        string MOTD { get; set; }
        string Owner { get; set; }

        IGame Game { get; }

        void Start(IGame game);
        void Stop();

        void Disconnect(IConnectionState connection);
        void DisconnectAll();
    }
}

//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.Mob
{
    /// <summary>
    /// Creates a contract for objects whom want to implement IPlayer
    /// </summary>
    public interface IPlayer : IMob, IServerConnectionState
    {
        /// <summary>
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        void Connect(Socket socket);
    }
}

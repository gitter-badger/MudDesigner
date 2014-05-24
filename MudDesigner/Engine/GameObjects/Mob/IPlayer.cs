//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net.Sockets;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob
{
    /// <summary>
    /// Creates a contract for objects whom want to implement IPlayer
    /// </summary>
    public interface IPlayer : IMob, IServerObject
    {
        /// <summary>
        /// Connects the user via the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        void Connect(Socket socket);
    }
}

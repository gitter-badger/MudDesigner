//-----------------------------------------------------------------------
// <copyright file="ServerStatus.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MudEngine.Engine.Networking
{
    /// <summary>
    /// Provides different statuses for a server.
    /// </summary>
    public enum ServerStatus
    {
        /// <summary>
        /// The server has stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// Server is in the process of starting.
        /// </summary>
        Starting,

        /// <summary>
        /// Server is up and running.
        /// </summary>
        Running
    }
}

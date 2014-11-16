//-----------------------------------------------------------------------
// <copyright file="ServerStatus.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
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

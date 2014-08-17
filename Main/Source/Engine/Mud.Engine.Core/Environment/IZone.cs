//-----------------------------------------------------------------------
// <copyright file="IZone.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides a contract for creating Zone objects.
    /// </summary>
    public interface IZone : IEnvironment
    {
        /// <summary>
        /// Gets or sets the rooms within this Zone.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        IEnumerable<IRoom> Rooms { get; set; }
    }
}

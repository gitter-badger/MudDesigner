//-----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using Mud.Engine.Core.Engine;
    using System;

    /// <summary>
    /// Defines properties that are used across various environments such as worlds, realms and zones.
    /// </summary>
    public interface IEnvironment : IPersistedObject
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        double TimeFromCreation { get; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}

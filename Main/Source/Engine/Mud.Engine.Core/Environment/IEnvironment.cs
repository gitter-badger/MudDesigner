//-----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines properties that are used across various environments such as worlds, realms and zones.
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        /// <value>
        /// The time from creation.
        /// </value>
        double TimeFromCreation { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// Initializes this instance. The environment time and states are set up.
        /// </summary>
        void Initialize();
    }
}

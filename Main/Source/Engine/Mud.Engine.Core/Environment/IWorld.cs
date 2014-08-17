//-----------------------------------------------------------------------
// <copyright file="IWorld.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a world 
    /// </summary>
    public interface IWorld : IEnvironment
    {
        /// <summary>
        /// Gets how many seconds have passed since the creation date.
        /// </summary>
        /// <value>
        /// The time from creation.
        /// </value>
        long TimeFromCreation { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        DateTime CreationDate { get; }

        /// <summary>
        /// Gets or sets hour many hours it takes in-game to complete 1 day.
        /// </summary>
        /// <value>
        /// The hours per day.
        /// </value>
        int HoursPerDay { get; set; }

        /// <summary>
        /// Gets or sets the hours ratio. If set to 4, it takes 4 in-game hours to equal 1 real-world hour.
        /// </summary>
        /// <value>
        /// The hours ratio.
        /// </value>
        float HoursRatio { get; set; }

        /// <summary>
        /// Gets or sets the realms in this world.
        /// </summary>
        /// <value>
        /// The realms.
        /// </value>
        IEnumerable<IRealm> Realms { get; set; }
    }
}

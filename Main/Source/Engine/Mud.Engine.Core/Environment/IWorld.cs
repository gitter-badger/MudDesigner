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
        event EventHandler<ITimeOfDayState> TimeOfDayChanged;

        /// <summary>
        /// Gets or sets the current time of day.
        /// </summary>
        /// <value>
        /// The current time of day.
        /// </value>
        ITimeOfDayState CurrentTimeOfDay { get; set; }

        /// <summary>
        /// Gets or sets a collection of states that can be used for the time of day.
        /// </summary>
        /// <value>
        /// The time of day states.
        /// </value>
        IEnumerable<ITimeOfDayState> TimeOfDayStates { get; set; }

        /// <summary>
        /// Gets or sets how many hours it takes in-game to complete 1 day for this world.
        /// </summary>
        /// <value>
        /// The hours per day.
        /// </value>
        int HoursPerDay { get; set; }

        /// <summary>
        /// Gets or sets the hours factor. If set to 4, it takes 4 in-game hours to equal 1 real-world hour.
        /// </summary>
        /// <value>
        /// The hours ratio.
        /// </value>
        double HoursFactor { get; set; }

        /// <summary>
        /// Gets or sets the realms in this world.
        /// </summary>
        /// <value>
        /// The realms.
        /// </value>
        IEnumerable<IRealm> Realms { get; set; }
    }
}

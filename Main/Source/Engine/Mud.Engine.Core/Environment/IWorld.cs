//-----------------------------------------------------------------------
// <copyright file="IWorld.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;

    /// <summary>
    /// Defines a world 
    /// </summary>
    public interface IWorld : IEnvironment, IDisposable
    {
        event EventHandler<TimeOfDayChangedEventArgs> TimeOfDayChanged;

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
        /// Gets the game time ratio used to convert real-world time to game-time.
        /// </summary>
        /// <value>
        /// The game time ratio.
        /// </value>
        double GameTimeRatio { get; }

        /// <summary>
        /// Gets or sets the realms in this world.
        /// </summary>
        /// <value>
        /// The realms.
        /// </value>
        IEnumerable<IRealm> Realms { get; set; }

        /// <summary>
        /// Initializes this instance. The environment time and states are set up.
        /// </summary>
        void Initialize(ITimeOfDayState initialState = null);

        /// <summary>
        /// Adds the given realm to world and initializes it.
        /// </summary>
        /// <param name="realm">The realm.</param>
        void AddRealmToWorld(IRealm realm);
    }
}

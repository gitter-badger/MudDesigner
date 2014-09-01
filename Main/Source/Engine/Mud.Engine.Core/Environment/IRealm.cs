﻿//-----------------------------------------------------------------------
// <copyright file="IRealm.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using Mud.Engine.Core.Engine;
    using Mud.Engine.Core.Environment.Time;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IRealm can be implemented to provide Realm support to an object.
    /// </summary>
    public interface IRealm : IEnvironment
    {

        /// <summary>
        /// Gets or sets the offset from the World's current time for the Realm
        /// </summary>
        /// <value>
        /// The time zone offset.
        /// </value>
        TimeOfDay TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the time of day for the Realm.
        /// </summary>
        /// <value>
        /// The time of day.
        /// </value>
        TimeOfDay CurrentTimeOfDay { get; set; }

        /// <summary>
        /// Gets or sets the zones within this Realm.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        IEnumerable<IZone> Zones { get; set; }

        /// <summary>
        /// Gets the number of zones.
        /// </summary>
        int NumberOfZones { get; }

        /// <summary>
        /// Gets the World that owns this realm..
        /// </summary>
        /// <value>
        /// The World.
        /// </value>
        IWorld World { get; }

        /// <summary>
        /// Initializes the realm. Weather states are set up and time zone offset validation.
        /// </summary>
        /// <param name="world">The world this realm belongs to.</param>
        /// <param name="worldTimeOfDay">The world time of day.</param>
        void Initialize(IWorld world, TimeOfDay worldTimeOfDay);

        /// <summary>
        /// Adds the given zone to this realm.
        /// </summary>
        /// <param name="zone">The zone.</param>
        void AddZoneToRealm(IZone zone);

        /// <summary>
        /// Updates the time.
        /// </summary>
        /// <param name="timeOfDay">The time of day.</param>
        void ApplyTimeZoneOffset(TimeOfDay timeOfDay);

        ITimeOfDayState GetCurrentTimeOfDayState();
    }
}

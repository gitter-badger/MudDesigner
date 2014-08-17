//-----------------------------------------------------------------------
// <copyright file="IRealm.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System.Collections.Generic;

    /// <summary>
    /// IRealm can be implemented to provide Realm support to an object.
    /// </summary>
    public interface IRealm : IEnvironment
    {
        /// <summary>
        /// Gets or sets the zones within this Realm.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        IEnumerable<IZone> Zones { get; set; }
    }
}

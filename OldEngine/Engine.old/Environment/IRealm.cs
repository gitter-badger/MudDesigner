//-----------------------------------------------------------------------
// <copyright file="IRealm.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Objects;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// Implements properties and methods for creating Realm Objects
    /// </summary>
    public interface IRealm: IEnvironment
    {
        /// <summary>
        /// Gets or Sets the collection of Zones that belong to this Realm.
        /// </summary>
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        List<IZone> Zones { get; set; }
        
        /// <summary>
        /// Gets or Sets a reference to the game World
        /// </summary>
        IWorld World { get; set; }

        /// <summary>
        /// Adds a Zone to the Realm. This is the preferred method for adding Zones. It ensures that a null Zone is never added to the collection
        /// as well as provides the ability to overwrite a Zone if it already exists.
        /// </summary>
        /// <param name="zone">The Zone that you want to add to the Realm</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Zone if it already exists within the collection</param>
        void AddZone(IZone zone, bool forceOverwrite);

        /// <summary>
        /// Adds a collection of Zones to the Realm, with the option to overwrite any Zones that already exists.
        /// </summary>
        /// <param name="zones">THe array of Zones you want to add</param>
        /// <param name="forceOverwrite">If true, it will overwrite the Zone if it already exists within the collection.</param>
        void AddZones(IZone[] zones, bool forceOverwrite);

        /// <summary>
        /// Gets the specified Zone and returns a reference to it for use.
        /// </summary>
        /// <param name="zoneName">The name of the Zone you want to get a reference for.</param>
        /// <returns></returns>
        IZone GetZone(string zoneName);

        /// <summary>
        /// Gets the zones.
        /// </summary>
        /// <returns></returns>
        IZone[] GetZones();

        /// <summary>
        /// Removes the specified Zone from the Realms collection of Zones.
        /// </summary>
        /// <param name="zone">The Zone you want to remove.</param>
        void RemoveZone(IZone zone);
    }
}
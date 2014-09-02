//-----------------------------------------------------------------------
// <copyright file="IWorldRepository.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Repositories.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Environment;

    /// <summary>
    /// Provides methods for fetching and restoring objects from the data store.
    /// </summary>
    public interface IWorldRepository
    {
        /// <summary>
        /// Gets the worlds from the data store.
        /// </summary>
        /// <param name="includeAllChildrenObjects">if set to <c>true</c> then all of the Realms, Zones, Rooms and any GameObjects associated with them will be restored.</param>
        /// <returns>Returns a collection of IWorld implementations.</returns>
        Task<IEnumerable<IWorld>> GetAllWorlds(bool includeAllChildrenObjects = false);

        /// <summary>
        /// Gets the world for the given realm.
        /// </summary>
        /// <param name="realm">The realm.</param>
        /// <param name="includeAllChildrenObjects">if set to <c>true</c> [include all children objects].</param>
        /// <returns>
        /// Returns the World associated with the supplied Realm.
        /// </returns>
        Task<IWorld> GetWorldForRealm(IRealm realm, bool includeAllChildrenObjects = false);

        /// <summary>
        /// Gets the world by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeAllChildrenObjects">if set to <c>true</c> [include all children objects].</param>
        /// <returns>
        /// Returns the World with the given Id.
        /// </returns>
        Task<IWorld> GetWorldById(Guid id, bool includeAllChildrenObjects = false);

        /// <summary>
        /// Saves the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>Returns the Task associated with the async call.</returns>
        Task SaveWorld(IWorld world);

        /// <summary>
        /// Saves all worlds.
        /// </summary>
        /// <param name="worlds">The worlds.</param>
        /// <returns>Returns the Task associated with the async call.</returns>
        Task SaveAllWorlds(IEnumerable<IWorld> worlds);

        /// <summary>
        /// Deletes the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>Returns the Task associated with the async call.</returns>
        Task DeleteWorld(IWorld world);
    }
}

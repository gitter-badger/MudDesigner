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
using Mud.Engine.Core.Engine;

    /// <summary>
    /// Provides methods for fetching and restoring objects from the data store.
    /// </summary>
    public interface IWorldRepository
    {
        /// <summary>
        /// Gets the worlds from the data store.
        /// </summary>
        /// <param name="HydrateWorlds">if set to <c>true</c> all of the worlds will have their children objects restored.</param>
        /// <param name="context">The data store context.</param>
        /// <returns>
        /// Returns a collection of IWorld implementations.
        /// </returns>
        Task<IEnumerable<IWorld>> GetAllWorlds(bool HydrateWorlds = false, IDataStoreContext context = null);

        /// <summary>
        /// Gets the world for the given realm.
        /// </summary>
        /// <param name="realm">The realm.</param>
        /// <param name="HydrateWorld">if set to <c>true</c> the world will have its children objects restored.</param>
        /// <param name="context">The data store context.</param>
        /// <returns>
        /// Returns the World associated with the supplied Realm.
        /// </returns>
        Task<IWorld> GetWorldForRealm(IRealm realm, bool HydrateWorld = false, IDataStoreContext context = null);

        /// <summary>
        /// Gets the world by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="HydrateWorld">if set to <c>true</c> the world will have its children objects restored.</param>
        /// <param name="context">The data store context.</param>
        /// <returns>
        /// Returns the World with the given Id.
        /// </returns>
        Task<IWorld> GetWorldById(Guid id, bool HydrateWorld = false, IDataStoreContext context = null);

        /// <summary>
        /// Saves the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="context">The data store context.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        Task SaveWorld(IWorld world, IDataStoreContext context = null);

        /// <summary>
        /// Saves all worlds.
        /// </summary>
        /// <param name="worlds">The worlds.</param>
        /// <param name="context">The data store context.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        Task SaveAllWorlds(IEnumerable<IWorld> worlds, IDataStoreContext context = null);

        /// <summary>
        /// Deletes the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="context">The data store context.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        Task DeleteWorld(IWorld world, IDataStoreContext context = null);
    }
}

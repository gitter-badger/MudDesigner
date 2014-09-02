//-----------------------------------------------------------------------
// <copyright file="IWorldService.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Services.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Environment;

    /// <summary>
    /// Provides a contract to objects that want to expose methods for fetching World data from a data store.
    /// </summary>
    public interface IWorldService
    {
        /// <summary>
        /// Gets the worlds from the data store.
        /// </summary>
        /// <returns>
        /// Returns a collection of IWorld implementations.
        /// </returns>
        Task<IEnumerable<IWorld>> GetAllWorlds();

        /// <summary>
        /// Gets the world for the given realm.
        /// </summary>
        /// <param name="realm">The realm.</param>
        /// <returns>
        /// Returns the World associated with the supplied Realm.
        /// </returns>
        Task<IWorld> GetWorldForRealm(IRealm realm);

        /// <summary>
        /// Gets the world by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns the World with the given Id.</returns>
        Task<IWorld> GetWorldById(Guid id);

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

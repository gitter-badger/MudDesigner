//-----------------------------------------------------------------------
// <copyright file="WorldRepository.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Repositories.Engine.DefaultDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Environment;
    using Mud.Repositories.Shared;
    using Mud.Services.Shared;

    /// <summary>
    /// Provides methods for fetching Worlds from the data store.
    /// </summary>
    public class WorldRepository : IWorldRepository
    {
        /// <summary>
        /// The service
        /// </summary>
        private IWorldService service;

        /// <summary>
        /// The world cache
        /// </summary>
        private IList<IWorld> worldCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldRepository"/> class.
        /// </summary>
        /// <param name="worldService">The world service.</param>
        public WorldRepository(IWorldService worldService)
        {
            this.service = worldService;
            this.worldCache = new List<IWorld>();
        }

        /// <summary>
        /// Gets the worlds from the data store.
        /// This will refresh any cached data when invoked.
        /// </summary>
        /// <param name="includeAllChildrenObjects">if set to <c>true</c> then all of the Realms, Zones, Rooms and any GameObjects associated with them will be restored.</param>
        /// <returns>
        /// Returns a collection of IWorld implementations.
        /// </returns>
        public async Task<IEnumerable<IWorld>> GetAllWorlds(bool includeAllChildrenObjects = false)
        {
            IEnumerable<IWorld> worlds = await this.service.GetAllWorlds();
            this.worldCache = new List<IWorld>(worlds);

            return worlds;
        }

        /// <summary>
        /// Gets the world for the given realm. If the world has previously been fetched, a cached version of it will be returned.
        /// </summary>
        /// <param name="realm">The realm.</param>
        /// <param name="includeAllChildrenObjects">if set to <c>true</c> [include all children objects].</param>
        /// <returns>
        /// Returns the World associated with the supplied Realm.
        /// </returns>
        public async Task<IWorld> GetWorldForRealm(IRealm realm, bool includeAllChildrenObjects = false)
        {
            if (this.worldCache.Any(w => w.Realms.Any(r => r.Id == realm.Id)))
            {
                return this.worldCache.FirstOrDefault(w => w.Realms.Any(r => r.Id == realm.Id));
            }
            else
            {
                IWorld world = await this.service.GetWorldForRealm(realm);
                return world;
            }
        }

        /// <summary>
        /// Gets the world by identifier. If the world has previously been fetched, a cached version of it will be returned.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeAllChildrenObjects">providing a true value will return a World with all of it's children fetched from the data store as well.</param>
        /// <returns>
        /// Returns the World with the given Id.
        /// </returns>
        public async Task<IWorld> GetWorldById(Guid id, bool includeAllChildrenObjects = false)
        {
            if (this.worldCache.Any(w => w.Id == id))
            {
                return this.worldCache.FirstOrDefault(w => w.Id == id);
            }
            else
            {
                return await this.service.GetWorldById(id);
            }
        }

        /// <summary>
        /// Saves the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        /// <exception cref="System.NotImplementedException">This method is not implemented yet.</exception>
        public Task SaveWorld(IWorld world)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves all worlds.
        /// </summary>
        /// <param name="worlds">The worlds.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        /// <exception cref="System.NotImplementedException">This method is not implemented yet.</exception>
        public Task SaveAllWorlds(IEnumerable<IWorld> worlds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        /// <exception cref="System.NotImplementedException">This method is not implemented yet.</exception>
        public Task DeleteWorld(IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}

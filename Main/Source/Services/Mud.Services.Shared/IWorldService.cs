using Mud.Engine.Core.Environment;
using Mud.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Services.Shared
{
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
        /// <returns>
        /// Returns the World associated with the supplied Realm.
        /// </returns>
        Task<IWorld> GetWorldForRealm();

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

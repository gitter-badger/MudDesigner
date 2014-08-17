//-----------------------------------------------------------------------
// <copyright file="IWorldRepository.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Repositories.Shared
{
    using System.Collections.Generic;
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
        Task<IEnumerable<IWorld>> GetWorlds(bool includeAllChildrenObjects = false);
    }
}

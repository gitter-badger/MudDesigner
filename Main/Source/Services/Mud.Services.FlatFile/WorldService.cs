//-----------------------------------------------------------------------
// <copyright file="WorldService.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Services.FlatFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Engine;
    using Mud.Engine.Core.Environment;
    using Mud.Services.Shared;

    /// <summary>
    /// Fetches World data from standard text files.
    /// </summary>
    public class WorldService : IWorldService
    {
        /// <summary>
        /// The world path
        /// </summary>
        private string worldPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldService"/> class.
        /// </summary>
        public WorldService()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyFilename = Path.GetFileName(assembly.Location);
            string path = assembly.Location.Substring(0, assembly.Location.Length - assemblyFilename.Length);

            this.worldPath = Path.Combine(path, "Worlds");

            if (!Directory.Exists(this.worldPath))
            {
                Directory.CreateDirectory(this.worldPath);
            }
        }

        /// <summary>
        /// Gets the worlds from the data store.
        /// </summary>
        /// <returns>
        /// Returns a collection of IWorld implementations.
        /// </returns>
        /// <exception cref="System.InvalidCastException">Unable to restore the Id for the world.</exception>
        public async Task<IEnumerable<IWorld>> GetAllWorlds()
        {
            string[] files = Directory.GetFiles(this.worldPath, "*.txt");
            var worlds = new List<DefaultWorld>();

            foreach (string file in files)
            {
                List<string> lines = await Task.Run(() => File.ReadAllLines(file).ToList());
                var world = new DefaultWorld();

                // Restore the name
                if (lines.Any(line => line.StartsWith(world.GetPropertyName(p => p.Name))))
                {
                    world.Name = lines.FirstOrDefault(line => line.StartsWith(world.GetPropertyName(p => p.Name)));
                }
                
                // Restore the id
                if (lines.Any(line => line.StartsWith(world.GetPropertyName(p => p.Id))))
                {
                    string idAsString = lines.FirstOrDefault(line => line.StartsWith(world.GetPropertyName(p => p.Id)));
                    Guid id = Guid.Empty;
                    if (Guid.TryParse(idAsString, out id))
                    {
                        world.Id = id;
                    }
                    else
                    {
                        throw new InvalidCastException("Unable to restore the Id for the world.");
                    }
                }

                worlds.Add(world);
            }

            return worlds;
        }

        /// <summary>
        /// Gets the world for the given realm.
        /// </summary>
        /// <param name="realm">The realm that is associated with the world you wish to fetch.</param>
        /// <returns>
        /// Returns the World associated with the supplied Realm.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Method has not been implemented.</exception>
        public Task<IWorld> GetWorldForRealm(IRealm realm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the world by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns the World with the given Id.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Method has not been implemented.</exception>
        public Task<IWorld> GetWorldById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>
        /// Returns the Task associated with the async call.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Method has not been implemented.</exception>
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
        /// <exception cref="System.NotImplementedException">Method has not been implemented.</exception>
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
        /// <exception cref="System.NotImplementedException">Method has not been implemented.</exception>
        public Task DeleteWorld(IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}

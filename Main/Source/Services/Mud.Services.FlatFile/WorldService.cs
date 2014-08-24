using Mud.Engine.Core.Environment;
using Mud.Engine.Core.Engine;
using Mud.Services.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Services.FlatFile
{
    public class WorldService : IWorldService
    {
        private string worldPath;

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

            foreach(string file in files)
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

        public Task<IWorld> GetWorldForRealm(IRealm realm)
        {
            throw new NotImplementedException();
        }

        public Task<IWorld> GetWorldById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveWorld(IWorld world)
        {
            throw new NotImplementedException();
        }

        public Task SaveAllWorlds(IEnumerable<IWorld> worlds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorld(IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}

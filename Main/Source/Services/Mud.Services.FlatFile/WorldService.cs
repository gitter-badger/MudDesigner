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

        public async Task<IEnumerable<IWorld>> GetAllWorlds()
        {
            string[] files = Directory.GetFiles(this.worldPath, "*.txt");
            var worlds = new List<DefaultWorld>();

            foreach(string file in files)
            {
                List<string> lines = await Task.Run(() => File.ReadAllLines(file).ToList());
                var world = new DefaultWorld();

                if (lines.Any(l => l.Equals(world.GetPropertyName(p => p.Name))))
                {
                    world.Name = lines.FirstOrDefault(line => line.Equals(world.GetPropertyName(p => p.Name)));
                }

                worlds.Add(world);
            }

            return worlds;
        }

        public Task<IWorld> GetWorldForRealm()
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

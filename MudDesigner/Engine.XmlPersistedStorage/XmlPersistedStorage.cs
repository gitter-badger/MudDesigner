using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Environment;

namespace Engine.XmlPersistedStorage
{
    public class XmlPersistedStorage : IPersistedStorage
    {
        public string StoragePath { get; set; }

        public void InitializeStorage()
        {
            this.StoragePath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        }

        public string GetStoragePath<T>(T item = null) where T : class
        {
            throw new NotImplementedException();
        }

        public T Save<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Save<T>(T[] items) where T : class
        {
            throw new NotImplementedException();
        }

        public T Load<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Load<T>() where T : class
        {
            // Need a EngineFactory resolver. Call EngineFactory.GetFactoryForType<T>() to return a factory that can be used with <T>.
            // Then we can call resolvedFactory.GetObjects<T>() to instance all of the Types in the engine.
            // Then start scanning the HDD and deserializing each file into the Type associated with them.
            List<IWorld> worldTypes = WorldFactory.GetWorlds();

            return worldTypes as IEnumerable<T>;
        }

        public void Delete<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T[] items) where T : class
        {
            throw new NotImplementedException();
        }
    }
}

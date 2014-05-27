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
        // Attempt to find a factory that supports <T>
        Type factoryType = EngineFactory.FindFactory<T>();

        // Create an instance of the Factory found, and provide it with <T> as its constraint.
        IFactory<T> factoryInstance = Activator.CreateInstance(factoryType.MakeGenericType(typeof(T))) as IFactory<T>;

        // Instance the objects associated with <T>
        var objectsToLoad = factoryInstance.GetObjects();

        string pathToObjects = this.GetStoragePath<T>();

        if (!Directory.Exists(pathToObjects))
        {
            return new List<T>() as IEnumerable<T>;
        }

        objectsToLoad.ForEach(item =>
        {
            
        });

        // Return our collection of <T>.
        return objectsToLoad as IEnumerable<T>;
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

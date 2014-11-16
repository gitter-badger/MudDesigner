using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Environment;

namespace MudEngine.Engine.XmlPersistedStorage
{
    [PlatformSupport("Windows Vista", 6, 0)]
    [PlatformSupport("Windows 7", 6, 1)]
    [PlatformSupport("Windows 8", 6, 2)]
    [PlatformSupport("Windows 8.1", 6, 3)]
    [DisplayName("Xml Storage Container")]
    public class XmlPersistedStorage : IPersistedStorage
    {
        /// <summary>
        /// The storage path
        /// </summary>
        private string storagePath;

        public string StoragePath
        {
            get
            {
                if (string.IsNullOrEmpty(this.storagePath))
                {
                    this.storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                }

                return this.storagePath;
            }

            set
            {
                this.storagePath = value;
            }
        }

        public void InitializeStorage()
        {
        }

        /// <summary>
        /// Gets the storage path.
        /// </summary>
        /// <typeparam name="T">The type that you want to find the storage path for.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>Returns the path that will be used to save the supplied item.</returns>
        public string GetStoragePath<T>(T item = null) where T : class
        {
            string fullPath = string.Empty;

            if (item == null)
            {
                fullPath = Path.Combine(this.StoragePath,typeof(T).Name);
            }
            else
            {
                // The itemType is used for determining what sub-folder to store the object in.
                Type itemType = typeof(T);
                PropertyInfo property = this.GetPropertyWithAttribute<UniqueStorageIdentifierAttribute>(item);
                string result = this.GetUniqueIdentifierFromProperty<T>(property, item);
                
                if (string.IsNullOrEmpty(result))
                {
                    throw new NullReferenceException(
                        string.Format(
                            "The {0} item does not have a property defined with the {1} property attribute."
                            + "\nThis should be a uniquely identifying property, such as a Guid or integer based Id.",
                            itemType.Name, typeof(UniqueStorageIdentifierAttribute).Name));
                }
                else
                {
                    fullPath = Path.Combine(this.StoragePath, itemType.Name, string.Format("{0}.{1}", result, ".xml"));
                }
            }

            // RootPath\ItemType
            return fullPath;
        }

        public T Save<T>(T item) where T : class, new()
        {
            string savePath = this.GetStoragePath<T>();
            string fullFilePath = this.GetStoragePath<T>(item);

            // Prepare the directory.
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            else if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }

            try
            {
                // Serialize the object to disk.
                var serializer = new XmlSerializer(typeof(T), "MudEngine");
                TextWriter writer = new StreamWriter(fullFilePath);
                serializer.Serialize(writer, item);
                writer.Close();
            }
            catch (XmlException)
            {
                throw;
            }

            // When saving to Xml, there are no adjustments that are made to the object.
            // We just return a unmodified object.
            return item;
        }

        public IEnumerable<T> Save<T>(T[] items) where T : class, new()
        {
            items.AsParallel().ForAll(item => this.Save<T>(item));
            return items;
        }

        public T Load<T>(T item) where T : class, new()
        {
            // Attempt to find a factory that supports <T>
            Type factoryType = EngineFactory.FindFactory<T>();

            // Create an instance of the Factory found, and provide it with <T> as its constraint.
            IFactory<T> factoryInstance = Activator.CreateInstance(factoryType.MakeGenericType(typeof(T))) as IFactory<T>;

            // Instance the objects associated with <T>
            List<T> objectsToLoad = factoryInstance.GetObjects();

            return this.RestoreItem<T>(item, this.GetStoragePath<T>(item));
        }

        public IEnumerable<T> Load<T>() where T : class, new()
        {
            // Attempt to find a factory that supports <T>
            Type factoryType = EngineFactory.FindFactory<T>();

            // Create an instance of the Factory found, and provide it with <T> as its constraint.
            IFactory<T> factoryInstance = Activator.CreateInstance(factoryType.MakeGenericType(typeof(T))) as IFactory<T>;

            // Instance the objects associated with <T>
            List<T> objectsToLoad = factoryInstance.GetObjects();

            var restoredItems = new List<T>();

            objectsToLoad.AsParallel().ForAll(item =>
            {
                T loadedItem = this.RestoreItem<T>(item, this.GetStoragePath<T>(item));

                if (loadedItem != null)
                {
                    restoredItems.Add(loadedItem);
                }
            });

            // Return our collection of <T>.
            return restoredItems;
        }

        public void Delete<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T[] items) where T : class
        {
            throw new NotImplementedException();
        }

        private T RestoreItem<T>(T item, string filePath) where T : class, new()
        {
            if (!Directory.Exists(Path.GetFullPath(filePath)))
            {
                return null;
            }

            if (!File.Exists(filePath))
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(T));
            var fileStream = new FileStream(filePath, FileMode.Open);

            item = (T)serializer.Deserialize(fileStream);

            return item;
        }

        private PropertyInfo GetPropertyWithAttribute<T>(object item) where T : Attribute
        {
            if (!Attribute.IsDefined(item.GetType(), typeof(UniqueStorageIdentifierAttribute)))
            {
                return null;
            }

            // Find the property specified by the attribute.
            UniqueStorageIdentifierAttribute attribute = item.GetType().GetCustomAttribute<UniqueStorageIdentifierAttribute>();
            var result = item.GetType().GetProperties().FirstOrDefault(prop => prop.Name == attribute.PropertyName);
            return result;
        }

        /// <summary>
        /// Gets the unique identifier from the property associated with the supplied object.
        /// An item may have a property designated as the unique Id provided it is either an integer, string or guid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private string GetUniqueIdentifierFromProperty<T>(PropertyInfo property, T item) where T : class
        {
            if (property.PropertyType == typeof(int))
            {
                int value;
                int.TryParse(property.GetValue(item).ToString(), out value);

                return value.ToString();
            }
            else if (property.PropertyType == typeof(string))
            {
                return property.GetValue(item).ToString();
            }
            else if (property.PropertyType == typeof(Guid))
            {
                Guid result;
                Guid.TryParse(property.GetValue(item).ToString(), out result);
                
                return result.ToString();
            }

            return null;
        }
    }
}

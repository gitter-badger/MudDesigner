//-----------------------------------------------------------------------
// <copyright file="EngineXmlStorage.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using MudEngine.Engine.GameObjects;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// The core storage class for the Mud engine. Serializes objects into Xml.
    /// IPersistedStorage objects are not inheritable. 
    /// Each custom storage object must fully implement IPersistedStorage themselves
    /// in order to ensure data integrity.
    /// </summary>
    public sealed class EngineXmlStorage : IPersistedStorage
    {
        /// <summary>
        /// Gets or sets the root path for the data storage.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        public string FileExtension
        {
            get
            {
                return ".xml";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineXmlStorage"/> class.
        /// </summary>
        public EngineXmlStorage()
        {
            this.RootPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        }

        /// <summary>
        /// Initializes the Persisted Storage.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void InitializeStorage()
        {
            // Makes sure our save path exists.
            if (!Directory.Exists(this.RootPath))
            {
                Directory.CreateDirectory(this.RootPath);
            }
        }

        /// <summary>
        /// Gets the storage path for the supplied item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the full path for the item type.
        /// </returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public string GetStoragePath<T>(T item = null) where T : class
        {            
            // The itemType is used for determining what sub-folder to store the object in.
            Type itemType = typeof(T);
            PropertyInfo property = this.FindAttribute<StorageFilenameAttribute>(itemType);

            // If the object does not have any properties with the StorageFilename attribute
            // we throw an exception. The attribute is required to determine the filename.
            if (property == null)
            {
                throw new NullReferenceException(
                    string.Format(
                        "The {0} item does not have a property defined with the {1} property attribute."
                        + "\nThis should be a uniquely identifying property, such as a Name or Id.",
                        itemType.Name, typeof(StorageFilenameAttribute).Name));
            }

            // RootPath\ItemType
            string savePath = Path.Combine(this.RootPath, itemType.Name);
            // Item.xml - Only if item is not null. Otherwise fetching the value throws an exception.
            string filename = (item == null) ? 
                string.Empty : 
                string.Format("{0}{1}", property.GetValue(item).ToString(), this.FileExtension);
            // RootPath\ItemType\Item.xml
            string filePath = Path.Combine(savePath, filename);

            return (item == null) ? savePath : filePath;
        }

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <typeparam name="T">The Type that the item is of</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the item along with any updates, such as primary key information.
        /// </returns>
        public T Save<T>(T item) where T : class
        {
            // The itemType is used for determining what sub-folder to store the object in.
            Type itemType = typeof(T);
            PropertyInfo property = this.FindAttribute<StorageFilenameAttribute>(itemType);
            var val = property.GetValue(item);

            // If the object does not have any properties with the StorageFilename attribute
            // we throw an exception. The attribute is required to determine the filename.
            if (property == null || property.GetValue(item) == null)
            {
                throw new NullReferenceException(
                    string.Format(
                        "The {0} item does not have a property defined with the {1} property attribute." 
                        + "\nThis should be a uniquely identifying property, such as a Name or Id.",
                        itemType.Name, typeof(StorageFilenameAttribute).Name));
            }

            // RootPath\ItemType
            string savePath = Path.Combine(this.RootPath, itemType.Name);
            // Item.xml
            string filename = string.Format("{0}.xml", property.GetValue(item).ToString());
            // RootPath\ItemType\Item.xml
            string filePath = Path.Combine(savePath, filename);

            // Prepare the directory.
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            else if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            try
            {
                // Serialize the object to disk.
                var serializer = new XmlSerializer(itemType, "MudEngine");
                TextWriter writer = new StreamWriter(filePath);
                serializer.Serialize(writer, item);
                writer.Close();
            }
            catch (Exception)
            {
                throw;
            }

            // When saving to Xml, there are no adjustments that are made to the object.
            // We just return a unmodified object.
            return item;
        }

        /// <summary>
        /// Saves all of the items in the collection provided.
        /// </summary>
        /// <typeparam name="T">The type that the items are of.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// Returns a updated collection of items.
        /// </returns>
        public IEnumerable<T> Save<T>(T[] items) where T : class
        {
            // Save each item in the collection.
            foreach(T item in items.AsParallel().AsOrdered())
            {
                var currentItem = item;
                try
                {
                    // Since we are not modifying the object, we just return the item.
                    // This is cheaper than casting the return value of Save back to T 
                    // over each iteration and adding to a new collection.
                    this.Save<T>(currentItem);
                }
                catch(Exception)
                {
                    throw;
                }
            }

            return items;
        }

        /// <summary>
        /// Loads the specified item.
        /// </summary>
        /// <typeparam name="T">The type that the item is of.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the fully loaded item.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public T Load<T>(T item) where T : class, new()
        {
            // The itemType is used for determining what sub-folder to store the object in.
            Type itemType = typeof(T);
            PropertyInfo property = this.FindAttribute<StorageFilenameAttribute>(itemType);
            var val = property.GetValue(item);

            // If the object does not have any properties with the StorageFilename attribute
            // we throw an exception. The attribute is required to determine the filename.
            if (property == null || property.GetValue(item) == null)
            {
                throw new NullReferenceException(
                    string.Format(
                        "The {0} item does not have a property defined with the {1} property attribute."
                        + "\nThis should be a uniquely identifying property, such as a Name or Id.",
                        itemType.Name, typeof(StorageFilenameAttribute).Name));
            }

            // RootPath\ItemType
            string savePath = Path.Combine(this.RootPath, itemType.Name);
            // Item.xml
            string filename = string.Format("{0}.xml", property.GetValue(item).ToString());
            // RootPath\ItemType\Item.xml
            string filePath = Path.Combine(savePath, filename);

            // Prepare the directory.
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            // The object we deserialize in to.
            T newItem = null;
            try
            {
                // Serialize the object to disk.
                var serializer = new XmlSerializer(itemType, "MudEngine");
                TextReader writer = new StreamReader(filePath);
                newItem = (T)serializer.Deserialize(writer);
                writer.Close();
            }
            catch (Exception)
            {
                throw;
            }

            // When saving to Xml, there are no adjustments that are made to the object.
            // We just return a unmodified object.
            return newItem;
        }

        /// <summary>
        /// Loads all items of type T..
        /// </summary>
        /// <typeparam name="T">The type that the items are of.</typeparam>
        /// <returns>
        /// Returns a collection of all items matching T.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<T> Load<T>() where T : class, new()
        {
            // The itemType is used for determining what sub-folder to store the object in.
            Type itemType = typeof(T);

            // RootPath\ItemType
            string savePath = Path.Combine(this.RootPath, itemType.Name);

            // Prepare the directory.
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var itemsLoaded = new List<T>();
            try
            {
                // Serialize the object to disk.
                foreach (string file in Directory.GetFiles(savePath).AsParallel())
                {
                    var serializer = new XmlSerializer(itemType, "MudEngine");
                    TextReader writer = new StreamReader(file);
                    T currentItem = (T)serializer.Deserialize(writer);
                    writer.Close();

                    itemsLoaded.Add(currentItem);
                }
            }
            catch (Exception)
            {
                throw;
            }

            // When saving to Xml, there are no adjustments that are made to the object.
            // We just return a unmodified object.
            return itemsLoaded;
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T">The type that the item is of.</typeparam>
        /// <param name="item">The item.</param>
        public void Delete<T>(T item) where T : class
        {
            string filePath = this.GetStoragePath<T>(item);

            // If the file exists, we delete.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Deletes the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        public void Delete<T>(T[] items) where T : class
        {
            // TODO: Should do a performance check. 
            // If items.Count < n than perform a traditional foreach loop.
            // Spawning threads to delete a handful of files is more taxing than a sequential delete.
            // There's only benefits for a large number of files.
            Parallel.ForEach<T>(items, (item, loopState) =>
            {
                string file = this.GetStoragePath<T>(item);

                // If a file exists, we delete it.
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            });
        }

        private PropertyInfo FindAttribute<T>(Type itemType) where T : Attribute
        {
            PropertyInfo[] properties = itemType.GetProperties()
                .Where(property => Attribute.IsDefined(property, typeof(T)))
                .ToArray();

            // if we have any properties that contains the attribute specified
            // we return it.
            if (properties.Any())
            {
                return properties.FirstOrDefault();
            }
            
            // If none are found, then we hit the base type up next.
            // Crawl up the tree until we find what we want.
            return (itemType.BaseType != null) ? FindAttribute<T>(itemType.BaseType) : null;
        }
    }
}

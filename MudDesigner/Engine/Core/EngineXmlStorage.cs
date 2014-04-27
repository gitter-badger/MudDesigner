//-----------------------------------------------------------------------
// <copyright file="EngineXmlStorage.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>
        /// Returns the item along with any updates, such as primary key information.
        /// </returns>
        public IGameObject Save(IGameObject item, Type itemType = null)
        {
            // Get the item's Type and save path.
            if (itemType == null)
            {
                itemType = item.GetType();
            }
            string savePath = Path.Combine(this.RootPath, itemType.Name);

            var serializer = new XmlSerializer(itemType, "MudEngine");
            TextWriter writer = new StreamWriter(savePath);

            try
            {
                serializer.Serialize(writer, item);
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
        /// Saves the specified item.
        /// </summary>
        /// <typeparam name="T">The Type that the item is of</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the item along with any updates, such as primary key information.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public T Save<T>(T item) where T : IGameObject
        {
            try
            {
                this.Save(item, typeof(T));
            }
            catch (Exception)
            {
                throw;
            }

            // Since we are not modifying the object, we just return the item.
            // This is cheaper than casting the return value of Save back to T.
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
        public IEnumerable<T> SaveAll<T>(T[] items) where T : IGameObject
        {
            // Save each item in the collection.
            foreach(T item in items.AsParallel().AsOrdered())
            {
                try
                {
                    // Since we are not modifying the object, we just return the item.
                    // This is cheaper than casting the return value of Save back to T 
                    // over each iteration and adding to a new collection.
                    this.Save(item, typeof(T));
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
        public T Load<T>(T item) where T : IGameObject, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads all items of type T..
        /// </summary>
        /// <typeparam name="T">The type that the items are of.</typeparam>
        /// <returns>
        /// Returns a collection of all items matching T.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<T> Load<T>() where T : IGameObject, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns true if the item was deleted.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Delete(IGameObject item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T">The type that the item is of.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns true if the item was deleted.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Delete<T>(T item) where T : IGameObject
        {
            throw new NotImplementedException();
        }
    }
}

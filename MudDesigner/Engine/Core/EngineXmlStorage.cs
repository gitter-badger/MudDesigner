//-----------------------------------------------------------------------
// <copyright file="EngineXmlStorage.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// The core storage class for the Mud engine. Serializes objects into Xml.
    /// </summary>
    public class EngineXmlStorage : IPersistedStorage
    {
        /// <summary>
        /// Gets or sets the root path for the data storage.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Initializes the Persisted Storage.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void InitializeStorage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the item along with any updates, such as primary key information.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IGameObject Save(IGameObject item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves all of the items in the collection provided.
        /// </summary>
        /// <typeparam name="T">The type that the items are of.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// Returns a updated collection of items.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<T> SaveAll<T>(T[] items) where T : IGameObject
        {
            throw new NotImplementedException();
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

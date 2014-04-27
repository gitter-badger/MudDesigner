//-----------------------------------------------------------------------
// <copyright file="IPersistedStorage.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using MudEngine.Engine.GameObjects;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// IPersistedStorage provides a contract for objects wanting to provide a source of data for the engine.
    /// Objects implementing IPersistedStorage can be used to read/write data from persisted storage on disk or any other service.
    /// </summary>
    public interface IPersistedStorage
    {
        /// <summary>
        /// Gets or sets the root path for the data storage.
        /// </summary>
        string RootPath { get; set; }

        /// <summary>
        /// Initializes the Persisted Storage.
        /// </summary>
        /// <returns>Returns true if the storage is valid and ready for use.</returns>
        void InitializeStorage();

        /// <summary>
        /// Gets the storage path for the supplied item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the full path for the item type.
        /// </returns>
        string GetStoragePath<T>(T item = null) where T : class;

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <typeparam name="T">The Type that the item is of</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="propertyForFilename">The property used to base the filename on.</param>
        /// <returns>
        /// Returns the item along with any updates, such as primary key information.
        /// </returns>
        T Save<T>(T item) where T : class;

        /// <summary>
        /// Saves all of the items in the collection provided.
        /// </summary>
        /// <typeparam name="T">The type that the items are of.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>Returns a updated collection of items.</returns>
        IEnumerable<T> Save<T>(T[] items) where T : class;

        /// <summary>
        /// Loads the specified item.
        /// </summary>
        /// <typeparam name="T">The type that the item is of.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>Returns the fully loaded item.</returns>
        T Load<T>(T item) where T : class, new();

        /// <summary>
        /// Loads all items of type T..
        /// </summary>
        /// <typeparam name="T">The type that the items are of.</typeparam>
        /// <returns>Returns a collection of all items matching T.</returns>
        IEnumerable<T> Load<T>() where T : class, new();

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T">The type that the item is of.</typeparam>
        /// <param name="item">The item.</param>
        void Delete<T>(T item) where T : class;

        /// <summary>
        /// Deletes the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        void Delete<T>(T[] items) where T : class;
    }
}

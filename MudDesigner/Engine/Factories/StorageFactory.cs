//-----------------------------------------------------------------------
// <copyright file="GameFactory.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// Provides a means to fetch objects that implement the IPersistedStorage interface
    /// </summary>
    public static class StorageFactory
    {
        /// <summary>
        /// Gets or sets the default storage.
        /// </summary>
        /// <value>
        /// The default storage.
        /// </value>
        public static IPersistedStorage DefaultStorage { get; set; }

        /// <summary>
        /// Gets a collection objects implementing IPersistedStorage.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IPersistedStorage</returns>
        public static List<IPersistedStorage> GetStorageCollection(Assembly[] fromAssemblies = null)
        {
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            if (fromAssemblies == null)
            {
                fromAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IPersistedStorage
            foreach (Assembly assembly in fromAssemblies)
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(IPersistedStorage).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            return new List<IPersistedStorage>(
                (from type in types
                 select Activator.CreateInstance(type) as IPersistedStorage));
        }

        /// <summary>
        /// Gets the Storage specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IPersistedStorage that you want to find</typeparam>
        /// <returns>Returns the Storage specified.</returns>
        public static IPersistedStorage GetStorage<T>(Assembly[] fromAssemblies = null) where T : IPersistedStorage
        {
            // If the DefaultStorage has not been set, we will set it the first time a specific Type is passed.
            // It is assumed since a specific Type is provided, it will be re-used.
            if (StorageFactory.DefaultStorage == null)
            {
                StorageFactory.DefaultStorage = default(T);
            }

            // This isn't the most efficient.
            // Considering that GetStorageCollection should really only be returning a few (1-5?) IPersistedStorage objects
            // Filtering the list a second time (filtered once in GetStorageCollection) shouldn't hurt much.
            // If users start loading dozens of IPersistedStorage (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return StorageFactory.GetStorageCollection(fromAssemblies).FirstOrDefault(storage => storage.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default storage.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance matching the default storage object.</returns>
        public static IPersistedStorage GetDefaultStorage(Assembly[] fromAssemblies = null)
        {
            if (StorageFactory.DefaultStorage == null)
            {
                throw new NullReferenceException("DefaultStorage must not be null.");
            }

            // This isn't the most efficient.
            // Considering that GetStorageCollection should really only be returning a few (1-5?) IPersistedStorage objects
            // Filtering the list a second time (filtered once in GetStorageCollection) shouldn't hurt much.
            // If users start loading dozens of IPersistedStorage (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return StorageFactory.GetStorageCollection(fromAssemblies).FirstOrDefault(storage => storage.GetType() == StorageFactory.DefaultStorage.GetType());
        }
    }
}

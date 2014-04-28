//-----------------------------------------------------------------------
// <copyright file="PersistedPersistedStorageFactory.cs" company="AllocateThis!">
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
    /// The PersistedStorageFactory provides access to all of the storage containers that are available for the runtime.
    /// By implementing IPersistedStorage, any given runtime can have it's own storage container for data persistance.
    /// </summary>
    public static class PersistedStorageFactory
    {
        /// <summary>
        /// Gets or sets the default storage.
        /// </summary>
        public static IPersistedStorage DefaultStorage { get; set; }

        /// <summary>
        /// Searches all of the non-.NET Framework assemblies to locate any object that implements the IPersistedStorage interface.
        /// All objects that are found are checked to see if they are compatible with the current OS and returned in an array if they are.
        /// This method only returns an array of Types that can be used, uninstantiated.
        /// </summary>
        /// <param name="restrictToCurrentPlatform">If true, then only IPersistedStorage objects that are marked as compatible with the current OS are returned.</param>
        /// <returns>Returns an array of Types that can be instantiated for use for data persistance.</returns>
        public static List<IPersistedStorage> GetStorageContainers(Assembly[] fromAssemblies = null, bool restrictToCurrentPlatform = true)
        {
            if (fromAssemblies == null || fromAssemblies.Length == 0)
            {
                // We ignore any assembly that begins with "System" since they are the .NET assemblies which will not contain any of our IPersistedStorage objects.
                fromAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(
                        a => !a.FullName.StartsWith("System") &&
                        !a.FullName.StartsWith("Microsoft.") &&
                        !a.FullName.StartsWith("mscor") &&
                        !a.FullName.StartsWith("vshost"))
                    .ToArray();
            }
            var availableContexts = new List<IPersistedStorage>();

            // The current Operating System version.
            Version osVersion = System.Environment.OSVersion.Version;

            // Loop through all assemblies found and look for objects that implement IDataContext.
            foreach (Assembly assembly in fromAssemblies)
            {
                var temp = assembly;
                var contexts = temp.GetTypes()
                    .Where(t => t.GetInterface(typeof(IPersistedStorage).Name) != null);

                foreach (Type context in contexts)
                {
                    // If the user wants to have an array of objects that are guaranteed by the object to work on the current OS
                    // then we check for the PlatformSupport attribute and compare OS versions.
                    if (restrictToCurrentPlatform)
                    {
                        PlatformSupportAttribute[] supportedPlatform = (PlatformSupportAttribute[])context.GetCustomAttributes(typeof(PlatformSupportAttribute));

                        foreach (PlatformSupportAttribute platform in supportedPlatform)
                        {
                            if (platform.MajorVersion == osVersion.Major && platform.MinorVersion == osVersion.Minor)
                            {
                                availableContexts.Add(Activator.CreateInstance(context) as IPersistedStorage);
                                break;
                            }
                        }
                    }
                    else
                    {
                        // If the user doesn't want platform specific context's, then we just return every one of them that we find.
                        availableContexts.Add(Activator.CreateInstance(context) as IPersistedStorage);
                    }
                }
            }

            return availableContexts;
        }

        /// <summary>
        /// Gets the storage container specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns a storage container matching T</returns>
        public static IPersistedStorage GetStorageContainer<T>(Assembly[] fromAssemblies = null) where T : IPersistedStorage
        {
            // If the DefaultStorage has not been set, we will set it the first time a specific Type is passed.
            // It is assumed since a specific Type is provided, it will be re-used.
            if (PersistedStorageFactory.DefaultStorage == null)
            {
                PersistedStorageFactory.DefaultStorage = default(T);
            }

            // This isn't the most efficient.
            // Considering that GetStorageContainers should really only be returning a few (1-5?) IPersistedStorage objects
            // Filtering the list a second time (filtered once in GetStorageContainers) shouldn't hurt much.
            // If users start loading dozens of IPersistedStorage (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            // We fetch all container regardless if it is on a supported platform or not. Since it is strongly specified, we assume 
            // the user knows if it is compatible or not.
            return PersistedStorageFactory.GetStorageContainers(fromAssemblies, false).FirstOrDefault(storage => storage.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default storage.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">DefaultStorage can not be null.</exception>
        public static IPersistedStorage GetDefaultStorage(Assembly[] fromAssemblies)
        {
            if (PersistedStorageFactory.DefaultStorage == null)
            {
                throw new NullReferenceException("DefaultStorage can not be null.");
            }

            return (IPersistedStorage)Activator.CreateInstance(PersistedStorageFactory.DefaultStorage.GetType());
        }
    }
}

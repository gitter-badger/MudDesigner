//-----------------------------------------------------------------------
// <copyright file="PersistedStorageFactory.cs" company="AllocateThis!">
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
        public static List<IPersistedStorage> GetAvailableContext(Assembly[] fromAssemblies = null, bool restrictToCurrentPlatform = true)
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

            return PersistedStorageFactory.GetAvailableContext(fromAssemblies, true)
                .FirstOrDefault(storage => storage.GetType() == PersistedStorageFactory.DefaultStorage.GetType());
        }
    }
}

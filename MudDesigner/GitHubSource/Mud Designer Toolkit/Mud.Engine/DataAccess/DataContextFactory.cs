// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mud.DataAccess
{
    /// <summary>
    /// The DataContext Factory provides access to all of the storage container contexts that are available for the runtime.
    /// By implementing IDataContext, any given runtime can have it's own storage container for data persistance.
    /// </summary>
    public static class DataContextFactory
    {
        /// <summary>
        /// Searches all of the non-.NET Framework assemblies to locate any object that implements the IDataContext interface.
        /// All objects that are found are checked to see if they are compatible with the current OS and returned in an array if they are.
        /// This method only returns an array of Types that can be used, uninstantiated.
        /// </summary>
        /// <param name="restrictToCurrentPlatform">If true, then only IDataContext objects that are marked as compatible with the current OS are returned.</param>
        /// <returns>Returns an array of Types that can be instantiated for use for data persistance.</returns>
        public static Type[] GetAvailableContext(bool restrictToCurrentPlatform = true)
        {
            // We ignore any assembly that begins with "System" since they are the .NET assemblies which will not contain any of our IDataContext objects.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.FullName.StartsWith("System") && !a.FullName.StartsWith("Microsoft.") && !a.FullName.StartsWith("mscor") && !a.FullName.StartsWith("vshost"));
            var availableContexts = new List<Type>();

            // The current Operating System version.
            Version osVersion = Environment.OSVersion.Version;

            // Loop through all assemblies found and look for objects that implement IDataContext.
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                var contexts = assembly.GetTypes().Where(t => t.GetInterface("IDataContext") != null /* && !t.IsAbstract && !t.IsInterface */);

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
                                availableContexts.Add(context);
                                break;
                            }
                        }
                    }
                    else
                    {
                        // If the user doesn't want platform specific context's, then we just return every one of them that we find.
                        availableContexts.Add(context);
                    }
                }
            }

            return availableContexts.ToArray();
        }
    }
}

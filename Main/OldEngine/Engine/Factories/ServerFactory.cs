//-----------------------------------------------------------------------
// <copyright file="ServerFactory.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MudEngine.Engine.Core;
using MudEngine.Engine.Networking;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// Provides a means to fetch objects that implement the IServer interface
    /// </summary>
    public static class ServerFactory
    {
        /// <summary>
        /// Gets or sets the default server.
        /// </summary>
        /// <value>
        /// The default server.
        /// </value>
        public static IServer DefaultServer { get; set; }

        /// <summary>
        /// Gets a collection objects implementing IServer.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IServer</returns>
        public static List<IServer> GetServers(Assembly[] fromAssemblies = null)
        {
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            if (fromAssemblies == null)
            {
                fromAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IServer
            foreach (Assembly assembly in fromAssemblies)
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(IServer).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            return new List<IServer>(
                (from type in types
                 select Activator.CreateInstance(type) as IServer));
        }
        /// <summary>
        /// Gets the server specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IServer that you want to find</typeparam>
        /// <returns>Returns the server specified.</returns>
        public static IServer GetServer<T>(Assembly[] fromAssemblies = null) where T : IServer, new()
        {
            // If the DefaultServer has not been set, we will set it the first time a specific Type is passed.
            // It is assumed since a specific Type is provided, it will be re-used.
            if (ServerFactory.DefaultServer == null)
            {
                ServerFactory.DefaultServer = new T();
            }

            // This isn't the most efficient.
            // Considering that GetServers should really only be returning a few (1-5?) IServer objects
            // Filtering the list a second time (filtered once in GetServers) shouldn't hurt much.
            // If users start loading dozens of IServer (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return ServerFactory.GetServers(fromAssemblies).FirstOrDefault(server => server.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default server.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance matching the default server object.</returns>
        public static IServer GetDefaultServer(Assembly[] fromAssemblies = null)
        {
            if (ServerFactory.DefaultServer == null)
            {
                throw new NullReferenceException("DefaultServer must not be null.");
            }

            return ServerFactory.GetServers(fromAssemblies).FirstOrDefault(server => server.GetType() == ServerFactory.DefaultServer.GetType());
        }
    }
}

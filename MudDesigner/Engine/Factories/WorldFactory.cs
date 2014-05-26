//-----------------------------------------------------------------------
// <copyright file="WorldFactory.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Environment;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// Provides a means to fetch objects that implement the IWorld interface
    /// </summary>
    public static class WorldFactory
    {
        /// <summary>
        /// Gets or sets the default World.
        /// </summary>
        public static IWorld DefaultWorld { get; set; }

        /// <summary>
        /// Gets a collection objects implementing IWorld.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IWorld</returns>
        public static List<IWorld> GetWorlds(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IWorld
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(IWorld).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            // Convert our collection or Types into instances of IWorld
            // then return the IWorld collection.
            return new List<IWorld>(
                (from type in types
                 select Activator.CreateInstance(type) as IWorld));
        }

        /// <summary>
        /// Gets the World specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IWorld that you want to find</typeparam>
        /// <returns>Returns the World specified.</returns>
        public static IWorld GetWorld<T>(Assembly[] fromAssemblies = null) where T : IWorld, new()
        {
            // If the DefaultWorld has not been set, we will set it the first time a specific Type is passed.
            // It is assumed since a specific Type is provided, it will be re-used.
            if (WorldFactory.DefaultWorld == null)
            {
                WorldFactory.DefaultWorld = new T();
            }

            // This isn't the most efficient.
            // Considering that GetWorlds should really only be returning a few (1-5?) IWorld objects
            // Filtering the list a second time (filtered once in GetWorlds) shouldn't hurt much.
            // If users start loading dozens of IWorlds (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return WorldFactory.GetWorlds(fromAssemblies).FirstOrDefault(World => World.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default World.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance matching the default World object.</returns>
        public static IWorld GetDefaultWorld(Assembly[] fromAssemblies = null)
        {
            if (WorldFactory.DefaultWorld == null)
            {
                throw new NullReferenceException("DefaultWorld must not be null.");
            }

            return WorldFactory.GetWorlds(fromAssemblies)
                .FirstOrDefault(World => World.GetType() == WorldFactory.DefaultWorld.GetType());
        }
    }
}

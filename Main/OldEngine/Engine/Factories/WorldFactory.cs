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
    public class WorldFactory<T> : IFactory<T> where T : class, IWorld
    {
        /// <summary>
        /// Gets a collection objects implementing IWorld.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IWorld</returns>
        public List<T> GetObjects(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();


            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IWorld
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(T).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            // Convert our collection or Types into instances of IWorld
            // then return the IWorld collection.
            return new List<T>(
                (from type in types
                 select Activator.CreateInstance(type) as T));
        }

        /// <summary>
        /// Gets the World specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IWorld that you want to find</typeparam>
        /// <returns>Returns the World specified.</returns>
        public T GetObject<UTypeToFetch>(Assembly[] fromAssemblies = null, string compatibleType = null)
        {
            // This isn't the most efficient.
            // Considering that GetWorlds should really only be returning a few (1-5?) IWorld objects
            // Filtering the list a second time (filtered once in GetWorlds) shouldn't hurt much.
            // If users start loading dozens of IWorlds (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            List<T> results = this.GetObjects(fromAssemblies);
            T selectedType = null;

            if (compatibleType != null)
            {
                selectedType = results.FirstOrDefault(World => World.GetType().Name == compatibleType);
            }
            else
            {
                selectedType = results.FirstOrDefault(World => World.GetType() == typeof(UTypeToFetch));
            }

            return selectedType;
        }
    }
}

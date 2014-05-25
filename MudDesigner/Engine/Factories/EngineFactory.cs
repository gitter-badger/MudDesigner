//-----------------------------------------------------------------------
// <copyright file="EngineFactory.cs" company="AllocateThis!">
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
    /// Provides a means to fetch objects that implement the IGame interface
    /// </summary>
    public class EngineFactory <T> where T : class, new()
    {
        /// <summary>
        /// Gets a collection objects implementing T.
        /// </summary>
        /// <returns>A collection of objects in memory implementing T</returns>
        public List<T> GetObjects(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement 'T'
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.Name == (typeof(T).Name) &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            // Convert our collection or Types into instances of 'T'
            // then return the T collection.
            return new List<T>(
                (from type in types
                 select Activator.CreateInstance<T>()));
        }

        /// <summary>
        /// Gets the T specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing T that you want to find</typeparam>
        /// <returns>Returns the T specified.</returns>
        public T GetObject(Assembly[] fromAssemblies = null)
        {
            // This isn't the most efficient.
            // Considering that GetObjects should really only be returning a few (1-5?) T objects
            // Filtering the list a second time (filtered once in GetObjects) shouldn't hurt much.
            // If users start loading dozens of T (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return this.GetObjects(fromAssemblies).FirstOrDefault(game => game.GetType() == typeof(T));
        }
    }
}

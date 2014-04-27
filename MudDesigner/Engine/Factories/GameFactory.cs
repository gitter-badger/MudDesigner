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
    /// Provides a means to fetch objects that implement the IGame interface
    /// </summary>
    public static class GameFactory
    {
        /// <summary>
        /// Gets or sets the default game.
        /// </summary>
        public static IGame DefaultGame { get; set; }

        /// <summary>
        /// Gets a collection objects implementing IGame.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IGame</returns>
        public static List<IGame> GetGames(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IGame
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(IGame).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            // Convert our collection or Types into instances of IGame
            // then return the IGame collection.
            return new List<IGame>(
                (from type in types
                 select Activator.CreateInstance(type) as IGame));
        }

        /// <summary>
        /// Gets the game specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IGame that you want to find</typeparam>
        /// <returns>Returns the game specified.</returns>
        public static IGame GetGame<T>(Assembly[] fromAssemblies = null) where T : IGame
        {
            // If the DefaultGame has not been set, we will set it the first time a specific Type is passed.
            // It is assumed since a specific Type is provided, it will be re-used.
            if (GameFactory.DefaultGame == null)
            {
                GameFactory.DefaultGame = default(T);
            }

            // This isn't the most efficient.
            // Considering that GetGames should really only be returning a few (1-5?) IGame objects
            // Filtering the list a second time (filtered once in GetGames) shouldn't hurt much.
            // If users start loading dozens of IGames (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return GameFactory.GetGames(fromAssemblies).FirstOrDefault(game => game.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default game.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance matching the default game object.</returns>
        public static IGame GetDefaultGame(Assembly[] fromAssemblies = null)
        {
            if (GameFactory.DefaultGame == null)
            {
                throw new NullReferenceException("DefaultGame must not be null.");
            }

            return GameFactory.GetGames(fromAssemblies)
                .FirstOrDefault(game => game.GetType() == GameFactory.DefaultGame.GetType());
        }
    }
}

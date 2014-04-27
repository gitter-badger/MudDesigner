//-----------------------------------------------------------------------
// <copyright file="GameFactory.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// Provides a means to fetch objects that implement the IMob and IPlayer interfaces
    /// </summary>
    public static class MobFactory
    {
        /// <summary>
        /// Gets or sets the default mob.
        /// </summary>
        public static IMob DefaultMob { get; set; }

        /// <summary>
        /// Gets or sets the default player.
        /// </summary>
        public static IPlayer DefaultPlayer { get; set; }

        /// <summary>
        /// Gets a collection objects implementing IMob.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IMob</returns>
        public static List<IMob> GetMobs(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IMob
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(IMob).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            return new List<IMob>(
                (from type in types
                 select Activator.CreateInstance(type) as IMob));
        }

        /// <summary>
        /// Gets the game specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IGame that you want to find</typeparam>
        /// <returns>Returns the game specified.</returns>
        public static IMob GetMob<T>(Assembly[] fromAssemblies = null) where T : IMob
        {
            // This isn't the most efficient.
            // Considering that GetMobs should really only be returning a few (1-5?) IMob objects
            // Filtering the list a second time (filtered once in GetMobs) shouldn't hurt much.
            // If users start loading dozens of IMob (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return MobFactory.GetMobs(fromAssemblies).FirstOrDefault(mob => mob.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default game.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance matching the default game object.</returns>
        public static IMob GetDefaultMob(Assembly[] fromAssemblies = null)
        {
            return MobFactory.GetMobs(fromAssemblies)
                .FirstOrDefault(mob => mob.GetType() == MobFactory.DefaultMob.GetType());
        }

        /// <summary>
        /// Gets a collection objects implementing IPlayer.
        /// </summary>
        /// <returns>A collection of objects in memory implementing IPlayer</returns>
        public static List<IPlayer> GetPlayers(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement IPlayer
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(IPlayer).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            return new List<IPlayer>(
                (from type in types
                 select Activator.CreateInstance(type) as IPlayer));
        }

        /// <summary>
        /// Gets the game specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing IGame that you want to find</typeparam>
        /// <returns>Returns the game specified.</returns>
        public static IPlayer GetPlayer<T>(Assembly[] fromAssemblies = null) where T : IPlayer
        {
            // This isn't the most efficient.
            // Considering that GetPlayers should really only be returning a few (1-5?) IPlayer objects
            // Filtering the list a second time (filtered once in GetPlayers) shouldn't hurt much.
            // If users start loading dozens of IPlayer (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            return MobFactory.GetPlayers(fromAssemblies).FirstOrDefault(player => player.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets the default game.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance matching the default game object.</returns>
        public static IPlayer GetDefaultPlayer(Assembly[] fromAssemblies = null)
        {
            return MobFactory.GetPlayers(fromAssemblies).FirstOrDefault(player => player.GetType() == MobFactory.DefaultPlayer.GetType());
        }
    }
}

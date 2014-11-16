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
using MudEngine.Engine.Commands;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// Provides a means to fetch objects that implement the ICommand interface
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Gets a collection objects implementing ICommand.
        /// </summary>
        /// <returns>A collection of objects in memory implementing ICommand</returns>
        public static IEnumerable<ICommand> GetCommands(Assembly[] fromAssemblies = null)
        {
            var types = new List<Type>();

            // Loop through each assembly in our current app domain
            // generating a collection of Types that implement ICommand
            // If we are not provided with assemblies, we fetch all of them from the current domain.
            foreach (Assembly assembly in fromAssemblies ?? AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    type => type.GetInterface(typeof(ICommand).Name) != null &&
                    !type.IsAbstract && // Do not add abstract classes
                    !type.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            // Convert our collection or Types into instances of ICommand
            // then return the ICommand collection.
            return new List<ICommand>(
                (from type in types
                 select Activator.CreateInstance(type) as ICommand));
        }

        /// <summary>
        /// Gets the command specified.
        /// </summary>
        /// <typeparam name="T">The Type implementing ICommand that you want to find</typeparam>
        /// <returns>Returns the command specified.</returns>
        public static ICommand GetCommand<T>(Assembly[] fromAssemblies = null) where T : ICommand, new()
        {
            // This isn't the most efficient.
            // Considering that GetCommands should really only be returning a few (1-5?) ICommand objects
            // Filtering the list a second time (filtered once in GetCommands) shouldn't hurt much.
            // If users start loading dozens of ICommand (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            IEnumerable<ICommand> commands = CommandFactory.GetCommands(fromAssemblies);

            foreach(ICommand command in commands)
            {
                Type type = command.GetType();

                if (type == typeof(T))
                {
                    return command;
                }
            }

            return null;
        }

        public static ICommand GetCommand(string name, Assembly[] fromAssemblies = null)
        {
            // This isn't the most efficient.
            // Considering that GetCommands should really only be returning a few (1-5?) ICommand objects
            // Filtering the list a second time (filtered once in GetCommands) shouldn't hurt much.
            // If users start loading dozens of ICommand (bad practice!) in their scripts folder, then
            // this needs to be revisited.
            IEnumerable<ICommand> commands = CommandFactory.GetCommands(fromAssemblies);

            foreach (ICommand command in commands)
            {
                Type type = command.GetType();

                if (type.Name.ToLower() == name.ToLower())
                {
                    return command;
                }
                else if (type.Name == string.Format("{0}command", name.ToLower()))
                {
                    return command;
                }

                ShorthandNameAttribute attribute = type.GetCustomAttribute<ShorthandNameAttribute>();

                if (attribute != null)
                {
                    if (attribute.Command.ToLower() == name.ToLower())
                    {
                        return command;
                    }
                    else if (attribute.Shorthand.ToLower() == name.ToLower())
                    {
                        return command;
                    }
                }
            }

            return null;
        }

    }
}

//-----------------------------------------------------------------------
// <copyright file="ShorthandNameAttribute.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MudEngine.Engine.Commands
{
    /// <summary>
    /// Provides metadata for determining a shortcut for the supplied command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ShorthandNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShorthandNameAttribute"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="shorthand">The shorthand.</param>
        public ShorthandNameAttribute(string command, string shorthand)
        {
            this.Command = command;
            this.Shorthand = shorthand;
        }

        /// <summary>
        /// Gets the full command.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Gets the shorthand for the full command.
        /// </summary>
        public string Shorthand { get; private set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="HelpAttribute.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// HelpAttribute should be attached to all game Commands. It allows game commands to output helpful information
    /// regarding the command when the internal HelpCommand is used by the player.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HelpAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public HelpAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="IAppearanceAttribute.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// Appearance information related to the character that can be displayed to the players
    /// </summary>
    public interface IAppearanceAttribute
    {
        /// <summary>
        /// Gets or sets the Name of this appearance attribute. Example: Hair
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description of this appearance attribute.  Example: With long flowing hair that shines brightly in the sunlight.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        //
        /// <summary>
        /// Gets or sets the value that overrides the default description. Used for custom values that the player sets.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        string Value { get; set; }
    }   
}

//-----------------------------------------------------------------------
// <copyright file="CharacterRoles.cs" company="AllocateThis!">
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
    /// An enumeration of Roles that the player can have on the server
    /// </summary>
    public enum CharacterRoles
    {
        /// <summary>
        /// The standard
        /// </summary>
        Standard,

        /// <summary>
        /// The builder
        /// </summary>
        Builder,

        /// <summary>
        /// The admin
        /// </summary>
        Admin,

        /// <summary>
        /// The owner
        /// </summary>
        Owner
    }
}

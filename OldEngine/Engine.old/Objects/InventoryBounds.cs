//-----------------------------------------------------------------------
// <copyright file="InventoryBounds.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Objects
{
    /// <summary>
    /// An enumerator for determining if items can be moved from character to character or not.
    /// </summary>
    public enum InventoryBounds
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

        /// <summary>
        /// The account bound
        /// </summary>
        AccountBound,

        /// <summary>
        /// The character bound
        /// </summary>
        CharacterBound
    }
}

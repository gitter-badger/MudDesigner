//-----------------------------------------------------------------------
// <copyright file="IGender.cs" company="AllocateThis!">
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
    /// A interface contract that provides properties and methods for creating custom character Genders
    /// </summary>
    public interface IGender
    {
        /// <summary>
        /// Gets or Sets the name of this Gender
        /// </summary>
        string Name { get; set; }
    }
}

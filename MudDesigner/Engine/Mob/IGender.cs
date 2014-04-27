﻿//-----------------------------------------------------------------------
// <copyright file="IGender.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MudEngine.Engine.Mob
{
    /// <summary>
    /// Provides a contract for objects to implement a gender.
    /// </summary>
    public interface IGender
    {
        /// <summary>
        /// Gets or sets the name of this Gender.
        /// </summary>
        string Name { get; set; }
    }
}

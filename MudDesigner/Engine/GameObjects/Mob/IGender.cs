//-----------------------------------------------------------------------
// <copyright file="IGender.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MudEngine.Engine.GameObjects.Mob
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

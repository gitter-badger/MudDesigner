//-----------------------------------------------------------------------
// <copyright file="IWorld.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MudEngine.Engine.GameObjects.Environment
{
    /// <summary>
    /// Provides a contract for objects that want to implement a World
    /// </summary>
    public interface IWorld : IGameObject
    {
        /// <summary>
        /// Gets or sets a value indicating whether the world is safe.
        /// </summary>
        bool IsSafe { get; set; }
    }
}

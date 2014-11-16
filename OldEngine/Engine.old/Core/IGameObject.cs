//-----------------------------------------------------------------------
// <copyright file="IGameObject.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Globalization;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// The interface contract for GameObjects.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Name of the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description of this object.
        /// </summary>
        string Description { get; set; }
        
        /// <summary>
        /// Gets or Sets if the object is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or Sets if this object can never be deleted from the game.
        /// </summary>
        bool Permanent { get; set; }

        /// <summary>
        /// Gets if this GameObject has been destroyed.  If True, Save() code is ignored.
        /// </summary>
        bool Destroyed { get; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid ID { get; set; }
        
        /// <summary>
        /// Called when the object is no longer needed.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Copies the current values of this object to a new object
        /// </summary>
        /// <param name="copyTo">The object that should have it's properties overwritten with the values of the calling Object</param>
        void CopyState(ref IGameObject copyFrom, bool ignoreNonNullProperties = false);
    }
}
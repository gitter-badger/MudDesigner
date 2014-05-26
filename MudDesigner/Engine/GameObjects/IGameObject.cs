//-----------------------------------------------------------------------
// <copyright file="IGameObject.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.GameObjects
{
    /// <summary>
    /// Provides a contract to objects wanting to implement IGameObject
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editable.
        /// </summary>
        bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is permanent.
        /// </summary>
        bool IsPermanent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is destroyed.
        /// </summary>
        bool IsDestroyed { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        DateTime CreatedDate { get; set; }

        void Initialize(IGame game);

        /// <summary>
        /// Copies the state of one IGameObject to this one..
        /// </summary>
        /// <param name="copyFrom">The object to copy from.</param>
        /// <param name="ignoreExistingPropertyValues">if set to <c>true</c> [ignore existing property values].</param>
        void CopyState(IGameObject copyFrom, bool ignoreExistingPropertyValues = false);
    }
}

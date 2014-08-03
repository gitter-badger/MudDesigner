//-----------------------------------------------------------------------
// <copyright file="IItem.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Objects
{
    /// <summary>
    /// The interface contract supplies properties and methods for creating custom game items. This is the base for all game items
    /// </summary>
    public interface IItem : IGameObject
    {
        /// <summary>
        /// Gets or Sets if this item is accessible by only the admins or not.
        /// </summary>
        bool IsAdminOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is stackable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is stackable; otherwise, <c>false</c>.
        /// </value>
        bool IsStackable { get; set; }

        /// <summary>
        /// Gets or Sets the weight of this object
        /// </summary>
        int Weight { get; set; }

        /// <summary>
        /// Gets or Sets how much health this item has
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the current health.
        /// </summary>
        /// <value>
        /// The current health.
        /// </value>
        int CurrentHealth { get; set; }

        /// <summary>
        /// Gets or Sets if this item is indestructible
        /// </summary>
        bool Indestructible { get; set; }

        /// <summary>
        /// Inspects the item and tells the player what is seen.
        /// </summary>
        /// <param name="player">The player that wants to inspect this item</param>
        void Inspect(IPlayer player);

        /// <summary>
        /// Repairs the item
        /// </summary>
        /// <param name="healder">The character that is performing the heal</param>
        /// <param name="amount">The healing amount</param>
        void Repair(IMob healer, int amount);

        /// <summary>
        /// Deals damage to this item
        /// </summary>
        /// <param name="dealer">The character that is dealing the damage</param>
        /// <param name="amount">The amount of damage</param>
        void Damage(IMob dealer, int amount);
    }
}
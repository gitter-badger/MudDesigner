//-----------------------------------------------------------------------
// <copyright file="BaseItem.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Objects
{
    /// <summary>
    /// The base class for all game items.
    /// </summary>
    public abstract class BaseItem : GameObject,  IItem
    {
        /// <summary>
        /// Gets or Sets how much this item weighs
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is stackable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is stackable; otherwise, <c>false</c>.
        /// </value>
        public bool IsStackable { get; set; }

        /// <summary>
        /// Gets or sets the current health.
        /// </summary>
        /// <value>
        /// The current health.
        /// </value>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// Gets or Sets how much health this item has
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or Sets if this item is indestructible
        /// </summary>
        public bool Indestructible { get; set; }

        /// <summary>
        /// Gets or Sets if this item is usable only by admins or not.
        /// </summary>
        public bool IsAdminOnly { get; set; }

        /// <summary>
        /// Inspects the item and tells the player what is seen.
        /// </summary>
        /// <param name="player">The player that wants to inspect this item</param>
        public virtual void Inspect(IPlayer player)
        {
            player.SendMessage(Name);
            player.SendMessage(Description);
            player.SendMessage("Weight: " + Weight);
            player.SendMessage("Durability: " + Health);

            if (Indestructible)
                player.SendMessage("This item is permanent");

            player.SendMessage(""); // blank line
        }

        /// <summary>
        /// Repairs the item
        /// </summary>
        /// <param name="healder">The character that is performing the heal</param>
        /// <param name="amount">The healing amount</param>
        public void Repair(IMob healer, int amount)
        {
            if ((CurrentHealth + amount) > Health)
                CurrentHealth = Health;
            else
                CurrentHealth += amount;

            healer.SendMessage(String.Format("You repair {0} to a durability of {1}", Name, CurrentHealth));
        }

        /// <summary>
        /// Deals damage to this item
        /// </summary>
        /// <param name="dealer">The character that is dealing the damage</param>
        /// <param name="amount">The amount of damage</param>
        public void Damage(IMob dealer, int amount)
        {

        }
    }
}

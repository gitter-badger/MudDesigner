//-----------------------------------------------------------------------
// <copyright file="IEquipable.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Objects
{
    /// <summary>
    /// The interface contract supplies properties and methods for creating custom equipable items.
    /// </summary>
    public interface IEquipable : IItem
    {
        /// <summary>
        /// Can place 'stackable' items within this equipable item.
        /// Can be a Weapon with a "Rune of +5 Endurance" or
        /// if can be a bag with a bunch of item stacks within it.
        /// </summary>
        Dictionary<IItem, int> Components { get; }

        /// <summary>
        /// Equips this item to the specified player
        /// </summary>
        /// <param name="player">The player to equip to</param>
        void Equip(IPlayer player);

        /// <summary>
        /// Unequips this item from the specified player
        /// </summary>
        /// <param name="player">The player to unequip from</param>
        void Unequip(IPlayer player);

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="equipment">The equipment.</param>
        void AddComponent(IItem equipment);

        /// <summary>
        /// Removes the component.
        /// </summary>
        /// <param name="equipment">The equipment.</param>
        void RemoveComponent(IItem equipment);
    }
}

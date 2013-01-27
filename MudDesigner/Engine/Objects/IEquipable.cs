/* IEquipable
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The interface contract supplies properties and methods for creating custom equipable items.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
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

        void AddComponent(IItem equipment);

        void RemoveComponent(IItem equipment);
    }
}

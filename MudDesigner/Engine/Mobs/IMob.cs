/* IMob
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: A interface contract that provides properties and methods for creating custom characters
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// A enumerator for determing at what level this character can broadcast messages to.
    /// </summary>
    public enum MessageBroadcastLevels
    {
        Zone,
        Room,
    }

    /// <summary>
    /// A interface contract that provides properties and methods for creating custom characters
    /// </summary>
    public interface IMob : IGameObject
    {
        /// <summary>
        /// Gender of this character
        /// </summary>
        IGender Gender { get; set; }

        /// <summary>
        /// Race of this character
        /// </summary>
        IRace Race { get; set; }

        /// <summary>
        /// Character Class
        /// </summary>
        IClass Class { get; set; }

        /// <summary>
        /// Current location this character resides within
        /// </summary>
        IRoom Location { get; }

        /// <summary>
        /// Gets or Sets if this Character can Talk
        /// </summary>
        bool CanTalk { get; set; }

        /// <summary>
        /// Gets or Sets a collection of Items that the character has.
        /// </summary>
        List<IItem> Items { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of items the character can carry.
        /// </summary>
        int MaxInventorySize { get; set; }

        /// <summary>
        /// Gets or Sets a collection of stats that the character has
        /// </summary>
        List<IStat> Stats { get; set; }

        /// <summary>
        /// Gets or Sets a reference to a collection of appearance attributes the character has.
        /// </summary>
        List<IAppearanceAttribute> Appearance { get; set; }

        IServerDirector Director { get; set; }

        void SendMessage(string message, bool newLine = true);

        /// <summary>
        /// Sends a message from the character, to all occupants within the broadcast range specified.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="broadcastLevel"></param>
        void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room);

        /// <summary>
        /// Sends a message from the character, to the target character
        /// </summary>
        /// <param name="message"></param>
        /// <param name="target"></param>
        void Talk(string message, IMob target);

        /// <summary>
        /// Sends a message from the character, to the target group of characters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="group"></param>
        void Talk(string message, IMob[] group);

        //TODO: Think how this should be implemented
        //void Talk(string message, IFaction faction);

        /// <summary>
        /// Moves the character from one location, to another.
        /// </summary>
        /// <param name="room"></param>
        void Move(IRoom room);

        /// <summary>
        /// Adds a specified item to the characters inventory
        /// </summary>
        /// <param name="item"></param>
        void AddItem(IItem item);

        /// <summary>
        /// Adds the specified stat to the character
        /// </summary>
        /// <param name="stat"></param>
        void AddStat(IStat stat);

        /// <summary>
        /// Adds an appearance attribute to the character
        /// </summary>
        /// <param name="attribute"></param>
        void AddAppearanceDescription(IAppearanceAttribute attribute);

        /// <summary>
        /// Returns an array of the characters inventory items
        /// </summary>
        /// <returns></returns>
        IItem[] GetItems();

        /// <summary>
        /// Returns an array of all stats the character currently has
        /// </summary>
        /// <returns></returns>
        IStat[] GetStats();

        /// <summary>
        /// Gets all of the appearance attributes that the character has.
        /// </summary>
        /// <returns></returns>
        IAppearanceAttribute[] GetAppearanceDescriptions();

        /// <summary>
        /// Removes the specified item from the characters inventory
        /// </summary>
        /// <param name="item"></param>
        void RemoveItem(IItem item);

        /// <summary>
        /// Removes the specified item from the characters inventory
        /// </summary>
        /// <param name="item"></param>
        void RemoveItem(string item);

        /// <summary>
        /// Removes the specified stat from the character
        /// </summary>
        /// <param name="stat"></param>
        void RemoveStat(IStat stat);

        /// <summary>
        /// Removes a specified appearance attribute from the character
        /// </summary>
        /// <param name="attribute"></param>
        void RemoveAppearanceAttribute(IAppearanceAttribute attribute);

        /// <summary>
        /// Clears the characters inventory
        /// </summary>
        void ClearItems();

        void Attack(IMob target);

        void Attack(IMob[] targets); //Array for AOE attacks

        void Damage(IGameObject dealer, int amount);

        void Heal(IGameObject dealer, int amount);

        void RestoreMana(IGameObject dealer, int amount);

        void ConsumeMana(int amount);
    }
}

//-----------------------------------------------------------------------
// <copyright file="EngineMob.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml.Serialization;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob
{
    /// <summary>
    /// The base engine mob object that all Mob related objects inherit from.
    /// </summary>
    [Serializable]
    public class EngineMob : GameObject, IMob
    {
        /// <summary>
        /// Gets or Sets the currently running game.
        /// </summary>
        [XmlIgnore]
        public IGame Game { get; set; }

        /// <summary>
        /// Gets or sets the gender for this character.
        /// </summary>
        [XmlIgnore]
        public IGender Gender { get; set; }

        /// <summary>
        /// Gets or sets this characters current location in the world.
        /// </summary>
        [XmlIgnore]
        public Environment.IRoom Location { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can speak or not..
        /// </summary>
        public bool IsMute { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the inventory.
        /// </summary>
        /// <value>
        /// The maximum size of the inventory.
        /// </value>
        public int MaximumInventorySize { get; set; }

        /// <summary>
        /// Processes an ICommand.
        /// AI based mobs can use ICommand's as well.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ProcessCommand(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ProcessCommand(string command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts the specified message to the appropriate environment.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="broadcastLevel">The broadcast level.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts the message to the specified IMob object.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Talk(string message, IMob target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts the message to a specific group of IMob objects.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="group">The group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Talk(string message, IMob[] group)
        {
            throw new NotImplementedException();
        }
    }
}

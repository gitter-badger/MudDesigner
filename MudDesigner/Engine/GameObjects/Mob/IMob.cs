//-----------------------------------------------------------------------
// <copyright file="IMob.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Environment;

namespace MudEngine.Engine.GameObjects.Mob
{
    /// <summary>
    /// Provides a contract for objects that need to be a Mob.
    /// </summary>
    public interface IMob : IGameObject
    {
        /// <summary>
        /// Gets or Sets the currently running game.
        /// </summary>
        IGame Game { get; set; }

        /// <summary>
        /// Gets or sets the gender for this character.
        /// </summary>
        IGender Gender { get; set; }

        /// <summary>
        /// Gets or sets this characters current location in the world.
        /// </summary>
        IRoom Location { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can speak or not..
        /// </summary>
        bool IsMute { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the inventory.
        /// </summary>
        /// <value>
        /// The maximum size of the inventory.
        /// </value>
        int MaximumInventorySize { get; set; }

        /// <summary>
        /// Processes an ICommand.
        /// AI based mobs can use ICommand's as well.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        bool ProcessCommand(byte[] data);

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        bool ProcessCommand(string command);

        // TODO: Move all of the Talk methods into a TalkCommand class.

        /// <summary>
        /// Broadcasts the specified message to the appropriate environment.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="broadcastLevel">The broadcast level.</param>
        void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room);

        /// <summary>
        /// Broadcasts the message to the specified IMob object.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        void Talk(string message, IMob target);

        /// <summary>
        /// Broadcasts the message to a specific group of IMob objects.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="group">The group.</param>
        void Talk(string message, IMob[] group);
    }
}

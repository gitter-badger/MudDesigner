//-----------------------------------------------------------------------
// <copyright file="ICharacter.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Character
{
    using System;
    using Mud.Engine.Shared.Environment;
    using Mud.Engine.Shared.Commanding;
    using Mud.Engine.Shared.Core;

    /// <summary>
    /// Provides a contract for objects wanting to act as an character Type.
    /// </summary>
    public interface ICharacter : IPersistedObject
    {
        /// <summary>
        /// Occurs when the object is loaded.
        /// </summary>
        event EventHandler Loaded;

        /// <summary>
        /// Occurs when being unloaded.
        /// </summary>
        event EventHandler Unloaded;

        /// <summary>
        /// Occurs when the instance receives an IMessage.
        /// </summary>
        event EventHandler<InputArgs> MessageReceived;

        /// <summary>
        /// Occurs when the instance sends an IMessage.
        /// </summary>
        event EventHandler<InputArgs> MessageSent;

        /// <summary>
        /// Occurs when the character changes rooms.
        /// </summary>
        event EventHandler<OccupancyChangedEventArgs> RoomChanged;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        /// Gets or sets the current room that this character occupies.
        /// </summary>
        IRoom CurrentRoom { get; set; }

        /// <summary>
        /// Gets the command manager.
        /// </summary>
        ICommandManager CommandManager { get; }

        /// <summary>
        /// Initializes this instance with the given game.
        /// </summary>
        /// <param name="game">The game.</param>
        void Initialize(IGame game);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(IMessage message);

        /// <summary>
        /// Accepts a message from another object.
        /// </summary>
        /// <param name="message">The message.</param>
        void AcceptMessage(IMessage message);
    }
}

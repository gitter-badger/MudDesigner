//-----------------------------------------------------------------------
// <copyright file="IMob.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Character
{
    using System;
    using Mud.Engine.Core.Engine;

    /// <summary>
    /// Provides a contract for objects wanting to act as an IMob Type.
    /// </summary>
    public interface ICharacter
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
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        IGame Game { get; }

        /// <summary>
        /// Initializes this instance with the given game.
        /// </summary>
        /// <param name="game">The game.</param>
        void Initialize(IGame game);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(string message);
    }
}

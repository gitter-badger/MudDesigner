//-----------------------------------------------------------------------
// <copyright file="DefaultPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Character
{
    using System;
    using Mud.Engine.Core.Engine;
    using Mud.Engine.Core.Environment;

    /// <summary>
    /// The Default Engine implementation of IPlayer.
    /// </summary>
    public class DefaultPlayer : IPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPlayer"/> class.
        /// </summary>
        public DefaultPlayer()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Occurs when the object is loaded.
        /// </summary>
        public event EventHandler Loaded;

        /// <summary>
        /// Occurs when being unloaded.
        /// </summary>
        public event EventHandler Unloaded;

        /// <summary>
        /// Occurs when the instance receives an IMessage.
        /// </summary>
        public event EventHandler<InputArgs> MessageReceived;

        /// <summary>
        /// Occurs when the instance sends an IMessage.
        /// </summary>
        public event EventHandler<InputArgs> MessageSent;

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public IGame Game { get; private set; }

        public IRoom CurrentRoom { get; set; }

        /// <summary>
        /// Initializes this instance with the given game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Initialize(IGame game)
        {
            this.Game = game;
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(string message)
        {
            this.OnMessageSent(message);
        }

        /// <summary>
        /// Called when the player is loaded.
        /// </summary>
        protected virtual void OnLoaded()
        {
            EventHandler handler = this.Loaded;
            if (handler == null)
            {
                return;
            }

            handler(this, new EventArgs());
        }

        /// <summary>
        /// Called when the player is unloaded.
        /// </summary>
        protected virtual void OnUnloaded()
        {
            EventHandler handler = this.Unloaded;
            if (handler == null)
            {
                return;
            }

            handler(this, new EventArgs());
        }

        /// <summary>
        /// Called when an IMessage is received.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void OnMessageReceived(string message)
        {
            EventHandler<InputArgs> handler = this.MessageReceived;
            if (handler == null)
            {
                return;
            }

            handler(this, new InputArgs(message));
        }

        /// <summary>
        /// Called when an IMessage is sent from the player.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void OnMessageSent(string message)
        {
            EventHandler<InputArgs> handler = this.MessageSent;
            if (handler == null)
            {
                return;
            }

            handler(this, new InputArgs(message));
        }
    }
}

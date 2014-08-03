//-----------------------------------------------------------------------
// <copyright file="MessengerArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.GameObjects
{
    /// <summary>
    /// Defines a message that is broadcasted from one object.
    /// </summary>
    public class MessengerArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessengerArgs"/> class.
        /// </summary>
        /// <param name="messenger">The messenger.</param>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        public MessengerArgs(IGameObject messenger, string message, IEnumerable<IGameObject> target)
        {
            this.Messenger = messenger;
            this.Message = message;
        }

        /// <summary>
        /// Gets the message content.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the messenger.
        /// </summary>
        public IGameObject Messenger { get; private set; }

        /// <summary>
        /// Gets or sets the target this message is intended for.
        /// </summary>
        public IEnumerable<IGameObject> Target { get; set; }
    }
}

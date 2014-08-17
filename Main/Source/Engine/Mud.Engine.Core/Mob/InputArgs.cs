//-----------------------------------------------------------------------
// <copyright file="InputArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Mob
{
    using System;
    using Mud.Engine.Core.Engine;

    /// <summary>
    /// Input Arguments provided when an event is fired requiring messaging support.
    /// </summary>
    public class InputArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InputArgs(string message)
        {
            this.Message = new SystemMessage(message);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public IMessage Message { get; private set; }
    }
}

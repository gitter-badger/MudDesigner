//-----------------------------------------------------------------------
// <copyright file="SystemMessage.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Core
{
    /// <summary>
    /// Provides System Messages for sharing.
    /// </summary>
    public class SystemMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SystemMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }
    }
}

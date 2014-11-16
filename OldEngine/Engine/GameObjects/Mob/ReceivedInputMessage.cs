using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob
{
    /// <summary>
    /// An implementation of IMessage for messages received from user input.
    /// </summary>
    public class ReceivedInputMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedInputMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ReceivedInputMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <returns>
        /// Returns the message, formatted as lower case for easier equality comparisons.
        /// </returns>
        public string FormatMessage()
        {
            return Message.ToLower();
        }
    }
}

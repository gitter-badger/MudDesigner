using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Leaves the original message intact, un-formatted with a new line appended.
    /// </summary>
    public class InformationalMessage : IMessage
    {
        public InformationalMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Formats the message.
        /// </summary>
        public string FormatMessage()
        {
            return this.Message += Environment.NewLine;
        }
    }
}

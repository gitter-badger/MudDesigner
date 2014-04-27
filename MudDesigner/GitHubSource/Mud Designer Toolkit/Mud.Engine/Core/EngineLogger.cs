using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Core
{
    // TODO: Replace this with something better.
    public class EngineLogger
    {
        public enum LogLevel
        {
            Informational,
            Warning,
            Error
        }

        /// <summary>
        /// Gets a reference to all of the informational messages logged during this session.
        /// </summary>
        public List<string> InformationMessages { get; private set; }

        /// <summary>
        /// Gets a reference to all of the warning messages logged during this session.
        /// </summary>
        public List<string> WarningMessages { get; private set; }

        /// <summary>
        /// Gets a reference to all of the error messages logged during this session.
        /// </summary>
        public List<string> ErrorMesssages { get; private set; }

        /// <summary>
        /// Gets the last message logged.
        /// </summary>
        public string LastMessageReceived { get; private set; }

        public void Log(string message, LogLevel level = LogLevel.Informational)
        {
            // Make sure are collections are instanced
            if (this.InformationMessages == null) this.InformationMessages = new List<string>();
            if (this.WarningMessages == null) this.WarningMessages = new List<string>();
            if (this.ErrorMesssages == null) this.ErrorMesssages = new List<string>();

            // Log the message into the corresponding collection.
            if (level == LogLevel.Informational) this.InformationMessages.Add(message);
            else if (level == LogLevel.Warning) this.WarningMessages.Add(message);
            else if (level == LogLevel.Error) this.ErrorMesssages.Add(message);

            // Log the last message received. 
            this.LastMessageReceived = message;
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="TextfileLogger.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.DefaultDesktop.Engine
{
    using System.IO;
    using Mud.Engine.Core.Engine;

    /// <summary>
    /// A logger that logs information to a local text file.
    /// </summary>
    public class TextfileLogger : ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        public void Log<TMessage>(TMessage message) where TMessage : IMessage, new()
        {
            using (var outfile = new StreamWriter(@"\Log.txt"))
            {
                outfile.Write(string.Format("{0} - {1}", typeof(TMessage).Name, message.Message));
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Core
{
    /// <summary>
    /// The Logger interface provides logging methods for objects to implement.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        void Log<TMessage>(TMessage message) where TMessage : IMessage, new();
    }
}

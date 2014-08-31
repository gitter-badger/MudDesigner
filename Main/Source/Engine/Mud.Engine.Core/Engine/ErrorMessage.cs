//-----------------------------------------------------------------------
// <copyright file="ErrorMessage.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// An implementation of IValidationMessage that can be used for error messages
    /// </summary>
    public class ErrorMessage : IMessage
    {
        private Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        public ErrorMessage()
        {
            this.Message = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ErrorMessage(string message, Exception exception)
        {
            this.exception = exception;
            this.Message = string.Format(
                "{0} exception for type {1}. Error Message -  {2}\nCall Stack - {3}", 
                this.exception.GetType().Name, 
                this.exception.Source,  
                this.exception.StackTrace,
                message);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that contains Message for this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents the Message in this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "{0} exception for type {1}. Error Message -  {2}\nCall Stack - {3}", 
                this.exception.GetType().Name, 
                this.exception.Source,  
                this.exception.StackTrace,
                this.Message);
        }
    }
}

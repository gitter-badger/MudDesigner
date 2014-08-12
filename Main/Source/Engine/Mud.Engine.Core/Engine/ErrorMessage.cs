namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// An implementation of IValidationMessage that can be used for error messages
    /// </summary>
    public class ErrorMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        public ErrorMessage() : this (string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ErrorMessage(string message)
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that contains Message for this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents the Message in this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Message;
        }
    }
}

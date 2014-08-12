namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// An implementation of IValidationMessage that can be used for warning messages
    /// </summary>
    public class WarningMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarningMessage"/> class.
        /// </summary>
        public WarningMessage() : this(string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WarningMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public WarningMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

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

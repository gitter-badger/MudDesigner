//-----------------------------------------------------------------------
// <copyright file="ValidationChangedEventArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Runtime.Core
{
    using Mud.Engine.Shared.Core;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Arguments that are provided when an objects validation has changed.
    /// </summary>
    public class ValidationChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationChangedEventArgs"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="messages">The messages.</param>
        public ValidationChangedEventArgs(string property, IEnumerable<IMessage> messages)
        {
            this.ChangedProperty = property;
            this.ValidationMessages = messages;
        }

        /// <summary>
        /// Gets the changed property name.
        /// </summary>
        /// <value>
        /// The changed property.
        /// </value>
        public string ChangedProperty { get; private set; }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>
        /// The validation messages.
        /// </value>
        public IEnumerable<IMessage> ValidationMessages { get; private set; }
    }
}

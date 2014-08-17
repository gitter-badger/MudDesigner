//-----------------------------------------------------------------------
// <copyright file="IValidationRule.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine.Validation
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Objects implementing IValidationRule can be used to perform validation on an object.
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// Gets or sets the type of the validation message used when validation fails..
        /// </summary>
        /// <value>
        /// The type of the validation message.
        /// </value>
        Type ValidationMessageType { get; set; }

        /// <summary>
        /// Gets or sets the failure message to be used when validation fails..
        /// </summary>
        /// <value>
        /// The failure message.
        /// </value>
        string FailureMessage { get; set; }

        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="sender">The sender.</param>
        /// <returns>Returns an IMessage matching the ValidationMessageType property, with the message given by the FailureMessage property</returns>
        IMessage Validate(PropertyInfo property, IValidatable sender);
    }
}

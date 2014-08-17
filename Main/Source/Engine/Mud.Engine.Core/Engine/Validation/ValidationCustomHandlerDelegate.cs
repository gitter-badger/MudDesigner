//-----------------------------------------------------------------------
// <copyright file="ValidationCustomHandlerDelegate.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine.Validation
{
    using System;

    /// <summary>
    /// Used to identify a method to be invoked when validation occurs on a property having a ValidateWithCustomHandlerAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ValidationCustomHandlerDelegate : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the delegate.
        /// </summary>
        /// <value>
        /// The name of the delegate.
        /// </value>
        public string DelegateName { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ValidateValueIsNotNullOrEmptyAttribute.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine.Validation
{
    using System;
    using System.Collections;

    /// <summary>
    /// Validates if the property is null or not.
    /// </summary>
    public class ValidateValueIsNotNullOrEmptyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <param name="property">The property that will its value validated.</param>
        /// <param name="sender">The sender who owns the property.</param>
        /// <returns>
        /// Returns a validation message if validation failed. Otherwise null is returned to indicate a passing validation.
        /// </returns>
        public override IMessage Validate(System.Reflection.PropertyInfo property, IValidatable sender)
        {
            if (!this.CanValidate(sender))
            {
                return null;
            }

            var validationMessage = Activator.CreateInstance(this.ValidationMessageType, this.FailureMessage) as IMessage;
            var value = property.GetValue(sender, null);

            if (value == null)
            {
                return validationMessage;
            }
            else if (property.PropertyType == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(value as string))
                {
                    return validationMessage;
                }
            }
            else if (value is ICollection)
            {
                if ((value as ICollection).Count == 0)
                {
                    return validationMessage;
                }
            }

            return null;
        }
    }
}

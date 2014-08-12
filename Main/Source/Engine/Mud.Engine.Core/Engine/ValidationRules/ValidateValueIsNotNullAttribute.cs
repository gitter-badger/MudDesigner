using System;

namespace Mud.Engine.Core.Engine.ValidationRules
{
    /// <summary>
    /// Valides if the property is null or not.
    /// </summary>
    public class ValidateValueIsNotNullAttribute : ValidationAttribute
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

            return value == null ? validationMessage : null;
        }
    }
}

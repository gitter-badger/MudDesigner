using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mud.Engine.Core.Engine.ValidationRules
{
    public class ValidateWithCustomHandlerAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the name of the delegate.
        /// </summary>
        /// <value>
        /// The name of the delegate.
        /// </value>
        public string DelegateName { get; set; }

        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <param name="property">The property that will its value validated.</param>
        /// <param name="sender">The sender who owns the property.</param>
        /// <returns>
        /// Returns a validation message if validation failed. Otherwise null is returned to indicate a passing validation.
        /// </returns>
        /// <exception cref="System.MissingMethodException"></exception>
        public override IMessage Validate(System.Reflection.PropertyInfo property, IValidatable sender)
        {
            if (!this.CanValidate(sender))
            {
                return null;
            }

            // Create an instance of our validation message and return it if there is not a delegate specified.
            IMessage validationMessage = Activator.CreateInstance(this.ValidationMessageType, this.FailureMessage) as IMessage;
            if (string.IsNullOrEmpty(this.DelegateName))
            {
                return validationMessage;
            }

            // Find our delegate method.
            IEnumerable<MethodInfo> validationMethods = sender
                .GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.GetCustomAttributes(typeof(ValidationCustomHandlerDelegate), true).Any());

            MethodInfo validationDelegate = validationMethods.FirstOrDefault(m => m
                    .GetCustomAttributes(typeof(ValidationCustomHandlerDelegate), true)
                    .FirstOrDefault(del => (del as ValidationCustomHandlerDelegate).DelegateName == this.DelegateName) != null);

            // Attempt to invoke our delegate method.
            object result = null;
            try
            {
                 result = validationDelegate.Invoke(sender, new object[] { property, sender, validationMessage });

            }
            catch (Exception)
            {
                throw;
            }

            // Return the results of the delegate method.
            if (result != null && result is IMessage)
            {
                return result as IMessage;
            }
            else if (result == null)
            {
                return null;
            }

            return validationMessage;
        }
    }
}

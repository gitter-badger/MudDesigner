using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine.ValidationRules
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class ValidationAttribute : Attribute, IValidationRule
    {
        /// <summary>
        /// Gets or sets the type that will be used to create a IValidationMessage instance.
        /// </summary>
        /// <value>
        /// The type of the validation message.
        /// </value>
        public Type ValidationMessageType { get; set; }

        /// <summary>
        /// Gets or sets the failure message.
        /// </summary>
        /// <value>
        /// The failure message.
        /// </value>
        public string FailureMessage { get; set; }

        /// <summary>
        /// Gets or sets whether this validation will run. If the target property specified is true, then validation runs.
        /// </summary>
        /// <value>
        /// The enable validation from property boolean.
        /// </value>
        public string ValidateIfPropertyIsTrue { get; set; }

        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <param name="property">The property that will its value validated.</param>
        /// <param name="sender">The sender who owns the property.</param>
        /// <returns>Returns a validation message if validation failed. Otherwise null is returned to indicate a passing validation.</returns>
        public abstract IValidationMessage Validate(PropertyInfo property, IValidatable sender);

        /// <summary>
        /// Determines if the value passed in to it is a valid boolean.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Can not base validation off of a non-boolean property.</exception>
        protected bool CanValidate(object sender)
        {
            if (string.IsNullOrEmpty(this.ValidateIfPropertyIsTrue))
            {
                return true;
            }

            string valueToParse = string.Empty;
            bool evaluateInverseValue = false;
            if (this.ValidateIfPropertyIsTrue.StartsWith("!"))
            {
                evaluateInverseValue = true;
                valueToParse = this.ValidateIfPropertyIsTrue.Substring(1);
            }

            bool result = false;
            if (!bool.TryParse(this.GetComparisonValue(sender, valueToParse).ToString(), out result))
            {
                throw new ArgumentException("Can not base validation off of a non-boolean property.");
            }

            if (evaluateInverseValue)
            {
                return !result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Walks the senders Type tree to find the property (or sub-property) specified.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="alternateProperty">The secondary property.</param>
        /// <returns>
        /// Returns the value associated with the specified property.
        /// </returns>
        protected object GetComparisonValue(object sender, string alternateProperty)
        {
            if (!string.IsNullOrEmpty(alternateProperty))
            {
                string[] pathToProperty = alternateProperty.Split('.');
                PropertyInfo comparisonProperty = null;

                try
                {
                    comparisonProperty = sender
                        .GetType()
                        .GetProperty(pathToProperty[0]);
                }
                catch (Exception)
                {
                    throw;
                }

                if (pathToProperty.Length == 1)
                {
                    return comparisonProperty.GetValue(sender, null);
                }
                else if (pathToProperty.Length > 1)
                {
                    // Walk down the tree to find the final value we are evaluating against.
                    object childSender = null;
                    for (int index = 1; index < pathToProperty.Length; index++)
                    {
                        try
                        {
                            childSender = comparisonProperty.GetValue(sender, null);
                            comparisonProperty = childSender
                                .GetType()
                                .GetProperty(pathToProperty[index]);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    // Grab the length of this string.
                    return comparisonProperty.GetValue(childSender, null);
                }
            }

            return null;
        }
    }
}

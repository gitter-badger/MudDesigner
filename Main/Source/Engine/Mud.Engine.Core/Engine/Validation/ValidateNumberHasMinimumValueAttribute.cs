//-----------------------------------------------------------------------
// <copyright file="ValidateNumberHasMinimumValueAttribute.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine.Validation
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Validates that a number has a minimum value.
    /// </summary>
    public class ValidateNumberHasMinimumValueAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the type of the number data.
        /// </summary>
        /// <value>
        /// The type of the number data.
        /// </value>
        private ValidationNumberDataTypes numberDataType;

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public string MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets the optional comparison property.
        /// This can be a child property within a property as long as the root property belongs to the sender provided to the Validate method.
        /// If a comparison property is specified, then MaximumLength property is ignored.
        /// Using this comparison is much slower than just specifying a MaximumLength.
        /// </summary>
        /// <value>
        /// The optional comparison property.
        /// </value>
        public string ComparisonProperty { get; set; }

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

            var validationMessage =
                Activator.CreateInstance(this.ValidationMessageType, this.FailureMessage) as IMessage;

            // Get the property value.
            var propertyValue = property.GetValue(sender, null);

            // Ensure the property value is the same data type we are comparing to.
            if (!this.ValidateDataTypesAreEqual(propertyValue))
            {
                var error = string.Format(
                    "The property '{0}' data type is not the same as the data type ({1}) specified for validation checks. They must be the same Type.",
                    property.PropertyType.Name,
                    this.numberDataType.ToString());
                throw new ArgumentNullException(error);
            }

            // Check if we need to compare against another property.
            object alternateProperty = null;
            if (!string.IsNullOrEmpty(this.ComparisonProperty))
            {
                // Fetch the value of the secondary property specified.
                alternateProperty = this.GetComparisonValue(sender, this.ComparisonProperty);
            }

            if (this.numberDataType == ValidationNumberDataTypes.Short)
            {
                return this.ValidateMinimumShortValue(propertyValue, alternateProperty, validationMessage);
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Int)
            {
                return this.ValidateMinimumIntegerValue(propertyValue, alternateProperty, validationMessage);
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Long)
            {
                return this.ValidateMinimumLongValue(propertyValue, alternateProperty, validationMessage);
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Float)
            {
                return this.ValidateMinimumFloatValue(propertyValue, alternateProperty, validationMessage);
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Double)
            {
                return this.ValidateMinimumDoubleValue(propertyValue, alternateProperty, validationMessage);
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Decimal)
            {
                return this.ValidateMinimumDecimalValue(propertyValue, alternateProperty, validationMessage);
            }

            return validationMessage;
        }

        /// <summary>
        /// Validates the data types are equal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns true if the types match.</returns>
        private bool ValidateDataTypesAreEqual(object value)
        {
            // If no data type was specified, we have to determine it ourself.
            // We need to identify the data type so that we can attempt to
            // properly convert the provided value to its associated data type.
            if (this.numberDataType == ValidationNumberDataTypes.None)
            {
                // Run through the supported data types and assign the numberDataType value
                // once we identify a supported value. If none is found, we return false.
                if (value is short)
                {
                    // Assign is as Short.
                    this.numberDataType = ValidationNumberDataTypes.Short;
                }
                else if (value is int)
                {
                    // Assign it as integer.
                    this.numberDataType = ValidationNumberDataTypes.Int;
                }
                else if (value is long)
                {
                    // Assign it as long
                    this.numberDataType = ValidationNumberDataTypes.Long;
                }
                else if (value is float)
                {
                    // Assign it as float
                    this.numberDataType = ValidationNumberDataTypes.Float;
                }
                else if (value is double)
                {
                    // Assign it as double.
                    this.numberDataType = ValidationNumberDataTypes.Double;
                }
                else if (value is decimal)
                {
                    // Assign it as decimal.
                    this.numberDataType = ValidationNumberDataTypes.Decimal;
                }
                else
                {
                    return false;
                }

                return true;
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Short && value is short)
            {
                // The data type (short) required and the value provided match.
                return true;
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Int && value is int)
            {
                // The data type (int) required and the value provided match.
                return true;
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Long && value is long)
            {
                // The data type (long) required and the value provided match.
                return true;
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Float && value is float)
            {
                // The data type (float) required and the value provided match.
                return true;
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Double && value is double)
            {
                // The data type (double) required and the value provided match.
                return true;
            }
            else if (this.numberDataType == ValidationNumberDataTypes.Decimal && value is decimal)
            {
                // The data type (decimal) required and the value provided match.
                return true;
            }

            // We never found a matching data type.
            return false;
        }

        /// <summary>
        /// Validates the minimum short value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="alternateProperty">The alternate property.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Returns a Validation message if it fails</returns>
        private IMessage ValidateMinimumShortValue(object propertyValue, object alternateProperty, IMessage validationMessage)
        {
            short convertedValueFromProperty = 0;
            short convertedMinimumValue = 0;
            bool propertyConversionSucceeded = short.TryParse(propertyValue.ToString(), NumberStyles.Integer, null, out convertedValueFromProperty);
            bool valueComparisonConversionSucceeded = short.TryParse(this.MinimumValue, NumberStyles.Integer, null, out convertedMinimumValue);

            if (!propertyConversionSucceeded && !valueComparisonConversionSucceeded && alternateProperty == null)
            {
                throw new InvalidCastException("Validation failed due to invalid data being provided to the validator for conversion.");
            }

            // Compare against our secondary property and the senders property value.
            short alternateValue;
            if (alternateProperty != null &&
                short.TryParse(alternateProperty.ToString(), NumberStyles.Integer, null, out alternateValue))
            {
                return alternateValue <= convertedValueFromProperty ? null : validationMessage;
            }
            else
            {
                // Compare the value to the maximum allowed by the attribute. 
                return convertedMinimumValue <= convertedValueFromProperty ? null : validationMessage;
            }
        }

        /// <summary>
        /// Validates the minimum integer value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="alternateProperty">The alternate property.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Returns a Validation message if it fails</returns>
        private IMessage ValidateMinimumIntegerValue(object propertyValue, object alternateProperty, IMessage validationMessage)
        {
            int convertedValueFromProperty = 0;
            int convertedMinimumValue = 0;
            bool propertyConversionSucceeded = int.TryParse(propertyValue.ToString(), NumberStyles.Integer, null, out convertedValueFromProperty);
            bool valueComparisonConversionSucceeded = int.TryParse(this.MinimumValue, NumberStyles.Integer, null, out convertedMinimumValue);

            if (!propertyConversionSucceeded && !valueComparisonConversionSucceeded && alternateProperty == null)
            {
                throw new InvalidCastException("Validation failed due to invalid data being provided to the validator for conversion.");
            }

            // Compare against our secondary property and the senders property value.
            int alternateValue;
            if (alternateProperty != null &&
                int.TryParse(alternateProperty.ToString(), NumberStyles.Integer, null, out alternateValue))
            {
                return alternateValue <= convertedValueFromProperty ? null : validationMessage;
            }
            else
            {
                // Compare the value to the maximum allowed by the attribute. 
                return convertedMinimumValue <= convertedValueFromProperty ? null : validationMessage;
            }
        }

        /// <summary>
        /// Validates the minimum long value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="alternateProperty">The alternate property.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Returns a Validation message if it fails</returns>
        private IMessage ValidateMinimumLongValue(object propertyValue, object alternateProperty, IMessage validationMessage)
        {
            long convertedValueFromProperty = 0;
            long convertedMinimumValue = 0;
            bool propertyConversionSucceeded = long.TryParse(propertyValue.ToString(), NumberStyles.Integer, null, out convertedValueFromProperty);
            bool valueComparisonConversionSucceeded = long.TryParse(this.MinimumValue, NumberStyles.Integer, null, out convertedMinimumValue);

            if (!propertyConversionSucceeded && !valueComparisonConversionSucceeded && alternateProperty == null)
            {
                throw new InvalidCastException("Validation failed due to invalid data being provided to the validator for conversion.");
            }

            // Compare against our secondary property and the senders property value.
            long alternateValue;
            if (alternateProperty != null &&
                long.TryParse(alternateProperty.ToString(), NumberStyles.Integer, null, out alternateValue))
            {
                return alternateValue <= convertedValueFromProperty ? null : validationMessage;
            }
            else
            {
                // Compare the value to the maximum allowed by the attribute. 
                return convertedMinimumValue <= convertedValueFromProperty ? null : validationMessage;
            }
        }

        /// <summary>
        /// Validates the minimum float value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="alternateProperty">The alternate property.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Returns a Validation message if it fails</returns>
        private IMessage ValidateMinimumFloatValue(object propertyValue, object alternateProperty, IMessage validationMessage)
        {
            float convertedValueFromProperty = 0;
            float convertedMinimumValue = 0;
            bool propertyConversionSucceeded = float.TryParse(propertyValue.ToString(), NumberStyles.Float, null, out convertedValueFromProperty);
            bool valueComparisonConversionSucceeded = float.TryParse(this.MinimumValue, NumberStyles.Float, null, out convertedMinimumValue);

            if (!propertyConversionSucceeded && !valueComparisonConversionSucceeded && alternateProperty == null)
            {
                throw new InvalidCastException("Validation failed due to invalid data being provided to the validator for conversion.");
            }

            // Compare against our secondary property and the senders property value.
            float alternateValue;
            if (alternateProperty != null &&
                float.TryParse(alternateProperty.ToString(), NumberStyles.Float, null, out alternateValue))
            {
                return alternateValue <= convertedValueFromProperty ? null : validationMessage;
            }
            else
            {
                // Compare the value to the maximum allowed by the attribute. 
                return convertedMinimumValue <= convertedValueFromProperty ? null : validationMessage;
            }
        }

        /// <summary>
        /// Validates the minimum double value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="alternateProperty">The alternate property.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Returns a Validation message if it fails</returns>
        private IMessage ValidateMinimumDoubleValue(object propertyValue, object alternateProperty, IMessage validationMessage)
        {
            double convertedValueFromProperty = 0;
            double convertedMinimumValue = 0;
            bool propertyConversionSucceeded = double.TryParse(propertyValue.ToString(), NumberStyles.Number, null, out convertedValueFromProperty);
            bool valueComparisonConversionSucceeded = double.TryParse(this.MinimumValue, NumberStyles.Number, null, out convertedMinimumValue);

            if (!propertyConversionSucceeded && !valueComparisonConversionSucceeded && alternateProperty == null)
            {
                throw new InvalidCastException("Validation failed due to invalid data being provided to the validator for conversion.");
            }

            // Compare against our secondary property and the senders property value.
            double alternateValue;
            if (alternateProperty != null &&
                double.TryParse(alternateProperty.ToString(), NumberStyles.Number, null, out alternateValue))
            {
                return alternateValue <= convertedValueFromProperty ? null : validationMessage;
            }
            else
            {
                // Compare the value to the maximum allowed by the attribute. 
                return convertedMinimumValue <= convertedValueFromProperty ? null : validationMessage;
            }
        }

        /// <summary>
        /// Validates the minimum decimal value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="alternateProperty">The alternate property.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>Returns a Validation message if it fails</returns>
        private IMessage ValidateMinimumDecimalValue(object propertyValue, object alternateProperty, IMessage validationMessage)
        {
            decimal convertedValueFromProperty = 0;
            decimal convertedMinimumValue = 0;
            bool propertyConversionSucceeded = decimal.TryParse(propertyValue.ToString(), NumberStyles.Number, null, out convertedValueFromProperty);
            bool valueComparisonConversionSucceeded = decimal.TryParse(this.MinimumValue, NumberStyles.Number, null, out convertedMinimumValue);

            if (!propertyConversionSucceeded && !valueComparisonConversionSucceeded && alternateProperty == null)
            {
                throw new InvalidCastException("Validation failed due to invalid data being provided to the validator for conversion.");
            }

            // Compare against our secondary property and the senders property value.
            decimal alternateValue;
            if (alternateProperty != null &&
                decimal.TryParse(alternateProperty.ToString(), NumberStyles.Number, null, out alternateValue))
            {
                return alternateValue <= convertedValueFromProperty ? null : validationMessage;
            }
            else
            {
                // Compare the value to the maximum allowed by the attribute. 
                return convertedMinimumValue <= convertedValueFromProperty ? null : validationMessage;
            }
        }
    }
}

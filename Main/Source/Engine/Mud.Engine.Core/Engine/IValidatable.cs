using Mud.Engine.Core.Engine.ValidationRules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// Provides a contract to objects wanting to support data validation.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Registers an objects properties so that its validation Messages are accessible for observers to access.
        /// </summary>
        /// <param name="propertyName">The name of the property you want to register.</param>
        void RegisterProperty(params string[] propertyName);

        /// <summary>
        /// Adds the supplied validation message to the ValidationMessages collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        void AddValidationMessage(IMessage message, string property = "");

        /// <summary>
        /// Removes the validation message from the ValidationMessages collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        void RemoveValidationMessage(IMessage message, string property = "");

        /// <summary>
        /// Removes all of the validation messages associated to the supplied property from the ValidationMessages collection.
        /// </summary>
        /// <param name="property">The property this validation was performed against.</param>
        void RemoveValidationMessages(string property = "");

        /// <summary>
        /// Determines whether the specified property contains any validation messages.
        /// If no property is specified, then the method will check all properties and return true if the instance has any validation messages, regardless of type.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">You must specify a property name when invoking HasValidationMessages.</exception>
        bool HasValidationMessages(string property = "");

        /// <summary>
        /// Determines whether the object has any validation message Type's matching T for the the specified property.
        /// </summary>
        /// <typeparam name="T">A Type implementing IValidationMessage</typeparam>
        /// <param name="property">The property this validation was performed against.</param>
        /// <returns></returns>
        bool HasValidationMessages<T>(string property = "") where T : IMessage, new();

        /// <summary>
        /// Determines whether the object has any validation message matching the type specified for the the given property.
        /// If no property is specified, then the method will check all properties and return true if the instance has any validation messages of given Type
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        /// <returns>
        /// Returns true if this instance's ValidationMessages collection contains the Type specified.
        /// </returns>
        bool HasValidationMessages(Type messageType, string property = "");

        /// <summary>
        /// Gets the validation messages for a given property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Returns a collection of validation messages for the given property.</returns>
        IEnumerable<IMessage> GetValidationMessages(string property);

        /// <summary>
        /// Gets the validation messages for all of the properties..
        /// </summary>
        /// <returns>Returns a key-value dictionary. The key represents the property and the value represents a collection of validation messages.</returns>
        Dictionary<string, IEnumerable<IMessage>> GetValidationMessages();
        
        void PerformValidation(IValidationRule rule, string property, IValidatable validationProxy = null);

        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <param name="validationDelegate">The validation delegate.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="validationProxy">The validation proxy.</param>
        /// <returns>
        /// Returns a validation message if the validation failed. Otherwise, null is returned.
        /// </returns>
        IMessage ValidateProperty(Func<bool> validationDelegate, IMessage failureMessage, string propertyName = "", IValidatable validationProxy = null);
    }
}

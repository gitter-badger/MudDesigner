using Mud.Engine.Core.Engine.ValidationRules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// An implementation of IValidatable and INotifyPropertyChanged that provides a base class for validating instances with property attributes imlementing IValidationRule.
    /// </summary>
    public class AttributeValidationBase : IValidatable
    {
        /// <summary>
        /// The ValidationMessages backing field.
        /// </summary>
        private Dictionary<string, ICollection<IValidationMessage>> validationMessages;

        private readonly Type validationStorageContainer;

        /// <summary>
        /// The property validation cache
        /// </summary>
        private static readonly Dictionary<Type, Dictionary<PropertyInfo, IEnumerable<IValidationRule>>> PropertyValidationCache
            = new Dictionary<Type, Dictionary<PropertyInfo, IEnumerable<IValidationRule>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeValidationBase"/> class.
        /// </summary>
        /// <param name="storageContainer">The storage container type to use for validation messages.</param>
        public AttributeValidationBase(ICollection<IValidationMessage> validationStorageContainer)
        {
            this.validationMessages = new Dictionary<string, ICollection<IValidationMessage>>();
            this.validationStorageContainer = validationStorageContainer.GetType();

            this.SetupValidation();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>
        /// The validation messages.
        /// </value>
        public Dictionary<string, ICollection<IValidationMessage>> ValidationMessages
        {
            get
            {
                return this.validationMessages;
            }

            private set
            {
                this.validationMessages = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Registers a property so observers can accessing its ValidationMessages, even when zero exist.
        /// Any property that contains an attribute implementing IValidationRule will be automatically registered.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void RegisterProperty(params string[] propertyName)
        {
            foreach (string property in propertyName)
            {
                if (!this.ValidationMessages.ContainsKey(property))
                {
                    this.ValidationMessages[property] = Activator.CreateInstance(this.validationStorageContainer.GetType()) as ICollection<IValidationMessage>;
                }
            }
        }

        /// <summary>
        /// Determines whether the object has any validation message Type's matching T for the the specified property.
        /// If no property is specified, then the method will check all properties and return true if the instance has any validation messages of type T
        /// </summary>
        /// <typeparam name="T">A Type implementing IValidationMessage</typeparam>
        /// <param name="property">The property this validation was performed against.</param>
        /// <returns>
        /// Returns true if this instance's ValidationMessages collection contains the Type specified.
        /// </returns>
        public bool HasValidationMessageType<T>(string property = "") where T : IValidationMessage, new()
        {
            if (string.IsNullOrEmpty(property) || !this.ValidationMessages.ContainsKey(property))
            {
                return this.ValidationMessages.Values.Any(collection => collection.Any(item => item is T));
            }

            return this.ValidationMessages.ContainsKey(property) &&
                this.ValidationMessages[property].Any(collection => collection is T);
        }

        /// <summary>
        /// Determines whether the object has any validation message matching the type specified for the the given property.
        /// If no property is specified, then the method will check all properties and return true if the instance has any validation messages of given Type
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        /// <returns>
        /// Returns true if this instance's ValidationMessages collection contains the Type specified.
        /// </returns>
        public bool HasValidationMessageType(Type messageType, string property = "")
        {
            if (string.IsNullOrEmpty(property) || !this.ValidationMessages.ContainsKey(property))
            {
                return this.ValidationMessages.Values.Any(collection => collection.Any(item => item.GetType() == messageType));
            }

            return this.ValidationMessages.ContainsKey(property) &&
                this.ValidationMessages[property].Any(collection => collection.GetType() == messageType);
        }

        /// <summary>
        /// Determines whether the specified property contains any validation messages.
        /// If no property is specified, then the method will check all properties and return true if the instance has any validation messages, regardless of type.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">You must specify a property name when invoking HasValidationMessages.</exception>
        public bool HasValidationMessages(string property = "")
        {
            if (string.IsNullOrEmpty(property) || !this.ValidationMessages.ContainsKey(property))
            {
                return this.ValidationMessages.Values.Any(collection => collection.Any());
            }

            return this.ValidationMessages.ContainsKey(property) &&
                this.ValidationMessages[property].Any();
        }

        /// <summary>
        /// Adds the supplied validation message to the ValidationMessages collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        public void AddValidationMessage(IValidationMessage message, string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentOutOfRangeException("property", "You must supply a property name when adding a new validation message to this instance.");
            }

            // If the key does not exist, then we create one.
            if (!this.ValidationMessages.ContainsKey(property))
            {
                this.ValidationMessages[property] = Activator.CreateInstance(this.validationStorageContainer.GetType()) as ICollection<IValidationMessage>;
            }

            if (this.ValidationMessages[property].Any(msg => msg.Message.Equals(message.Message) || msg == message))
            {
                return;
            }

            this.ValidationMessages[property].Add(message);
        }

        /// <summary>
        /// Removes all of the current validation messages across all of the instances properties..
        /// </summary>
        public void RemoveValidationMessages()
        {
            foreach (KeyValuePair<string, ICollection<IValidationMessage>> pair in this.ValidationMessages)
            {
                pair.Value.Clear();
            }
        }

        /// <summary>
        /// Removes the validation message from the ValidationMessages collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        public void RemoveValidationMessage(string message, string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            if (!this.validationMessages.ContainsKey(property))
            {
                return;
            }

            if (this.validationMessages[property].Any(msg => msg.Message.Equals(message)))
            {
                // Remove the error from the key's collection.
                this.validationMessages[property].Remove(
                    this.validationMessages[property].FirstOrDefault(msg => msg.Message.Equals(message)));
            }
        }

        /// <summary>
        /// Removes the validation message from the ValidationMessages collection.
        /// </summary>
        /// <param name="property">The property this validation was performed against.</param>
        public void RemoveValidationMessages(string property)
        {
            if (!string.IsNullOrEmpty(property) && this.ValidationMessages.ContainsKey(property))
            {
                // Remove all validation messages for the property if a message isn't specified.
                this.ValidationMessages[property].Clear();
            }
        }

        /// <summary>
        /// Runs the required validation rules against all applicable properties.
        /// </summary>
        /// <param name="validationProxy">The validation proxy.</param>
        public virtual void ValidateAll(IValidatable validationProxy = null)
        {
            this.RemoveValidationMessages();
            Dictionary<PropertyInfo, IEnumerable<IValidationRule>> cache = PropertyValidationCache[this.GetType()];

            foreach(KeyValuePair<PropertyInfo, IEnumerable<IValidationRule>> pair in cache)
            {
                foreach(IValidationRule rule in pair.Value)
                {
                    this.PerformValidation(rule, pair.Key, validationProxy);
                }
            }

            this.OnPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Runs all of the required validation rules for the specified property.
        /// If no property is specified, then the instance validates all properties registered for validation.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="validationProxy">The validation proxy.</param>
        public void ValidateProperty(string propertyName = "", IValidatable validationProxy = null)
        {
            // If no property is provided, we assume we are to validate everything.
            if (string.IsNullOrEmpty(propertyName))
            {
                this.ValidateAll(validationProxy);
                return;
            }

            this.RemoveValidationMessages(propertyName);
            var cache = AttributeValidationBase.PropertyValidationCache[this.GetType()];
            PropertyInfo property = cache.Keys.FirstOrDefault(p => p.Name.Equals(propertyName));

            if (property == null)
            {
                return;
            }

            foreach(IValidationRule rule in cache[property])
            {
                this.PerformValidation(rule, property, validationProxy);
            }

            this.OnPropertyChanged(string.Empty);
        }        
        
        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <param name="validationDelegate">The validation delegate.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <param name="propertyName"></param>
        /// <returns>
        /// Returns a validation message if the validation failed. Otherwise, null is returned.
        /// </returns>
        public IValidationMessage ValidateProperty(Func<string, IValidationMessage> validationDelegate, string failureMessage, string propertyName = "")
        {
            IValidationMessage result = validationDelegate(failureMessage);
            if (result != null)
            {
                this.AddValidationMessage(result, propertyName);
            }
            else
            {
                this.RemoveValidationMessage(failureMessage, propertyName);
            }

            return result;
        }

        /// <summary>
        /// Executes the validation rule supplied for the specified property.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="property">The property.</param>
        /// <param name="validationProxy">The validation proxy.</param>
        public void PerformValidation(IValidationRule rule, string property, IValidatable validationProxy = null)
        {
            PropertyInfo propertyInfo = null;
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("PerformValidation requires a registered property to be specified.");
            }
            else
            {
                propertyInfo = AttributeValidationBase.PropertyValidationCache[this.GetType()]
                    .FirstOrDefault(kv => kv.Key.Name.Equals(property)).Key;
            }

            if (propertyInfo == null)
            {
                throw new ArgumentNullException("PerformValidation requires a registered property to be specified.");
            }

            if (validationProxy != null && validationProxy is AttributeValidationBase)
            {
                var proxy = validationProxy as AttributeValidationBase;
                proxy.PerformValidation(rule, propertyInfo);
            }
            IValidationMessage result = rule.Validate(propertyInfo, this);
            if (result != null)
            {
                this.AddValidationMessage(result, propertyInfo.Name);
            }

            this.OnPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Refreshes the validation on the given property.
        /// </summary>
        /// <param name="property">The property.</param>
        public void RefreshValidation(string property)
        {
            if (!string.IsNullOrEmpty(property) && this.HasValidationMessages(property))
            {
                this.ValidateProperty(property);
            }
        }

        /// <summary>
        /// Sets the property only if it needs too.
        /// </summary>
        /// <typeparam name="T">The type is inferred by the oldValue parameter</typeparam>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Returns true if the property was changed and the Property Changed event was fired.
        /// </returns>
        public bool SetPropertyByReference<T>(ref T oldValue, T newValue, string propertyName) where T : class
        {
            if (oldValue != null && oldValue.Equals(newValue))
            {
                return false;
            }

            oldValue = newValue;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Called when the specified property is changed.
        /// If no property is specified, then all registered PropertyChanged event handlers are invoked.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void OnPropertyChanged(string propertyName = "")
        {
            // Grab a local reference. This allows us to access it across several threads
            // without needing to lock it.
            var handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }

            handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Executes the validation rule supplied for the specified property.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="property">The property.</param>
        /// <param name="validationProxy">The validation proxy.</param>
        private void PerformValidation(IValidationRule rule, PropertyInfo property, IValidatable validationProxy = null)
        {
            if (validationProxy != null && validationProxy is AttributeValidationBase)
            {
                var proxy = validationProxy as AttributeValidationBase;
                proxy.PerformValidation(rule, property);
            }

            IValidationMessage result = null;
            try
            {
                result = rule.Validate(property, this);
            }
            catch (Exception)
            {
                throw;
            }

            if (result != null)
            {
                this.AddValidationMessage(result, property.Name);
            }
        }

        /// <summary>
        /// Setups the validation, caching validation rules and properties as needed for re-use by other objects.
        /// </summary>
        private void SetupValidation()
        {
            // We instance a cache of property info's and validation rules. If any other Type is instanced that matches ours,
            // we won't need to use reflection to obtain it's members again. We will just hit the cache.
            var cache = new Dictionary<PropertyInfo, IEnumerable<IValidationRule>>();

            if (!AttributeValidationBase.PropertyValidationCache.ContainsKey(this.GetType()))
            {
                IEnumerable<PropertyInfo> propertiesToValidate = this.GetType().GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(ValidationAttribute), true).Any());

                // Loop through all property info's and build a collection of validation rules for each property.
                foreach (PropertyInfo property in propertiesToValidate)
                {
                    cache.Add(
                        property,
                        property.GetCustomAttributes(typeof(ValidationAttribute), true).Select(a => a) as IEnumerable<ValidationAttribute>);
                }

                AttributeValidationBase.PropertyValidationCache[this.GetType()] =
                    new Dictionary<PropertyInfo, IEnumerable<IValidationRule>>(cache);
            }
            else
            {
                cache = AttributeValidationBase.PropertyValidationCache[this.GetType()];
            }

            // Register each property for this instance once we are done caching.
            foreach(PropertyInfo property in cache.Keys)
            {
                this.RegisterProperty(property.Name);
            }
        }
    }
}

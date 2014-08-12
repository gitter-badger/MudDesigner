using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// An implementation of IValidatable and INotifyPropertyChanged for validation and property change tracking via method delegates.
    /// </summary>
    public class ValidatableBase : IValidatable, INotifyPropertyChanged
    {
        /// <summary>
        /// The ValidationMessages backing field.
        /// </summary>
        private Dictionary<string, ICollection<IMessage>> validationMessages;

        private readonly Type storageContainer;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public ValidatableBase(ICollection<IMessage> validationStorageContainer)
        {
            this.storageContainer = validationStorageContainer.GetType();
            this.validationMessages = new Dictionary<string, ICollection<IMessage>>();
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>
        /// The validation messages.
        /// </value>
        public Dictionary<string, ICollection<IMessage>> ValidationMessages
        {
            get
            {
                return this.validationMessages;
            }

            private set
            {
                this.validationMessages = value;
                this.OnPropertyChanged("ValidationMessages");
            }
        }

        /// <summary>
        /// Registers an objects properties its validation Messages are accessible for observers to access.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        public void RegisterProperty(params string[] propertyNames)
        {
            foreach (string property in propertyNames)
            {
                if (!this.ValidationMessages.ContainsKey(property))
                {
                    this.ValidationMessages[property] = Activator.CreateInstance(this.storageContainer) as ICollection<IMessage>;
                }
            }
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
            if (string.IsNullOrEmpty(property) || !this.validationMessages.ContainsKey(property))
            {
                return this.validationMessages.Values.Any(collection => collection.Any(item => item.GetType() == messageType));
            }

            return this.validationMessages.ContainsKey(property) &&
                this.validationMessages[property].Any(collection => collection.GetType() == messageType);
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
            if (string.IsNullOrEmpty(property) || !this.validationMessages.ContainsKey(property))
            {
                return this.validationMessages.Values.Any(collection => collection.Any());
            }

            return this.validationMessages.ContainsKey(property) &&
                this.validationMessages[property].Any();
        }

        /// <summary>
        /// Determines whether the object has any validation message Type's matching T for the the specified property.
        /// </summary>
        /// <typeparam name="T">A Type implementing IValidationMessage</typeparam>
        /// <param name="property">The property this validation was performed against.</param>
        /// <returns></returns>
        public bool HasValidationMessageType<T>(string property = "") where T : IMessage, new()
        {
            if (string.IsNullOrEmpty(property))
            {
                bool result = this.validationMessages.Values.Any(collection => collection.Any(msg => msg is T));
                return result;
            }

            return this.validationMessages.ContainsKey(property);
        }

        /// <summary>
        /// Gets the validation messages for a given property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// Returns a collection of validation messages for the given property.
        /// </returns>
        public IEnumerable<IMessage> GetValidationMessages(string property)
        {
            if (this.validationMessages.ContainsKey(property))
            {
                return this.validationMessages[property].ToArray();
            }

            // If no validation messages exist, return an empty collection.
            return new Collection<IMessage>();
        }

        /// <summary>
        /// Gets the validation messages for all of the properties..
        /// </summary>
        /// <returns>
        /// Returns a key-value dictionary. The key represents the property and the value represents a collection of validation messages.
        /// </returns>
        public Dictionary<string, IEnumerable<IMessage>> GetValidationMessages()
        {
            var messages = new Dictionary<string, IEnumerable<IMessage>>();

            // We have to iterate over the collection in order to conver the messages collection
            // from a ICollection type to an IEnumerable type.
            foreach (KeyValuePair<string, ICollection<IMessage>> pair in this.validationMessages)
            {
                messages.Add(pair.Key, pair.Value);
            }

            return messages;
        }

        /// <summary>
        /// Adds the supplied validation message to the ValidationMessages collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        public void AddValidationMessage(IMessage message, string property = "")
        {
            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            // If the key does not exist, then we create one.
            if (!this.validationMessages.ContainsKey(property))
            {
                this.validationMessages[property] = Activator.CreateInstance(this.storageContainer) as ICollection<IMessage>;
            }

            if (this.validationMessages[property].Any(msg => msg.Message.Equals(message.Message) || msg == message))
            {
                return;
            }

            this.validationMessages[property].Add(message);
        }

        /// <summary>
        /// Removes the validation message from the ValidationMessages collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property this validation was performed against.</param>
        public void RemoveValidationMessage(string message, string property = "")
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
        /// Removes all of the validation messages associated to the supplied property from the ValidationMessages collection.
        /// </summary>
        /// <param name="property">The property this validation was performed against.</param>
        public void RemoveValidationMessages(string property = "")
        {
            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            if (!this.validationMessages.ContainsKey(property))
            {
                return;
            }

            this.validationMessages[property].Clear();
            this.validationMessages.Remove(property);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public virtual void Validate()
        {
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
        public IMessage ValidateProperty(Func<string, IMessage> validationDelegate, string failureMessage, string propertyName = "")
        {
            IMessage result = validationDelegate(failureMessage);
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
        /// Called when the specified property is changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

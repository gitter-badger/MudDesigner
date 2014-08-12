using System;
namespace Mud.Engine.Core.Engine
{
    public interface IAttributeValidationBase
    {
        void AddValidationMessage(IMessage message, string property);
        System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<IMessage>> GetValidationMessages();
        System.Collections.Generic.IEnumerable<IMessage> GetValidationMessages(string property);
        bool HasValidationMessages(string property = "");
        bool HasValidationMessages(Type messageType, string property = "");
        bool HasValidationMessages<TMessage>(string property = "") where TMessage : IMessage, new();
        void PerformValidation(Mud.Engine.Core.Engine.ValidationRules.IValidationRule rule, string property, IValidatable validationProxy = null);
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void RefreshValidation(string property);
        void RegisterProperty(params string[] propertyName);
        void RemoveValidationMessage(IMessage message, string property);
        void RemoveValidationMessages();
        void RemoveValidationMessages(string property);
        bool SetPropertyByReference<T>(ref T oldValue, T newValue, string propertyName) where T : class;
        void ValidateAll();
        IMessage ValidateProperty(Func<bool> validationDelegate, IMessage failureMessage, string propertyName = "", IValidatable validationProxy = null);
        void ValidateProperty(string propertyName = "");
        event EventHandler<ValidationChangedEventArgs> ValidationChanged;
    }
}

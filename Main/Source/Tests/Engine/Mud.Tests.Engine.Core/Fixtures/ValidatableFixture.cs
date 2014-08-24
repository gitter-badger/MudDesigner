using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mud.Engine.Core.Engine.Validation;
using System.Reflection;

namespace Mud.Tests.Engine.Core.Fixtures
{
    public class ValidatableFixture : ValidatableBase
    {
        private const string PasswordConfirmationDelegateName = "ConfirmPasswordsMatch";

        public ValidatableFixture()
        {
            this.Name = string.Empty;
            this.Password = string.Empty;
            this.PasswordConfirmation = string.Empty;
        }


        [ValidateValueIsNotNullOrEmpty(ValidationMessageType = typeof(MessageFixture), FailureMessage = "Name must be set.")]
        public string Name { get; set; }


        [ValidateValueIsNotNullOrEmpty(ValidationMessageType = typeof(MessageFixture), FailureMessage = "Password must be set.")]
        [ValidateStringIsGreaterThan(GreaterThanValue = 4, ValidationMessageType = typeof(MessageFixture), FailureMessage = "Password must be greater than 4 characters.")]
        public string Password { get; set; }


        [ValidateWithCustomHandler(DelegateName = PasswordConfirmationDelegateName, ValidationMessageType = typeof(MessageFixture), FailureMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; }


        [ValidationCustomHandlerDelegate(DelegateName = PasswordConfirmationDelegateName)]
        public IMessage PasswordConfirmationValidation(IMessage message, PropertyInfo property)
        {
            return this.PasswordConfirmation.Equals(this.Password) ?
                null :
                message;
        }
    }
}

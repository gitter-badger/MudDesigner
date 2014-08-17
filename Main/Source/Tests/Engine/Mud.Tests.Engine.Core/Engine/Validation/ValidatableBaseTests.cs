namespace Mud.Tests.Engine.Core.Engine.Validation
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Mud.Engine.Core.Engine;
    using Mud.Engine.Core.Engine.Validation;
    using Mud.Tests.Engine.Core.Fixtures;

    /// <summary>
    /// Tests against the ValidatableBase class.
    /// </summary>
    [TestClass]
    public class ValidatableBaseTests
    {
        /// <summary>
        /// Tests that the ValidatableBase can add validation message to its internal collection.
        /// </summary>
        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_CanAddValidationMessage_ReturnsCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            // Act
            validatableBase.AddValidationMessage(message, "FakeProperty");

            // Assert
            Assert.IsNotNull(validatableBase.GetValidationMessages(), "Validation message collection returned null.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages().Count == 1,
                "Validation message collection did not return the correct number of elements.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages("FakeProperty").Count() == 1,
                "Validation message collection did not return the correct number of elements for the specified property."); 

        }

        /// <summary>
        /// Validates that the ValidatableBase can remove all validation messages from its internal collection.
        /// </summary>
        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_CanRemoveAllMessages_ReturnsEmptyCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            validatableBase.AddValidationMessage(message, "FakeProperty");
            validatableBase.AddValidationMessage(message, "SecondaryProperty");

            // Act
            validatableBase.RemoveValidationMessages();

            // Assert
            Assert.IsNotNull(validatableBase.GetValidationMessages(), "Validation message collection returned null.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages().Any(),
                "Validation message collection returned elements when it should not have.");
        }

        /// <summary>
        /// Validates that the ValidatableBase can remove validation messages for a specific property from its internal collection.
        /// </summary>
        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_CanRemoveAllMessagesFromProperty_ReturnsCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            validatableBase.AddValidationMessage(message, "FakeProperty");
            validatableBase.AddValidationMessage(message, "SecondaryProperty");

            // Act
            validatableBase.RemoveValidationMessages("SecondaryProperty");

            // Assert
            Assert.IsNotNull(validatableBase.GetValidationMessages(), "Validation message collection returned null.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages().Count == 2,
                "Validation message collection did not return the correct number of elements. The original collection should still exist, remaining empty.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages("FakeProperty").Count() == 1,
                "Validation message collection was missing an expected element.");
            Assert.IsFalse(validatableBase.GetValidationMessages("SecondaryProperty").Any(),
                "Validatable base contained a message that was supposed to have been removed.");
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_CanRemoveSingleMessageFromProperty_ReturnsEmptyCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            validatableBase.AddValidationMessage(message, "FakeProperty");
            validatableBase.AddValidationMessage(message, "SecondaryProperty");

            // Act
            validatableBase.RemoveValidationMessage(message, "FakeProperty");

            // Assert
            Assert.IsNotNull(validatableBase.GetValidationMessages(), "Validation message collection returned null.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages().Count == 2,
                "Validation message collection did not return the correct number of elements. The original collection should still exist, remaining empty.");
            Assert.IsTrue(
                validatableBase.GetValidationMessages("SecondaryProperty").Count() == 1,
                "Validation message collection was missing an expected element.");
            Assert.IsFalse(validatableBase.GetValidationMessages("FakeProperty").Any(),
                "Validatable base contained a message that was supposed to have been removed.");
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_PropertyHasValidationMessages_ReturnsEmptyCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            validatableBase.AddValidationMessage(message, "FakeProperty");

            // Act
            bool hasMessages = validatableBase.HasValidationMessages("FakeProperty");

            Assert.IsTrue(hasMessages, "Property did not contain any validation messages.");
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_PropertyHasValidationMessagesByType_ReturnsEmptyCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            validatableBase.AddValidationMessage(message, "FakeProperty");

            // Act
            bool hasMessages = validatableBase.HasValidationMessages(message.GetType(), "FakeProperty");

            Assert.IsTrue(hasMessages, "Property did not contain any validation messages.");
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_PropertyHasValidationMessagesByGeneric_ReturnsEmptyCollection()
        {
            // Arrange
            var validatableBase = new ValidatableBase();
            var messageFixture = new MessageFixture { Message = "Fixture message." };
            var mockMessage = new Mock<IMessage>();
            mockMessage.SetupGet(property => property.Message).Returns("Validation Failed!");
            IMessage message = mockMessage.Object;

            validatableBase.AddValidationMessage(messageFixture, "FakeProperty");
            validatableBase.AddValidationMessage(message, "SecondaryProperty");

            // Act
            bool fakePropertyHasMessages = validatableBase.HasValidationMessages<MessageFixture>("FakeProperty");
            bool secondaryPropertyHasMessages = validatableBase.HasValidationMessages<MessageFixture>("SecondaryProperty");

            Assert.IsTrue(fakePropertyHasMessages, "Property did not contain any validation messages.");
            Assert.IsFalse(secondaryPropertyHasMessages, "Property contained an invalid message type.");
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_ModelValidatesAllProperties_ReturnsTrue()
        {
            // Arrange
            var model = new ValidatableFixture();

            // Act
            model.ValidateAll();

            // Assert
            Assert.IsTrue(model.HasValidationMessages());
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_ModelValidatesAllProperties_ReturnsFalse()
        {
            // Arrange
            var model = new ValidatableFixture();
            model.Name = "TestName";
            model.Password = "pass";
            model.PasswordConfirmation = "pass";

            // Act
            model.ValidateAll();

            // Assert
            Assert.IsFalse(model.HasValidationMessages());
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_ModelValidatesProperty_ReturnsFalse()
        {
            // Arrange
            var model = new ValidatableFixture();
            model.Name = "TestName";
            model.Password = "pass";
            model.PasswordConfirmation = "pass";

            // Act
            model.ValidateProperty("Password");

            // Assert
            Assert.IsFalse(model.HasValidationMessages());
        }

        [TestMethod]
        [TestCategory("Engine Validation")]
        public void ValidatableBase_CustomValidationEnforced_ReturnsMessage()
        {
            // Arrange
            var model = new ValidatableFixture();
            model.Name = "TestName";
            model.Password = "pass";
            model.PasswordConfirmation = "pass";

            // Act
            IMessage result = model.ValidateProperty(
                () => string.IsNullOrEmpty(model.PasswordConfirmation),
                new MessageFixture("Password Confirmation can not be blank."),
                "PasswordConfirmation");

            // Assert
            Assert.IsNotNull(result, "Validation failed.");
        }
    }
}

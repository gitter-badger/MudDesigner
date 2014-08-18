using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mud.Engine.Core.Engine.Validation;
using Mud.Engine.Core.Engine;
using System.Reflection;

namespace Mud.Tests.Engine.Core.Engine.Validation
{
    [TestClass]
    public class ValidateNumberHasMaximumValueTests
    {
        [ValidateNumberHasMaximumValue(MaximumValue = "5", FailureMessage = "Value must be greater than 5.", ValidationMessageType = typeof(ErrorMessage))]
        public int TestInt { get; set; }

        [ValidateNumberHasMaximumValue(MaximumValue = "5", FailureMessage = "Value must be greater than 5.", ValidationMessageType = typeof(ErrorMessage))]
        public float TestFloat { get; set; }

        [ValidateNumberHasMaximumValue(MaximumValue = "5", FailureMessage = "Value must be greater than 5.", ValidationMessageType = typeof(ErrorMessage))]
        public float TestDouble { get; set; }

        [TestMethod]
        public void ValidateNumberHasMaxValue_AsInteger_ReturnsNull()
        {
            // Arrange
            var rule = new ValidateNumberHasMaximumValueAttribute();
            PropertyInfo property = this.GetType().GetProperty(this.GetPropertyName(p => p.TestInt));
            this.TestInt = 6;
        }
    }
}

using System;

namespace Mud.Engine.Core.Engine.ValidationRules
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ValidationCustomHandlerDelegate : Attribute
    {
        public string DelegateName { get; set; }
    }
}

using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine.ValidationRules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationRule
    {
        Type ValidationMessageType { get; set; }

        string FailureMessage { get; set; }

        IMessage Validate(PropertyInfo property, IValidatable sender);
    }
}

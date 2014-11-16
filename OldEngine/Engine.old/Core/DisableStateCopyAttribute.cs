using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Attribute that can be applied at the property scope. 
    /// Prevents copying the value from one object's property to another.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property)]
    public class DisableStateCopyAttribute : System.Attribute
    {
    }
}

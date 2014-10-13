namespace Mud.Engine.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Informs the data store services that a Property value must be persisted.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PersistValueAttribute : Attribute
    {
    }
}

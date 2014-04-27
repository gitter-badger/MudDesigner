using System;

namespace Mud.DataAccess
{
    /// <summary>
    /// Indicates that the designated property needs to be ignored when its owning class is being persisted to storage.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }
}

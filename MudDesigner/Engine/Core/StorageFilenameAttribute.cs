using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Any property decorated with a FilenameAttribute will have the property value used as the objects
    /// file name when stored using an IPersistedStorage object that stores objects to disk.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StorageFilenameAttribute : Attribute
    {
        // Empty by design.
    }
}

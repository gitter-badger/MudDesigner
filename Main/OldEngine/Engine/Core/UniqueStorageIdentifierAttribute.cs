using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Used to specify what property in the class is designated as the unique Id.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UniqueStorageIdentifierAttribute: Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueStorageIdentifierAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public UniqueStorageIdentifierAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the name of the property that is used as a Unique ID for the class attached to this attribute.
        /// </summary>
        public string PropertyName { get; private set; }
    }
}

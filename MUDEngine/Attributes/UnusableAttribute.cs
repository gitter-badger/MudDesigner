using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class UnusableAttribute : System.Attribute
    {
        private bool _IsUseable;
        public UnusableAttribute (bool useable)
        {
            _IsUseable = useable;
        }

        /// <summary>
        /// Sets if the class can be instanced or not. Regardless of what Type it inherits from
        /// </summary>
        public bool IsUseable
        {
            get;
            set;
        }
    }
}

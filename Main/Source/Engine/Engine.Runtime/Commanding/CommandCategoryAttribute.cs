using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Runtime.Commanding
{
    public sealed class CommandCategoryAttribute : Attribute
    {
        public CommandCategoryAttribute(string category)
        {
            this.Category = category;
        }

        public string Category { get; private set; }
    }
}

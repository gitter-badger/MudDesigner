using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
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

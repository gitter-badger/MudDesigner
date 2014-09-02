using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
{
    public sealed class CommandVariationsAttribute : Attribute
    {
        public CommandVariationsAttribute(params string[] variation)
        {
            this.CommandVariations = new List<string>(variation);
        }

        public IEnumerable<string> CommandVariations { get; private set; }
    }
}

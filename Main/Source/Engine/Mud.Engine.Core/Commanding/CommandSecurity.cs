using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
{
    public class CommandSecurity
    {
        public IPermission Permission { get; set; }

        public ICollection<ICommand> Commands { get; set; }
    }
}

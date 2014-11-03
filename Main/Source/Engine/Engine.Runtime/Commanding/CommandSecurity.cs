using Mud.Engine.Shared.Commanding;
using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Runtime.Commanding
{
    public class CommandSecurity
    {
        public IPermission Permission { get; set; }

        public ICollection<ICommand> Commands { get; set; }
    }
}

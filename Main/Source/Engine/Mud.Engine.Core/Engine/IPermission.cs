using Mud.Engine.Core.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    public interface IPermission
    {
        string Name { get; }

        ICollection<ICommand> AvailableCommands { get; set; }

        IPermission Parent { get; set; }
    }
}

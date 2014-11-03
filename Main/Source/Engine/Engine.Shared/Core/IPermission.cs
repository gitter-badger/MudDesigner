using Mud.Engine.Shared.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Core
{
    public interface IPermission
    {
        string Name { get; }

        ICollection<ICommand> AvailableCommands { get; set; }

        IPermission Parent { get; set; }
    }
}

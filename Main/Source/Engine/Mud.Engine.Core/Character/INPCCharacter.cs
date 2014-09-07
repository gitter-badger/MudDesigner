using Mud.Engine.Core.Commanding;
using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Character
{
    public interface INPCCharacter : ICharacter
    {
        List<ICommand> Commands { get; set; }
    }
}

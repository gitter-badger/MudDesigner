using Mud.Engine.Shared.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Character
{
    public interface INPCCharacter : ICharacter
    {
        List<ICommand> Commands { get; set; }
    }
}

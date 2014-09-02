using Mud.Engine.Core.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
{
    public interface ICommand
    {
        void Execute(ICharacter sender, params string[] arguments);
    }
}

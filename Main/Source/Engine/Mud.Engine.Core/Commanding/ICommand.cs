using Mud.Engine.Core.Character;
using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
{
    public interface ICommand
    {
        event EventHandler<CommandEventHandler> Executed;

        void Execute(ICharacter sender, params IMessage[] messages);

        bool CanExecute(ICharacter sender, params IMessage[] messages);
    }
}

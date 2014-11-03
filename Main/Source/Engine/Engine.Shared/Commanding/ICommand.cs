using Mud.Engine.Shared.Character;
using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Commanding
{
    public interface ICommand
    {
        event EventHandler<CommandEventHandler> Executed;

        void Execute(ICharacter sender, params IMessage[] messages);

        bool CanExecute(ICharacter sender, params IMessage[] messages);
    }
}

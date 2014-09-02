using Mud.Engine.Core.Character;
using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
{
    public class CommandManager : ICommandManager
    {
        private IEnumerable<ICommand> commands;

        private ICharacter character;

        public async Task Initialize(ICharacter character)
        {

        }

        public void ExecuteCommand(IMessage message)
        {
            throw new NotImplementedException();
        }

        public string[] GetCommandHistory()
        {
            throw new NotImplementedException();
        }
    }
}

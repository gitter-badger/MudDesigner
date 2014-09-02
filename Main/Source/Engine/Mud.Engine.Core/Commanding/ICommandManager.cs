using Mud.Engine.Core.Character;
using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
{
    public interface ICommandManager
    {
        Task Initialize(ICharacter character);

        void ExecuteCommand(IMessage message);

        string[] GetCommandHistory();
    }
}

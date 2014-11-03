using Mud.Engine.Shared.Character;
using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Commanding
{
    public interface ICommandManager
    {
        Task Initialize(ICharacter character);

        Task HandleMessage(IMessage message);

        string[] GetCommandHistory();
    }
}
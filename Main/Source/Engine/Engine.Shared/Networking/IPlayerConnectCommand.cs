using Mud.Engine.Shared.Character;
using Mud.Engine.Shared.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Networking
{
    public interface IPlayerConnectCommand : ICommand
    {
        IPlayer LoadedPlayer { get; }

        void Initialize<TPlayer>() where TPlayer : class, IPlayer, new();
    }
}

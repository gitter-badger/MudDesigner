using Mud.Engine.Core.Character;
using Mud.Engine.Core.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Networking
{
    public interface IPlayerConnectCommand : ICommand
    {
        IPlayer LoadedPlayer { get; }

        void Initialize<TPlayer>() where TPlayer : class, IPlayer, new();
    }
}

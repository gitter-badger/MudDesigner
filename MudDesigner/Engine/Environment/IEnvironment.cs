using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Environment
{
    public interface IEnvironment : IGameObject
    {
        void BroadcastMessage(string message);
        void BroadcastMessage(string message, List<IPlayer> playersToOmit);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Networking;
using Env = MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Game
{
    public class Game : MudDesigner.Engine.Core.Game
    {
        public int MinimumPlayerAge { get; set; }

        public override bool Initialize(IServer startedServer)
        {
            Name = "Mud Designer Default Game";
            Description = "This is the default game setup that comes shipped with the Mud Designer Game Engine!";
            HideRoomNames = true;
            MinimumPlayerAge = 0;
            Version = "Alpha 2.0";
            Website = "http://MudDesigner.Codeplex.com \n http://AllocateThis.com";

            return base.Initialize(startedServer);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Networking;
using Env = MudDesigner.Engine.Environment;
using MudDesigner.Scripts.Environment;
namespace MudDesigner.Scripts.Game
{
    public class Game : MudDesigner.Engine.Core.Game
    {
        public int MinimumPlayerAge { get; set; }

        public override bool Initialize(IServer startedServer)
        {
            return base.Initialize(startedServer);
        }
    }
}

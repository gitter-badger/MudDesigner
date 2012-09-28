using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Networking;

namespace WinPC.Engine.Core
{
    public class Game : EngineGame
    {
        public override void Initialize(IServer startedServer, IWorld world)
        {
            if (startedServer == null || world == null)
                return;

            server = startedServer;

            string worldFile = Path.Combine(@"\Worlds\", WinPC.Engine.Properties.Engine.Default.WorldFile);

            if (!World.Load(worldFile))
                World.Create("Empty World");
        }

        public override void Update()
        {
            if (server.Status == ServerStatus.Running)
            {
                World.Update();
            }
        }
    }
}

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
        public override bool Initialize(IServer startedServer)
        {
            Name = "AllocateThis! Mud Game";

            if (startedServer == null)
                return false;

            Server = startedServer;
            
            
            //TODO: Where should we Instance the IGame.World property?
            //World.Load(WinPC.Engine.Properties.Engine.Default.WorldFile);
            World = new World();

            return true;
        }

        public override void Update()
        {
            if (Server.Status == ServerStatus.Running)
            {
            }
        }
    }
}

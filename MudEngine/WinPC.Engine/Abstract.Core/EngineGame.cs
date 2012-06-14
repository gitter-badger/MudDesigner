using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPC.Engine.Abstract.Networking;

namespace WinPC.Engine.Abstract.Core
{
    public abstract class EngineGame : IGame
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        private IServer server;

        public abstract void Initialize(IServer startedServer);

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public abstract void Update();
    }
}

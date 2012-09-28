using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Core;

namespace WinPC.Engine.Abstract.Core
{
    public abstract class EngineGame : IGame
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        protected IServer server;

        public abstract void Initialize(IServer startedServer, IWorld world);

        public static IGame GetCustomGame(string className)
        {
            Type t = System.Reflection.Assembly.GetExecutingAssembly().GetType(className);
            if (t == null)
                return new Game();

            IGame customGame = (IGame)Activator.CreateInstance(t);
            return customGame;
        }

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public abstract void Update();


        public IWorld World
        {
            get { throw new NotImplementedException(); }
        }

        public bool Load(string filename)
        {
            throw new NotImplementedException();
        }

        public string Filename
        {
            get { throw new NotImplementedException(); }
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}

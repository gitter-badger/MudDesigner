using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Abstract.Objects;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Abstract.Core
{
    public abstract class EngineGame : IGame
    {
        public Dictionary<Guid, IGameObject> GameObjects { get; private set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Version { get; set; }

        protected IServer Server { get; set; }

        public IWorld World { get; set; }

        public DateTime LastSave { get; private set; }
        public string Filename { get; set; }

        protected EngineGame()
        {
            GameObjects = new Dictionary<Guid, IGameObject>();
        }


        public IGameObject GetGameObject(Guid id)
        {
            return GameObjects[id];
        }

        public void AddGameObject(IGameObject go)
        {
            GameObjects.Add(go.Id,go);
        }
        
        public abstract bool Initialize(IServer startedServer);

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

        public bool Load(string filename)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void Save(System.IO.BinaryWriter writer)
        {
            LastSave =  DateTime.Now;

            //TODO Go through each property and save 
            throw new NotImplementedException();
        }
    }
}

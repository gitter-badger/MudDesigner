//Microsoft .NET Using statements.
using System;
using System.Collections.Generic;
using System.Reflection;

//AllocateThis! Mud Designer Using statements
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Scripting;

//Abstract.Core namespace is used for Types that are the core component of the game engine such as the Game, Player and command Types
namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Provides Methods and Properties for adding objects to the game world, managing the server and maintaining the state of the game,
    /// thereby serving as a base class for all Types that run and manage the MUD Game.
    /// </summary>
    public abstract class Game : IGame
    {
        /// <summary>
        /// TODO - Michael, please expand on this more - JS.
        /// </summary>
        public Dictionary<Guid, IGameObject> GameObjects { get; private set; }

        /// <summary>
        /// The name of this game.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of this game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Version of this game.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets a reference to the game server
        /// </summary>
        public IServer Server { get; set; }

        /// <summary>
        /// Gets a reference to the game world
        /// </summary>
        public IWorld World { get; protected set; }

        /// <summary>
        /// Gets a the last time that the game was saved.
        /// </summary>
        public DateTime LastSave { get; private set; }

        protected Game()
        {
            GameObjects = new Dictionary<Guid, IGameObject>();
        }

        /// <summary>
        /// Returns a Game object that is currently loaded into the game.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IGameObject GetGameObject(Guid id)
        {
            //QUESTION - How do you know what Guid belongs to which object during runtime? - JS
            return GameObjects[id];
        }

        public void AddGameObject(IGameObject go)
        {
            GameObjects.Add(go.Id, go);
        }

        /// <summary>
        /// Sets up all of the game objects for use, loads any saved states, restores the world and links itself to the server.
        /// </summary>
        /// <param name="startedServer">A reference to a IServer Type that has had its Start() method called.</param>
        /// <param name="world">A instance of a IWorld Type.  This should be a non-restored World as the game will invoke the IWorld.Load method itself.</param>
        /// <returns></returns>
        public virtual bool Initialize(IServer startedServer, IWorld world)
        {
            Name = "AllocateThis! Mud Game";

            if (startedServer == null)
                return false;

            Server = startedServer;

            World = world;

            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());

            foreach (string assembly in MudDesigner.Engine.Properties.Engine.Default.ScriptLibrary)
            {
                ScriptFactory.AddAssembly(assembly);
            }

            return true;
        }

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void Save(System.IO.BinaryWriter writer)
        {
            LastSave = DateTime.Now;

            //TODO Go through each property and save 
            throw new NotImplementedException();
        }
    }
}

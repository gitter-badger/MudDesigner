//Microsoft .NET Using statements.
using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

//AllocateThis! Mud Designer Using statements
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Scripting;
using Newtonsoft.Json;

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
        /// TODO - I dont know if we need this, GameObjects shouldlive in the World not outside of it.... - MC 
        /// </summary>
        [Browsable(false)]
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
        /// Gets ors Sets the website for this game.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or Sets if the Rooms name will be displayed upon entering a room
        /// </summary>
        [DisplayName("Hide Room Names")]
        public bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets if the game should automatically save.
        /// </summary>
        [DisplayName("Auto-Save")]
        public bool Autosave {get;set;}

        /// <summary>
        /// Gets or Sets the frequency that the game saves when Autosave=true
        /// </summary>
        [DisplayName("Save Frequency")]
        public int SaveFrequency {get;set;}

        /// <summary>
        /// Gets a reference to the game server
        /// </summary>
        [Browsable(false)]
        public IServer Server { get; set; }

        /// <summary>
        /// Gets a reference to the game world
        /// </summary>
        [Browsable(false)]
        public IWorld World { get; protected set; }

        /// <summary>
        /// Gets a the last time that the game was saved.
        /// </summary>
        [Browsable(false)]
        public DateTime LastSave { get; private set; }

        public Game()
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
            GameObjects.Add(go.ID, go);
        }

        /// <summary>
        /// Sets up all of the game objects for use, loads any saved states, restores the world and links itself to the server.
        /// </summary>
        /// <param name="startedServer">A reference to a IServer Type that has had its Start() method called.</param>
        /// <param name="world">A instance of a IWorld Type.  This should be a non-restored World as the game will invoke the IWorld.Load method itself.</param>
        /// <returns></returns>
        public virtual bool Initialize(IServer startedServer)
        {
            Name = "AllocateThis! Mud Game";

            if (startedServer != null)
            {
                Server = startedServer;
            }
            else
            {
                Server = new Server();
            }

            //Add the engine assembly to the Script Factory
            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());
            
            //Add any additional assemblies that might have been compiled elsewhere (downloadable assemblies)
            if (MudDesigner.Engine.Properties.EngineSettings.Default.ScriptLibrary.Count != 0)
            {
                foreach (string assembly in MudDesigner.Engine.Properties.EngineSettings.Default.ScriptLibrary)
                {
                    //Make sure the assembly actually exists first.
                    if (File.Exists(assembly))
                        ScriptFactory.AddAssembly(System.Environment.CurrentDirectory + "\\" + assembly);
                }
            }

            IWorld world = (IWorld)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.DefaultWorldType, null);
            if (world != null)
                World = world;
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

        public void Load()
        {
            var fileAndPathToSave = Path.Combine(Directory.GetCurrentDirectory(), "saves",
                                                MudDesigner.Engine.Properties.EngineSettings.Default.WorldFile);
            var path = Path.GetDirectoryName(fileAndPathToSave);

            if (path == null)
            {
                return;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(string.Format(path));
            }

            if (!File.Exists(fileAndPathToSave)) return;

            using (var br = new BinaryReader(File.Open(fileAndPathToSave, FileMode.Open)))
            {
                var gameLoad = br.ReadString();
                var settings = new JsonSerializerSettings();
                var contract = new SerializationContracts();
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                settings.TypeNameHandling = TypeNameHandling.All;
                settings.ContractResolver = contract;

                World = JsonConvert.DeserializeObject<World>(gameLoad, settings);
            }
        }

        // This 
        public void Save()
        {
            LastSave = DateTime.Now;

            var fileAndPathToSave = Path.Combine(Directory.GetCurrentDirectory(), "saves",
                                                 MudDesigner.Engine.Properties.EngineSettings.Default.WorldFile);
            var path = Path.GetDirectoryName(fileAndPathToSave);

            if (path == null)
            {
                return;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(string.Format(path));
            }

           
            using (var bw = new BinaryWriter(File.Open(fileAndPathToSave, FileMode.OpenOrCreate)))
            {
                var settings = new JsonSerializerSettings();
                var contract = new SerializationContracts();

                settings.TypeNameHandling = TypeNameHandling.All;
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

                var gameSave = JsonConvert.SerializeObject(World, Formatting.Indented, settings);
                bw.Write(gameSave);
            }
        }
    }
}

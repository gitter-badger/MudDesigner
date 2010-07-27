//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;

namespace MudEngine.GameManagement
{
    /// <summary>
    /// Manages all of the projects settings.
    /// </summary>
    [XmlInclude(typeof(StartingLocation))]
    [XmlInclude(typeof(Currency))]
    public class Game
    {
        public enum TimeOfDayOptions
        {
            AlwaysDay,
            AlwaysNight,
            Transition,
        }

        [Browsable(false)]
        public bool IsRunning { get; internal set; }

        [Category("Company Settings")]
        [Description("The name of the Company or Author building the game.")]
        /// <summary>
        /// Gets or Sets the name of the company
        /// </summary>
        public string CompanyName { get; set; }

        [Category("Company Settings")]
        [Description("The website URL that a player can visit to view additional information related to the game")]
        /// <summary>
        /// Gets or Sets the companies website for this project
        /// </summary>
        public string Website { get; set; }

        [Category("Project Settings")]
        [Description("The name of the game displayed to the users, and title bar of the runtime.")]
        public string GameTitle { get; set; }

        [Category("Project Settings")]
        [Description("Enable or Disable Auto-saving of players when the player travels")]
        /// <summary>
        /// Gets or Sets if the game autosaves when the player changes locations.
        /// </summary>
        public bool AutoSave { get; set; }

        [Category("Project Settings")]
        [Description("Hide Room names from being outputted to the console.")]
        /// <summary>
        /// Gets or Sets if room names are hidden during console output.
        /// </summary>
        public bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets what time of day the world is currently in.
        /// </summary>
        [Category("Day Management")]
        [Description("Set what time of day the world will take place in.")]
        public TimeOfDayOptions TimeOfDay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets how long in minutes it takes to transition from day to night.
        /// </summary>
        [Category("Day Management")]
        [Description("Set how long in minutes it takes to transition from day to night.")]
        public int TimeOfDayTransition
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets how long in minutes a day lasts in the game world.
        /// </summary>
        [Category("Day Management")]
        [Description("Sets how long in minutes a day lasts in the game world.")]
        public int DayLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the current working version of the game.
        /// </summary>
        [Category("Project Settings")]
        [Description("The current working version of the game.")]
        public string Version { get; set; }

        [Category("Game Currency")]
        [DefaultValue(1)]
        [Description("Sets the amount that the base currency is valued at.")]
        public uint BaseCurrencyAmount { get; set; }


        [Category("Game Currency")]
        [DefaultValue("Copper")]
        public string BaseCurrencyName { get; set; }

        //TODO: Add Party support.

        /// <summary>
        /// Gets or Sets if all objects will be laoded during server startup. Enabling this results in a slower server start time, but faster object access.
        /// </summary>
        [Category("Project Settings")]
        [Description("If enabled, all objects will be loaded during server startup resulting in a slower server start time, but faster load time during gameplay")]
        public bool PreCacheObjects { get; set; }

        [Browsable(false)]
        public List<Currency> CurrencyList { get; set; }

        /// <summary>
        /// Gets or Sets the path to the current project
        /// </summary>
        [Browsable(false)]
        public string ProjectPath { get; set; }

        [Category("Environment Settings")]
        [ReadOnly(true)]
        public Realm InitialRealm
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the collection of Realms currently stored in the Game.
        /// </summary>
        public List<Realm> RealmCollection { get; private set; }

        [Browsable(false)]
        public string Story
        {
            get;
            set;
        }

        [Category("Object Setup")]
        public string Filename
        {
            get
            {
                return _Filename;
            }
        }
        private string _Filename;

        public Game()
        {
            CurrencyList = new List<Currency>();
            scriptEngine = new Scripting.ScriptEngine();
            RealmCollection = new List<Realm>();
            //PlayerCollection = new List<BaseCharacter>();

            GameTitle = "New Game";
            _Filename = "Game.xml";
            BaseCurrencyAmount = 1;
            BaseCurrencyName = "Copper";
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public bool Start()
        {
            //Setup the scripting engine
            scriptEngine.Initialize();
            scriptEngine.ScriptPath = "Scripts";
            scriptEngine.ScriptExtension = ".mud";
            
            //Load our scripts library
            if (System.IO.File.Exists("Scripts.dll"))
            {
                Assembly assem = Assembly.LoadFile("Scripts.dll");

                foreach (Type t in assem.GetTypes())
                {
                    if (t.BaseType.Name == "BaseCharacter")
                    {
                        Scripting.GameObject obj = new Scripting.GameObject();
                        obj = scriptEngine.GetObject(t.Name);
                        //PlayerCollection.Add((BaseCharacter)obj.Instance);
                    }
                }
            }

            //Load all of the engine commands
            CommandEngine.LoadAllCommands();

            //See if we have an Initial Realm set
            foreach (Realm r in RealmCollection)
            {
                if (r.IsInitialRealm)
                {
                    InitialRealm = r;
                    break;
                }
            }

            if (InitialRealm == null)
            {
                Log.Write("ERROR: No initial realm set, un-able to finish starting of Game");
                return false;
            }

            //Start the Telnet server
            this.StartServer();

            IsRunning = true;

            return true;
        }

        public void Shutdown()
        {
            //Place ending code here for game shut down.
            //TODO: Save content on shutdown.
            Server.EndServer();

            IsRunning = false;
        }

        public void Save(string filename)
        {
            string directory = Path.GetDirectoryName(filename);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            FileManager.Save(filename, this);
        }

        public object Load(string path)
        {
            string fileName = Path.Combine(path, _Filename);
            if (!File.Exists(fileName))
            {
                return null;
            }
            
            return FileManager.Load(fileName, this);
        }

        public void AddRealm(Realm realm)
        {
            if (realm.IsInitialRealm)
            {
                foreach (Realm r in RealmCollection)
                {
                    if (r.IsInitialRealm)
                    {
                        r.IsInitialRealm = false;
                        break;
                    }
                }
            }

            if (realm.IsInitialRealm)
                InitialRealm = realm;

            //TODO: Check for duplicate Realms.
            RealmCollection.Add(realm);
        }

        public Realm GetRealm(string realmName)
        {
            foreach (Realm realm in RealmCollection)
            {
                if (realm.Name == realmName)
                    return realm;
            }

            return null;
        }

        //TODO: This should be internal only; C# property using get; internal set; so only MudEngine.dll may edit this collection
        //public List<BaseCharacter> PlayerCollection;
        public BaseCharacter[] PlayerCollection;

        public MudEngine.Networking.Server Server { get; internal set; }
        public ProtocolType ServerType = ProtocolType.Tcp;
        public int ServerPort = 555;
        public int MaximumPlayers = 1000;

        private Scripting.ScriptEngine scriptEngine;

        private void StartServer()
        {
            //PlayerCollection = new List<BaseCharacter>(MaximumPlayers);
            PlayerCollection = new BaseCharacter[MaximumPlayers];
            for (int i = 0; i < MaximumPlayers; i++)
                PlayerCollection[i] = new BaseCharacter(this);
            Server = new Networking.Server();
            Server.Initialize(ServerPort, ref PlayerCollection);
            Server.Start();
        }
    }
}

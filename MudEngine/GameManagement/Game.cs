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
using MudEngine.Scripting;

namespace MudEngine.GameManagement
{
    /// <summary>
    /// Manages all of the projects settings.
    /// </summary>
    [XmlInclude(typeof(StartingLocation))]
    [XmlInclude(typeof(Currency))]
    public class Game
    {
        #region ==== Properties & Types ====
        #region Custom Types
        public enum TimeOfDayOptions
        {
            AlwaysDay,
            AlwaysNight,
            Transition,
        }

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
        #endregion

        #region Game Object Setup
        /// <summary>
        /// Gets or Sets if this game is running in debug mode. Additional information is sent to the log if enabled.
        /// </summary>
        public static bool IsDebug { get; set; }

        /// <summary>
        /// Gets or Sets if the game will run with a server or not.
        /// </summary>
        public bool IsMultiplayer { get; set; }

        /// <summary>
        /// Gets or Sets if this game is currently running.
        /// </summary>
        [Browsable(false)]
        public bool IsRunning { get; internal set; }

        /// <summary>
        /// Gets or Sets if all objects will be laoded during server startup. Enabling this results in a slower server start time, but faster object access.
        /// </summary>
        [Category("Project Settings")]
        [Description("If enabled, all objects will be loaded during server startup resulting in a slower server start time, but faster load time during gameplay")]
        public bool PreCacheObjects { get; set; }

        /// <summary>
        /// Gets or Sets the path to the current project
        /// </summary>
        [Browsable(false)]
        public string ProjectPath { get; set; }

        /// <summary>
        /// Gets or Sets the paths to various project related objects.
        /// </summary>
        public SaveDataPaths DataPaths { get; set; }

        /// <summary>
        /// Gets the scripting engine used by the game.
        /// </summary>
        public ScriptEngine scriptEngine { get; internal set; }
        #endregion

        #region Game Information
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

        /// <summary>
        /// Gets or Sets the current working version of the game.
        /// </summary>
        [Category("Project Settings")]
        [Description("The current working version of the game.")]
        public string Version { get; set; }

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
        /// Gets or Sets how long in minutes it takes to transition from day to night.
        /// </summary>
        [Category("Day Management")]
        [Description("Set how long in minutes it takes to transition from day to night.")]
        public int TimeOfDayTransition { get; set; }

        /// <summary>
        /// Gets or Sets how long in minutes a day lasts in the game world.
        /// </summary>
        [Category("Day Management")]
        [Description("Sets how long in minutes a day lasts in the game world.")]
        public int DayLength { get; set; }

        [Category("Game Currency")]
        [DefaultValue(1)]
        [Description("Sets the amount that the base currency is valued at.")]
        public uint BaseCurrencyAmount { get; set; }


        [Category("Game Currency")]
        [DefaultValue("Copper")]
        public string BaseCurrencyName { get; set; }
        
        [Browsable(false)]
        public List<Currency> CurrencyList { get; set; }

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

        /// <summary>
        /// The Story that is displayed on initial player entry into the game
        /// </summary>
        public string Story { get; set; }
        #endregion

        #region Networking
        //TODO: This should be internal only; C# property using get; internal set; so only MudEngine.dll may edit this collection
        /// <summary>
        /// Collection of players currently running on the server.
        /// </summary>
        internal BaseCharacter[] PlayerCollection;

        /// <summary>
        /// Gets the current running Server object.
        /// </summary>
        public MudEngine.Networking.Server Server { get; internal set; }

        /// <summary>
        /// Gets or Sets the protocol used by the server.
        /// </summary>
        public ProtocolType ServerType { get; set; }

        /// <summary>
        /// Gets or Sets the port that the server will run on
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of Players permitted to run on this Games server.
        /// </summary>
        public int MaximumPlayers { get; set; }
        #endregion
        #endregion

        #region ==== Constructors ====
        public Game()
        {
            //Instance all of the Games Objects.
            CurrencyList = new List<Currency>();
            scriptEngine = new Scripting.ScriptEngine(this);
            RealmCollection = new List<Realm>();

            //Prepare the Save Paths for all of our Game objects.
            SaveDataPaths paths = new SaveDataPaths();
            paths.Environment = FileManager.GetDataPath(SaveDataTypes.Realms);
            paths.Players = FileManager.GetDataPath(SaveDataTypes.Player);
            DataPaths = paths;

            //Setup default settings for the Game
            GameTitle = "New Game";
            BaseCurrencyAmount = 1;
            BaseCurrencyName = "Copper";
            Version = "1.0";
            Story = "";

            //Setup default Networking settings
            IsMultiplayer = true;
            ServerType = ProtocolType.Tcp;
            ServerPort = 555;
            MaximumPlayers = 1000;
        }
        #endregion

        #region ==== Methods ====
        /// <summary>
        /// Starts the game and runs the server if IsMultiplayer is true
        /// </summary>
        public bool Start()
        {
            Log.Write("Starting Game...");
            //Setup the scripting engine and load our script library
            scriptEngine.Initialize();
            
            Log.Write("Loading internal game commands...");
            //Loads the MudEngine Game Commands
            CommandEngine.LoadBaseCommands();

            //Ensure custom commands are loaded until everything is fleshed out.
            if (Game.IsDebug)
            {
                foreach (string command in CommandEngine.GetCommands())
                {
                    Log.Write("Command loaded: " + command);
                }
            }

            Log.Write("Initializing location...");
            //See if we have an Initial Realm set
            foreach (Realm r in RealmCollection)
            {
                if (r.IsInitialRealm)
                {
                    InitialRealm = r;
                    break;
                }
            }

            //Check if any the initial room exists or not.
            if ((InitialRealm == null) || (InitialRealm.InitialZone == null) || (InitialRealm.InitialZone.InitialRoom == null))
            {
                Log.Write("ERROR: No initial location defined. Game startup failed!");
                return false;
            }
            else
                Log.Write("Initial Location defined -> " + InitialRealm.Name + "." + InitialRealm.InitialZone.Name + "." + InitialRealm.InitialZone.InitialRoom.Name);

            Log.Write("Starting Server...");
            //Start the Telnet server
            if (IsMultiplayer)
                this.StartServer();

            //Game is running now.
            IsRunning = true;

            Log.Write("Game startup complete.");
            return true;
        }

        /// <summary>
        /// Shuts down the Game and Server.
        /// </summary>
        public void Shutdown()
        {
            Log.Write("Server shutdown requested...");
            
            //Place ending code here for game shut down.
            //TODO: Save content on shutdown.

            if (IsMultiplayer)
                Server.EndServer();

            IsRunning = false;

            Log.Write("Shutdown completed...");
        }

        /// <summary>
        /// Adds a Realm to the Games current list of Realms.
        /// </summary>
        /// <param name="realm"></param>
        public void AddRealm(Realm realm)
        {
            //If this Realm is set as Initial then we need to disable any previously
            //set Realms to avoid conflict.
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

            //Set this Realm as the Games initial Realm
            if (realm.IsInitialRealm)
                InitialRealm = realm;

            //TODO: Check for duplicate Realms.
            RealmCollection.Add(realm);
        }

        /// <summary>
        /// Gets a Realm currently stored in the Games collection of Realms.
        /// </summary>
        /// <param name="realmName"></param>
        /// <returns></returns>
        public Realm GetRealm(string realmName)
        {
            foreach (Realm realm in RealmCollection)
            {
                if (realm.Name == realmName)
                    return realm;
            }

            return null;
        }

        /// <summary>
        /// Starts the Server.
        /// </summary>
        private void StartServer()
        {
            //This is handled by the Game() Constructor
            //PlayerCollection = new List<BaseCharacter>(MaximumPlayers);
            PlayerCollection = new BaseCharacter[MaximumPlayers];
            for (int i = 0; i < MaximumPlayers; i++)
                PlayerCollection[i] = new BaseCharacter(this);
            Server = new Networking.Server();
            Server.Initialize(ServerPort, ref PlayerCollection);
            Server.Start();
        }
        #endregion
    }
}

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
        /// Gets or Sets the paths to various project related objects.
        /// </summary>
        public SaveDataPaths DataPaths { get; set; }

        /// <summary>
        /// Gets the scripting engine used by the game.
        /// </summary>
        public ScriptEngine scriptEngine { get; internal set; }

        /// <summary>
        /// Gets or Sets the path to the current project
        /// </summary>
        [Browsable(false)]
        public string ProjectPath { get; set; }

        /// <summary>
        /// Gets or Sets if all objects will be laoded during server startup. Enabling this results in a slower server start time, but faster object access.
        /// </summary>
        [Category("Project Settings")]
        [Description("If enabled, all objects will be loaded during server startup resulting in a slower server start time, but faster load time during gameplay")]
        public bool PreCacheObjects { get; set; }

        /// <summary>
        /// Gets a copy of all identifiers being used in the game.
        /// </summary>
        internal List<Int32> ObjectIdentifierCollection { get; private set; }
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

        [Category("Game Currency")]
        [DefaultValue(1)]
        [Description("Sets the amount that the base currency is valued at.")]
        public uint BaseCurrencyAmount { get; set; }


        [Category("Game Currency")]
        [DefaultValue("Copper")]
        public string BaseCurrencyName { get; set; }

        public GameTime WorldTime { get; set; }
        #endregion

        #region Networking
        //TODO: This should be internal only; C# property using get; internal set; so only MudEngine.dll may edit this collection
        /// <summary>
        /// Collection of players currently running on the server.
        /// </summary>
        public BaseCharacter[] PlayerCollection;

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
            WorldTime = new GameTime(this);

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

            //Setup the player arrays
            //used to be in Start
            PlayerCollection = new BaseCharacter[MaximumPlayers];
            for (int i = 0; i < MaximumPlayers; i++)
                PlayerCollection[i] = new BaseCharacter(this);

            GameTime.Time t = new GameTime.Time();
            t.Hour = 8;
            t.Minute = 0;
            t.Second = 0;
            t.Day = 1;
            t.Month = 1;
            t.Year = 2010;
            WorldTime.InitialGameTime = t;
            WorldTime.DaysPerMonth = 7;
            WorldTime.MonthsPerYear = 12;
            WorldTime.HoursPerDay = 23;
            WorldTime.MinutesPerHour = 59;
            WorldTime.SecondsPerMinute = 59;
        }
        #endregion

        #region ==== Methods ====
        /// <summary>
        /// Starts the game and runs the server if IsMultiplayer is true
        /// </summary>
        public virtual bool Start()
        {
            Log.Write("Game Initializing...");
            if (!Directory.Exists(DataPaths.Players))
                Directory.CreateDirectory(DataPaths.Players);

            //First, compile and execute all of the script files.
            scriptEngine.ScriptType = ScriptEngine.ScriptTypes.SourceFiles;
            scriptEngine.Initialize();
            
            //Next, load any pre-compiled libraries.
            scriptEngine.ScriptType = ScriptEngine.ScriptTypes.Assembly;
            scriptEngine.Initialize();

            /*
             * If a custom player script is loaded in the script engine, then the base commands are 
             * loaded when the script is instanced automatically. If  there is no script then these
             * don't get loaded and will need to be forced.
             * This prevents a duplicate "Loading Commands" message in the server console if the 
             * player script exists and pre-loads the commands during script instancing in ScriptEngine.Initialize()
             */
            Log.Write("Initializing Command Engine...");
            if ((CommandEngine.CommandCollection == null) || (CommandEngine.CommandCollection.Count == 0))
                CommandEngine.LoadBaseCommands();

            if (IsDebug)
            {
                foreach (string command in CommandEngine.CommandCollection.Keys)
                    Log.Write("Command Loaded: " + command);
            }
             
            //See if we have an Initial Realm set
            //TODO: Check for saved Realm files and load
            Log.Write("Initializing World...");
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
                Log.Write("Players will start in the Abyss. Each player will contain their own instance of this room.");
                //return false;
            }
            else
                Log.Write("Initial Location loaded-> " + InitialRealm.Name + "." + InitialRealm.InitialZone.Name + "." + InitialRealm.InitialZone.InitialRoom.Name);

            //Start the Telnet server
            if (IsMultiplayer)
            {
                Log.Write("Starting Server...");
                this.StartServer();
            }
            else //Not multiplayer so we change the save locations
            {
                SaveDataPaths paths = new SaveDataPaths("World", "Saved");
                DataPaths = paths;
                PlayerCollection[0].Initialize();
            }

            WorldTime.Initialize();

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

            if (IsMultiplayer)
                Server.EndServer();
            
            Save();

            IsRunning = false;

            Log.Write("Shutdown completed...");
        }

        public virtual void Update()
        {
            WorldTime.Update();
        }

        public virtual void Save()
        {
            Log.Write("Saving Game world....");

            Log.Write("Saving Game Players...");
            for (int i = 0; i <= PlayerCollection.Length - 1; i++)
            {
                if (PlayerCollection[i].Name == "New BaseCharacter")
                    continue;

                Log.Write("Saving " + PlayerCollection[i].Name);
                PlayerCollection[i].Save(Path.Combine(DataPaths.Players, PlayerCollection[i].Filename));
            }

            //Delete the last saved version of the World. We will dump the current version onto disk.
            if (Directory.Exists(DataPaths.Environment))
                Directory.Delete(DataPaths.Environment, true);

            //Re-create the environment directory
            Directory.CreateDirectory(DataPaths.Environment);

            //Loop through each Realm and save it.
            for (int x = 0; x <= RealmCollection.Count - 1; x++)
            {
                string realmFile = Path.Combine(DataPaths.Environment, RealmCollection[x].Filename);

                //Save the Realm
                RealmCollection[x].Save(realmFile);

                //Loop through each Zone in the Realm and save it.
                for (int y = 0; y <= RealmCollection[x].ZoneCollection.Count - 1; y++)
                {
                    string zonePath = Path.Combine(DataPaths.Environment, Path.GetFileNameWithoutExtension(RealmCollection[x].Filename), Path.GetFileNameWithoutExtension(RealmCollection[x].ZoneCollection[y].Filename));

                    if (!Directory.Exists(zonePath))
                        Directory.CreateDirectory(zonePath);

                    //Save the Zone.
                    RealmCollection[x].ZoneCollection[y].Save(Path.Combine(zonePath, RealmCollection[x].ZoneCollection[y].Filename));

                    for (int z = 0; z <= RealmCollection[x].ZoneCollection[y].RoomCollection.Count - 1; z++)
                    {
                        if (!Directory.Exists(Path.Combine(zonePath, "Rooms")))
                            Directory.CreateDirectory(Path.Combine(zonePath, "Rooms"));

                        string roomPath = Path.Combine(zonePath, "Rooms");

                        RealmCollection[x].ZoneCollection[y].RoomCollection[z].Save(Path.Combine(roomPath, RealmCollection[x].ZoneCollection[y].RoomCollection[z].Filename));
                    }
                }
            }
        }

        public virtual void Load()
        {
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
        public List<Realm> GetRealmByName(string realmName)
        {
            List<Realm> realms = new List<Realm>();

            foreach (Realm realm in RealmCollection)
            {
                if (realm.Name == realmName)
                    realms.Add(realm);
            }

            return realms;
        }

        public Realm GetRealmByID(Guid id)
        {
            foreach (Realm r in RealmCollection)
            {
                if (r.ID == id)
                    return r;
            }

            return null;
        }

        /// <summary>
        /// Starts the Server.
        /// </summary>
        private void StartServer()
        {
            Server = new Networking.Server();
            Server.Initialize(ServerPort, ref PlayerCollection);
            Server.Start();
        }
        #endregion
    }
}

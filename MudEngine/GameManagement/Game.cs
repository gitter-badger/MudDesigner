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
using MudEngine.Networking;

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
        public static Boolean IsDebug { get; set; }

        /// <summary>
        /// Gets or Sets if the game will run with a server or not.
        /// </summary>
        public Boolean IsMultiplayer { get; set; }

        /// <summary>
        /// Gets or Sets if this game is currently running.
        /// </summary>
        [Browsable(false)]
        public Boolean IsRunning { get; internal set; }

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
        public String ProjectPath { get; set; }

        /// <summary>
        /// Gets or Sets if all objects will be laoded during server startup. Enabling this results in a slower server start time, but faster object access.
        /// </summary>
        [Category("Project Settings")]
        [Description("If enabled, all objects will be loaded during server startup resulting in a slower server start time, but faster load time during gameplay")]
        public Boolean PreCacheObjects { get; set; }

        /// <summary>
        /// Gets a copy of all identifiers being used in the game.
        /// </summary>
        internal List<BaseObject> WorldObjects { get; private set; }
        #endregion

        #region Game Information
        [Category("Company Settings")]
        [Description("The name of the Company or Author building the game.")]
        /// <summary>
        /// Gets or Sets the name of the company
        /// </summary>
        public String CompanyName { get; set; }

        [Category("Company Settings")]
        [Description("The website URL that a player can visit to view additional information related to the game")]
        /// <summary>
        /// Gets or Sets the companies website for this project
        /// </summary>
        public String Website { get; set; }

        [Category("Project Settings")]
        [Description("The name of the game displayed to the users, and title bar of the runtime.")]
        public String GameTitle { get; set; }

        /// <summary>
        /// Gets or Sets the current working version of the game.
        /// </summary>
        [Category("Project Settings")]
        [Description("The current working version of the game.")]
        public String Version { get; set; }
        
        [Browsable(false)]
        public List<Currency> CurrencyList { get; set; }

        [Category("Environment Settings")]
        [ReadOnly(true)]
        public Realm InitialRealm
        {
            get;
            internal set;
        }

        /// <summary>
        /// The Story that is displayed on initial player entry into the game
        /// </summary>
        public String Story { get; set; }

        [Category("Project Settings")]
        [Description("Enable or Disable Auto-saving of players when the player travels")]
        /// <summary>
        /// Gets or Sets if the Game world is automatically saved at a specified interval.
        /// Players will be saved every-time they change location.
        /// </summary>
        public Boolean AutoSave { get; set; }

        /// <summary>
        /// Gets or Sets the interval in which the Game will automatically save every game object.
        /// </summary>
        public Int32 AutoSaveInterval { get; set; }

        /// <summary>
        /// Gets or Sets the minimum number of characters required to create a 'legal' password.
        /// Default value is 6 characters required for a password during character creation.
        /// </summary>
        public Int32 MinimumPasswordSize { get; set; }

        [Category("Project Settings")]
        [Description("Hide Room names from being outputted to the console.")]
        /// <summary>
        /// Gets or Sets if room names are hidden during console output.
        /// </summary>
        public Boolean HideRoomNames { get; set; }

        [Category("Game Currency")]
        [DefaultValue(1)]
        [Description("Sets the amount that the base currency is valued at.")]
        public Int32 BaseCurrencyAmount { get; set; }


        [Category("Game Currency")]
        [DefaultValue("Copper")]
        public String BaseCurrencyName { get; set; }

        public GameTime WorldTime { get; set; }

        public GameWorld World { get; set; }
        #endregion

        #region Networking
        /// <summary>
        /// Collection of players currently running on the server.
        /// </summary>
        protected BaseCharacter[] PlayerCollection;

        /// <summary>
        /// Gets the current running Server object.
        /// </summary>
        public Server Server { get; internal set; }

        /// <summary>
        /// Gets or Sets the protocol used by the server.
        /// </summary>
        public ProtocolType ServerType { get; set; }

        /// <summary>
        /// Gets or Sets the port that the server will run on
        /// </summary>
        public Int32 ServerPort { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of Players permitted to run on this Games server.
        /// </summary>
        public Int32 MaximumPlayers { get; set; }
        #endregion
        #endregion

        #region ==== Constructors ====
        public Game()
        {
            //Instance all of the Games Objects.
            CurrencyList = new List<Currency>();
            scriptEngine = new Scripting.ScriptEngine(this);
            World = new GameWorld(this);
            WorldTime = new GameTime(this);

            //Prepare the Save Paths for all of our Game objects.
            DataPaths = new SaveDataPaths("World", "Player");

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
            for (Int32 i = 0; i < MaximumPlayers; i++)
                PlayerCollection[i] = new BaseCharacter(this);

            AutoSave = true;
            AutoSaveInterval = 30;

            MinimumPasswordSize = 6;
        }
        #endregion

        #region ==== Methods ====
        /// <summary>
        /// Starts the game and runs the server if IsMultiplayer is true
        /// </summary>
        public virtual Boolean Start()
        {
            Log.Write("Game Initializing...");
            if (!Directory.Exists(DataPaths.Players))
                Directory.CreateDirectory(DataPaths.Players);

            //Load both pre-compiled and file based scripts
            scriptEngine.ScriptType = ScriptEngine.ScriptTypes.Both;
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
                foreach (String command in CommandEngine.CommandCollection.Keys)
                    Log.Write("Command Loaded: " + command);
            }

            World.Start();
           
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

            //Load the game and world if it was previously saved.
            Load();

            Log.Write("Game startup complete.");
            return true;
        }

        /// <summary>
        /// Shuts down the Game and Server.
        /// </summary>
        public virtual void Shutdown()
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

            DateTime serverTime = new DateTime();
            DateTime systemTime = DateTime.Now;

            Int32 lastSaveGap = 0;

            WorldTime.Update();
            
            if (lastSaveGap == AutoSaveInterval)
            {
                if (AutoSave)
                    Save();
                lastSaveGap = 0;
            }

            //ServerTime holds the last minute prior to our current minute.
            if (serverTime.Minute != DateTime.Now.Minute)
            {
                serverTime = DateTime.Now;
                lastSaveGap++;
            }


            if (IsMultiplayer)
            {
                Console.Write(Log.GetMessages());
                Log.FlushMessages();
                System.Threading.Thread.Sleep(1);
            }
            else
            {
                PlayerCollection[0].ExecuteCommand(Console.ReadLine());
            }
        }

        public virtual void Save()
        {
            Log.Write("Saving Game world....");

            Log.Write("Saving Game Players...");
            for (Int32 i = 0; i <= PlayerCollection.Length - 1; i++)
            {
                if (PlayerCollection[i].Name == "New BaseCharacter")
                    continue;
                PlayerCollection[i].ExecuteCommand("Save");
            }

            //Delete the last saved version of the World. We will dump the current version onto disk.
            if (Directory.Exists(DataPaths.Environment))
                Directory.Delete(DataPaths.Environment, true);

            //Re-create the environment directory
            Directory.CreateDirectory(DataPaths.Environment);

            //Save the Game World.
            World.Save();

            //Save the Game
            String filename = GameTitle + ".ini";

            if (File.Exists(filename))
                File.Delete(filename);

            FileManager.WriteLine(filename, this.AutoSave.ToString(), "AutoSave");
            FileManager.WriteLine(filename, this.AutoSaveInterval.ToString(), "AutoSaveInterval");
            FileManager.WriteLine(filename, this.BaseCurrencyAmount.ToString(), "BaseCurrencyAmount");
            FileManager.WriteLine(filename, this.BaseCurrencyName, "BaseCurrencyName");
            FileManager.WriteLine(filename, this.CompanyName, "CompanyName");
            FileManager.WriteLine(filename, this.DataPaths.Environment, "DataPathEnvironment");
            FileManager.WriteLine(filename, this.DataPaths.Players, "DataPathPlayers");
            FileManager.WriteLine(filename, this.GameTitle, "GameTitle");
            FileManager.WriteLine(filename, this.HideRoomNames.ToString(), "HideRoomNames");
            FileManager.WriteLine(filename, this.InitialRealm.Filename, "InitialRealm");
            FileManager.WriteLine(filename, this.IsMultiplayer.ToString(), "IsMultiplayer");
            FileManager.WriteLine(filename, this.MaximumPlayers.ToString(), "MaximumPlayers");
            FileManager.WriteLine(filename, this.PreCacheObjects.ToString(), "PreCacheObjects");
            FileManager.WriteLine(filename, this.ServerPort.ToString(), "ServerPort");
            FileManager.WriteLine(filename, this.ServerType.ToString(), "ServerType");
            FileManager.WriteLine(filename, this.Version, "Version");
            FileManager.WriteLine(filename, this.Website, "Website");

            //TODO: Save WorldTime
            //TODO: Save Story
            //TODO: Save Server Information
            //TODO: Save Currency Lists
            //TODO: Save Script Engine information
        }

        public virtual void Load()
        {
            String filename = GameTitle + ".ini";

            if (!File.Exists(filename))
                return;

            Log.Write("Restoring Game Settings...");
            this.AutoSave = Convert.ToBoolean(FileManager.GetData(filename, "AutoSave"));
            this.AutoSaveInterval = Convert.ToInt32(FileManager.GetData(filename, "AutoSaveInterval"));
            this.BaseCurrencyAmount = Convert.ToInt32(FileManager.GetData(filename, "BaseCurrencyAmount"));
            this.BaseCurrencyName = FileManager.GetData(filename, "BaseCurrencyName");
            this.CompanyName = FileManager.GetData(filename, "CompanyName");
            this.DataPaths = new SaveDataPaths(FileManager.GetData(filename, "DataPathEnvironment"), FileManager.GetData(filename, "DataPathPlayers"));
            this.GameTitle = FileManager.GetData(filename, "GameTitle");
            this.HideRoomNames = Convert.ToBoolean(FileManager.GetData(filename, "HideRoomNames"));
            this.InitialRealm = new Realm(this);
            this.IsMultiplayer = Convert.ToBoolean(FileManager.GetData(filename, "IsMultiplayer"));
            this.MaximumPlayers = Convert.ToInt32(FileManager.GetData(filename, "MaximumPlayers"));
            this.PreCacheObjects = Convert.ToBoolean(FileManager.GetData(filename, "PreCacheObjects"));
            this.ServerPort = Convert.ToInt32(FileManager.GetData(filename, "ServerPort"));
            this.Version = FileManager.GetData(filename, "Version");
            this.Website = FileManager.GetData(filename, "Webite");

            //Need to re-assign the enumerator value that was previously assigned to the ServerType property
            Array values = Enum.GetValues(typeof(ProtocolType));
            foreach (Int32 value in values)
            {
                //Since enum values are not strings, we can't simply just assign the String to the enum
                String displayName = Enum.GetName(typeof(ProtocolType), value);

                //If the value = the String saved, then perform the needed conversion to get our data back
                if (displayName.ToLower() == FileManager.GetData(filename, "ServerType").ToLower())
                {
                    ServerType = (ProtocolType)Enum.Parse(typeof(ProtocolType), displayName);
                    break;
                }
            }

            //Restore the world.
            Log.Write("Restoring World Environments...");
            World.Load();

            Log.Write("Game Restore complete.");
        }

        public BaseCharacter[] GetPlayerCollection()
        {
            return PlayerCollection;
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

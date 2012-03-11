using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using MudEngine.Networking;
using MudEngine.Core;
using MudEngine.Game.Characters;
using MudEngine.DAL;
using MudEngine.Game.Environment;
using MudEngine.Scripting;
using MudEngine.Core.Interfaces;

namespace MudEngine.Game
{
    /// <summary>
    /// StandardGame will be the base of all Mud Games created with the engine.
    /// It manages all of the game components including the Server and the Game World.
    /// </summary>
    public class StandardGame
    {
        /// <summary>
        /// Gets or Sets the Name of this game.
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// Gets or Sets the website where this game can be played at.
        /// </summary>
        public String Website { get; set; }

        /// <summary>
        /// Gets or Sets the Description of this game.  This is often displayed upon first connection.
        /// Note that this is displayed BEFORE the Servers MOTD.
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Gets or Sets the Version of this game.
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// Gets or Sets if Room Names will be shown to the player each time they travel to a new Room.
        /// </summary>
        public Boolean HiddenRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets if this Game is currently being played on a Server
        /// </summary>
        public Boolean Multiplayer { get; set; }

        /// <summary>
        /// Gets or Sets the minimum password size required for user account passwords
        /// </summary>
        public Int32 MinimumPasswordSize { get; set; }

        /// <summary>
        /// Get or Sets if the game will automatically save the world.  For servers with poor specifications, this can be disabled to
        /// help with performance.  Manually saving the world will be required.
        /// </summary>
        public Boolean AutoSave { get; set; }

        /// <summary>
        /// Gets if the game is currently running or not.
        /// </summary>
        public Boolean Enabled { get; protected set; }

        /// <summary>
        /// Gets or Sets if the game is in debug more or not.
        /// </summary>
        public Boolean Debugging { get; set; }

        /// <summary>
        /// Gets a reference to the currently running Server.
        /// </summary>
        public Server Server { get; protected set; }

        /// <summary>
        /// Gets a reference to the current Game World
        /// </summary>
        public World World { get; protected set; }

        /// <summary>
        /// Gets or Sets the paths that content is saved to.
        /// </summary>
        public DataPaths SavePaths { get; set; }

        public ScriptFactory ScriptFactory { get; set; }

        /// <summary>
        /// StandardGame constructor.  If no Port number is provided, 4000 is used.
        /// </summary>
        /// <param name="name"></param>
        public StandardGame(String name) : this(name, 4000)
        {
        }

        /// <summary>
        /// StandardGame constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="port"></param>
        public StandardGame(String name, Int32 port)
        {
            this.Name = name;
            this.Website = "http://scionwest.net";
            this.Description = "A sample Mud game created using the Mud Designer kit.";
            this.Version = "1.0";
            this.Multiplayer = true;
            this.MinimumPasswordSize = 8;
            this.AutoSave = true;

            //Setup default save paths.
            this.SavePaths = new DataPaths();
            this.SavePaths.SetRelativePath("Data", DataTypes.Root);

            //Setup our server.
            this.Server = new Server(this, port);

            //Instance our World
            this.World = new World(this);
        }

        /// <summary>
        /// Runs a script compiler and scans for custom scripts that
        /// inherit StandardGame and then returns them.  This provides
        /// support for custom Game rules via Script.
        /// </summary>
        /// <returns></returns>
        public StandardGame Initialize()
        {
            //Instance a new compiler
            CompileEngine compiler = new CompileEngine();
            Logger.WriteLine("Checking for custom Game scripts.");

            compiler.AddAssemblyReference(Path.Combine(this.SavePaths.GetPath(DataTypes.Root), Assembly.GetExecutingAssembly().Location));

            Boolean result = compiler.Compile(this.SavePaths.GetPath(DataTypes.Scripts));

            if (result)
            {
                ScriptFactory factory = new ScriptFactory(compiler.CompiledAssembly);
                StandardGame game = (StandardGame)factory.FindInheritedScripted("StandardGame", "Mud Game");

                if (game == null)
                {
                    Logger.WriteLine("No custom Game rules located.  Defaulting to Standard setup.");
                    return null;
                }
                else
                {
                    Logger.WriteLine("Located " + game.GetType().Name + " ruleset.");
                    return game;
                }
            }
            else
                Logger.WriteLine("Failed to perform startup compilation! " + compiler.Errors);
            return null;

        }

        /// <summary>
        /// Starts the game by getting all of the game scripts, loading the world
        /// loading all of the games commands and starting the server.
        /// </summary>
        /// <param name="maxPlayers"></param>
        /// <param name="maxQueueSize"></param>
        /// <returns></returns>
        public virtual Boolean Start(Int32 maxPlayers, Int32 maxQueueSize)
        {
            Logger.WriteLine("Starting up " + this.Name);

            //Instance Script Engine
            Logger.WriteLine("Preparing script engine...");
            CompileEngine compiler = new CompileEngine("cs");
            //compiler.AddAssemblyReference(Assembly.GetExecutingAssembly().FullName);
            compiler.AddAssemblyReference(Path.Combine(this.SavePaths.GetPath(DataTypes.Root), Assembly.GetExecutingAssembly().Location));
            
            //Compile any scripts
            Logger.WriteLine("Compiling game scripts.");
            Boolean result = compiler.Compile(this.SavePaths.GetPath(DataTypes.Scripts));
            if (!result)
            {
                Logger.WriteLine(compiler.Errors, Logger.Importance.Error);

                //Fail safe in the event compiling fails.  Just use this assembly.
                this.ScriptFactory = new ScriptFactory(Assembly.GetExecutingAssembly());
            }
            else
            {
                Logger.WriteLine("Compilation completed.");
                this.ScriptFactory = new ScriptFactory(compiler.CompiledAssembly);
            }

            //Load the default engine Commands
            Logger.WriteLine("Loading internal game commands.");
            CommandSystem.LoadCommands();
            if (CommandSystem.Commands.Count > 0)
            {
                foreach (ICommand command in CommandSystem.Commands.Values)
                    Logger.WriteLine("Loaded Command: " + command.Name);
            }
            else
                Logger.WriteLine("No internal game commands located.");

            Logger.WriteLine("Loading scripted game commands.");
            ICommand[] commands = CommandSystem.LoadCommandLibrary(this.ScriptFactory.Assembly, false);
            if (commands.Length > 0)
            {
                foreach (ICommand command in commands)
                {
                    Logger.WriteLine("Loaded Command: " + command.Name);
                }
            }
            else
                Logger.WriteLine("No scripted game commands located.");

            //Load World
            this.World.Initialize();
            this.World.Load();

            //Start our server.
            this.Server.Start(maxPlayers, maxQueueSize);

            //If the server started without error, flag the Game as enabled.
            if (this.Server.Enabled)
                this.Enabled = true;

            this.World.Save();

            return this.Enabled;
        }

        /// <summary>
        /// Stops the game but unloading the world, shutting down the server and unloading all scripts/commands.
        /// </summary>
        public void Stop()
        {
            //Save the world.

            //Stop the server
            this.Server.Stop();

            //Stop the world.

            //Purge all scripts and commands.
            CommandSystem.PurgeCommands();

            //Disable the game.
            this.Enabled = false;
        }

        public void Update()
        {

        }

        private void SetupPaths()
        {
            if (!Directory.Exists(this.SavePaths.GetPath(DataTypes.Characters)))
                Directory.CreateDirectory(this.SavePaths.GetPath(DataTypes.Characters));
            if (!Directory.Exists(this.SavePaths.GetPath(DataTypes.Environments)))
                Directory.CreateDirectory(this.SavePaths.GetPath(DataTypes.Environments));
            if (!Directory.Exists(this.SavePaths.GetPath(DataTypes.Equipment)))
                Directory.CreateDirectory(this.SavePaths.GetPath(DataTypes.Equipment));
            if (!Directory.Exists(this.SavePaths.GetPath(DataTypes.Players)))
                Directory.CreateDirectory(this.SavePaths.GetPath(DataTypes.Players));
            if (!Directory.Exists(this.SavePaths.GetPath(DataTypes.Scripts)))
                Directory.CreateDirectory(this.SavePaths.GetPath(DataTypes.Scripts));
        }
    }
}

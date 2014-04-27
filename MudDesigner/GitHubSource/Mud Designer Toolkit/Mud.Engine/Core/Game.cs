// Microsoft .NET Framework
using System;

// Mud Designer Framework
using Mud.Networking;
using Mud.DataAccess;
using Mud.Models.Environment;
using Mud.Scripting;

namespace Mud.Core
{
    /// <summary>
    /// Game is responsible for managing the state of the game as it runs on the server.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Gets or Sets the name of the game being played.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the current version of the game.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or Sets the website that users can visit to get information on the game.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or Sets if the names of the Rooms will be displayed when a "Look" styled command is used. If disabled, only the Room description will be printed by default.
        /// </summary>
        public bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets if the game will be saved automatically.
        /// </summary>
        public bool AutoSave { get; set; }

        /// <summary>
        /// Gets or Sets how often the game will be saved if AutoSave is true.
        /// </summary>
        public int SaveFrequency { get; set; }

        /// <summary>
        /// Gets the date associated with the last time the game was saved.
        /// </summary>
        public DateTime LastSave { get; set; }

        /// <summary>
        /// Gets or Sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        public IWorld World { get; set; }

        /// <summary>
        /// Gets or Sets the current Server for the game.
        /// </summary>
        public IServer Server { get; set; }

        /// <summary>
        /// Gets if the game is being hosted as a server or if it is a offline/singleplayer game.
        /// </summary>
        public bool IsMultiplayer { get; protected set; }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// Gets the Data Context used to persist the games data.
        /// </summary>
        public IDataContext DataContext { get; protected set; }

        /// <summary>
        /// Initializes the game object. 
        /// This includes setting up the world, any server or data setup and preparing the game for runtime.
        /// </summary>
        /// <param name="context">An object implementing IDataContext that will be used to persist the games data.</param>
        /// <param name="server">An object implementing IServer that will be used to host the game over a networked connection.</param>
        /// <returns>Returns true if the game setup was completed without issue</returns>
        public bool Initialize(IDataContext context, IServer server = null)
        {
            // If we have no way to store the data, then we do not consider outself initialized.
            if (context == null)
            {
                return false;
            }
            else
            {
                this.DataContext = context;
            }

            // We need to add our own assembly to the ScriptFactory for use.
            ScriptFactory.AddAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            // If we have a server, then we are going to run in networked mode. Otherwise it's offline/singleplayer only.
            if (server != null)
            {
                this.Server = server;
                this.IsMultiplayer = true;
                bool started = false;

                if (this.Server.Status == ServerStatus.Stopped)
                {
                    this.Server.Start(this);
                }

                if (!started)
                {
                    Console.WriteLine("Failed to start the server!");
                }
            }
            else
            { 
                this.IsMultiplayer = false;
            }

            this.World = new World();

            this.IsRunning = (this.DataContext == null || this.World == null) ? false : true;
            return this.IsRunning;
        }
    }
}

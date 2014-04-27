// Microsoft .NET Framework
using System;

// Mud Designer Framework
using Mud.DataAccess;
using Mud.Models.Environment;
using Mud.Networking;

namespace Mud.Core
{
    /// <summary>
    /// IGame based objects are responsible for managing the state of the game as it runs on the server.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Gets or Sets the name of the game being played.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or Sets the current version of the game.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Gets or Sets the website that users can visit to get information on the game.
        /// </summary>
        string Website { get; set; }

        /// <summary>
        /// Gets or Sets if the names of the Rooms will be displayed when a "Look" styled command is used. If disabled, only the Room description will be printed by default.
        /// </summary>
        bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets if the game will be saved automatically.
        /// </summary>
        bool AutoSave { get; set; }

        /// <summary>
        /// Gets or Sets how often the game will be saved if AutoSave is true.
        /// </summary>
        int SaveFrequency { get; set; }

        /// <summary>
        /// Gets the date associated with the last time the game was saved.
        /// </summary>
        DateTime LastSave { get; set; }

        /// <summary>
        /// Gets or Sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        IWorld World { get; set; }

        /// <summary>
        /// Gets or Sets the current Server for the game.
        /// </summary>
        IServer Server { get; set; }

        /// <summary>
        /// Gets if the game is being hosted as a server or if it is a offline/singleplayer game.
        /// </summary>
        bool IsMultiplayer { get; }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets the Data Context used to persist the games data.
        /// </summary>
        IDataContext DataContext { get; }

        /// <summary>
        /// Initializes the game object. 
        /// This includes setting up the world, any server or data setup and preparing the game for runtime.
        /// </summary>
        /// <param name="context">An object implementing IDataContext that will be used to persist the games data.</param>
        /// <param name="server">An object implementing IServer that will be used to host the game over a networked connection.</param>
        /// <returns>Returns true if the game setup was completed without issue</returns>
        bool Initialize(IDataContext context, IServer server = null);
    }
}

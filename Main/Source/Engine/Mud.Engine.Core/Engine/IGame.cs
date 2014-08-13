using Mud.Engine.Core.Environment;
using Mud.Engine.Core.Mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    public interface IGame
    {
        /// <summary>
        /// Gets a value indicating whether this instance is multiplayer.
        /// </summary>
        bool IsMultiplayer { get; }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets or Sets the name of the game being played.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or Sets the current version of the game.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Gets or Sets the website that users can visit to get information on the game.
        /// </summary>
        string Website { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide room names].
        /// </summary>
        bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable automatic save].
        /// </summary>
        bool EnableAutoSave { get; set; }

        /// <summary>
        /// Gets or sets the automatic save frequency.
        /// </summary>
        int AutoSaveFrequency { get; set; }

        /// <summary>
        /// Gets or sets the last saved.
        /// </summary>
        DateTime LastSaved { get; set; }

        /// <summary>
        /// Gets or Sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        ICollection<IWorld> Worlds { get; set; }

        ILogger Logger { get; }

        /// <summary>
        /// The initialize method is responsible for restoring the world and state.
        /// </summary>
        Task Initialize();
    }
}

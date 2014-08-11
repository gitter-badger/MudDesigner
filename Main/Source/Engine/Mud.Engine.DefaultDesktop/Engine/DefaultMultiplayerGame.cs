using Mud.Engine.Core.Engine;
using Mud.Engine.Core.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.DefaultDesktop.Engine
{
    /// <summary>
    /// The default implementation of the IGame interface for the Mud Engine.
    /// </summary>
    public class DefaultMultiplayerGame : IGame
    {
        /// <summary>
        /// Gets a value indicating whether this instance is multiplayer.
        /// </summary>
        public bool IsMultiplayer
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets or Sets the name of the game being played.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the current version of the game.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or Sets the website that users can visit to get information on the game.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide room names].
        /// </summary>
        public bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable automatic save].
        /// </summary>
        public bool EnableAutoSave { get; set; }

        /// <summary>
        /// Gets or sets the automatic save frequency.
        /// </summary>
        public int AutoSaveFrequency { get; set; }

        /// <summary>
        /// Gets or sets the last saved.
        /// </summary>
        public DateTime LastSaved { get; set; }

        /// <summary>
        /// Gets or Sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        public List<IWorld> Worlds { get; set; }

        public void Initialize()
        {
        }
    }
}

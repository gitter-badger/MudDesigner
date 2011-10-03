using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MudEngine.Core
{
    public abstract class BaseGame : BaseObject
    {
        private bool _EnableServer;

        /// <summary>
        /// Enables or Disables the Auto Save feature.
        /// </summary>
        [Category("Game Settings")]
        [Description("Enables or Disables the Auto Save feature.")]
        public bool EnableAutoSave { get; set; }

        /// <summary>
        /// Gets if the game is currently running or not.
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// Gets or Sets the auto-save interval for the game during runtime
        /// </summary>
        public int AutoSaveInterval { get; set; }

        /// <summary>
        /// Gets or Sets the minimum size a users account password must be
        /// </summary>
        public int PasswordMinimumSize { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of players allowed to connect to the server at once.
        /// </summary>
        public int MaximumPlayers { get; set; }

        /// <summary>
        /// Gets a reference to the collection of players currently connected to the server
        /// </summary>
        public Dictionary<TcpClient, ICharacter> ConnectedPlayers { get; private set; }

        public BaseServer Server { get; protected set; }

        public bool EnableServer { get; set; }

        /// <summary>
        /// Gets or Sets the current version of the game.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or Sets the environment that will be used when a new user account is created.
        /// </summary>
        public IEnvironment InitialEnvironment { get; set; }

        public BaseGame()
        {
            this.ConnectedPlayers = new Dictionary<TcpClient, ICharacter>();
            this.ID = 0;
        }

        public abstract void Initialize();

        public abstract void Update();

        public abstract void Shutdown();

        public abstract void OnConnect(TcpClient client);

        public abstract void OnDisconnect(TcpClient client);

        public abstract int GetAvailableID();
    }
}

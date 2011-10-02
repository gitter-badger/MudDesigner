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
        /// <summary>
        /// Enables or Disables the server.
        /// </summary>
        [Category("Game Settings")]
        [Description("Enables or Disabels the server.")]
        public bool EnableServer { get; set; }

        /// <summary>
        /// Enables or Disables the Auto Save feature.
        /// </summary>
        [Category("Game Settings")]
        [Description("Enables or Disables the Auto Save feature.")]
        public bool EnableAutoSave { get; set; }

        public int AutoSaveInterval { get; set; }

        public int PasswordMinimumSize { get; set; }

        public int MaximumPlayers { get; set; }

        public Dictionary<TcpClient, ICharacter> ConnectedPlayers { get; private set; }

        public string Version { get; set; }

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

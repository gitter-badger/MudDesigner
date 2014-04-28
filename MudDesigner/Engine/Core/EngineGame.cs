//-----------------------------------------------------------------------
// <copyright file="CoreGame.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml.Serialization;
using MudEngine.Engine.GameObjects.Environment;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// The Core Game that can be used across any game built on the Mud Engine.
    /// </summary>
    [Serializable]
    public class EngineGame : IGame
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiplayer.
        /// </summary>
        [XmlIgnore]
        public bool IsMultiplayer { get; protected set; }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        [XmlIgnore]
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// Gets the storage source.
        /// </summary>
        [XmlIgnore]
        public IPersistedStorage StorageSource { get; protected set; }

        /// <summary>
        /// Gets or Sets the name of the game being played.
        /// </summary>
        [StorageFilenameAttribute]
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
        [XmlIgnore]
        public IWorld World { get; set; }

        /// <summary>
        /// Gets or Sets the current Server for the game.
        /// </summary>
        [XmlIgnore]
        public IServer Server { get; set; }

        /// <summary>
        /// Initializes the specified storage source and server.
        /// </summary>
        /// <param name="storageSource">The storage source.</param>
        /// <param name="server">The server.</param>
        public virtual void Initialize(IPersistedStorage storageSource, IServer server = null)
        {
            if (storageSource == null)
            {
                throw new NullReferenceException("The storageSource parameter can not be null.");
            }

            this.StorageSource = storageSource;
            this.StorageSource.InitializeStorage();

            // If we have a server, we need to start it.
            if (server != null)
            {
                StartServer(server);
            }

            // this.World = new CoreWorld(this);

            // If a server exists and is running, we are good to go. If no server, then we default to Running = true;
            this.IsRunning = (this.Server != null) ? (this.Server.Status == ServerStatus.Running) : true;
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <param name="server">The server object.</param>
        private void StartServer(IServer server)
        {
            this.Server = server;
            this.IsMultiplayer = true;

            // Make sure the server status is stopped.
            if (this.Server.Status == ServerStatus.Running)
            {
                this.Server.Stop();
            }

            // Start the server.
            try
            {
                this.Server.Start(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

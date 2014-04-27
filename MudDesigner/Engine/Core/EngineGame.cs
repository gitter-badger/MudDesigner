//-----------------------------------------------------------------------
// <copyright file="CoreGame.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// The Core Game that can be used across any game built on the Mud Engine.
    /// </summary>
    public class EngineGame : IGame
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
        /// Gets or sets a value indicating whether this instance is multiplayer.
        /// </summary>
        public bool IsMultiplayer { get; protected set; }

        /// <summary>
        /// Gets or Sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        public IWorld World { get; set; }

        /// <summary>
        /// Gets or Sets the current Server for the game.
        /// </summary>
        public IServer Server { get; set; }

        /// <summary>
        /// Gets if the game (online or offline) is currently running.
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// Gets the storage source.
        /// </summary>
        public IPersistedStorage StorageSource { get; protected set; }

        /// <summary>
        /// Initializes the specified storage source and server.
        /// </summary>
        /// <param name="storageSource">The storage source.</param>
        /// <param name="server">The server.</param>
        public void Initialize(IPersistedStorage storageSource, IServer server = null)
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

            // this.World = new CoreWorld(this);

            // We made it to the end, we're good to start.
            this.IsRunning = true;
        }
    }
}

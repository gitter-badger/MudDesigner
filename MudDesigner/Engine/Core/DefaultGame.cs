//-----------------------------------------------------------------------
// <copyright file="CoreGame.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MudEngine.Engine.GameObjects.Environment;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// The Core Game that can be used across any game built on the Mud Engine.
    /// </summary>
    [Serializable]
    public class DefaultGame : IGame
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
        public List<IWorld> Worlds { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public IProgress<IMessage> Logger { get; set; }

        /// <summary>
        /// Initializes the specified storage source and server.
        /// </summary>
        /// <param name="storageSource">The storage source.</param>
        /// <exception cref="System.NullReferenceException">The storageSource parameter can not be null.</exception>
        public virtual void Initialize<T>(IPersistedStorage storageSource) where T : class, IPlayer, new()
        {
            if (storageSource == null)
            {
                this.LogMessage("initializing the persisted storage system failed! No data will be restored.");
            }
            else
            {
                this.StorageSource = storageSource;
                // this.StorageSource.InitializeStorage();
            }

            this.SetupWorlds();

            // If a server exists and is running, we are good to go. If no server, then we default to Running = true;
            this.IsRunning = this.Worlds != null && this.StorageSource != null;

            if (!this.IsRunning)
            {
                this.LogMessage("Failed to start the game.");
                return;
            }

            this.LogMessage("Setting up the player.");
            this.Player = new T();
            this.Player.Initialize(this);
            this.Player.SendMessage += (target, message) => this.BroadcastToPlayer(target as IMob, message);

            Task.Run(() =>
            {
                while(this.IsRunning)
                {
                    string input = Console.ReadLine();
                    this.Player.ReceiveInput(new InputMessage(input));
                }
            });
        }

        /// <summary>
        /// Setups the worlds up by restoring them from the storage source.
        /// </summary>
        protected virtual void SetupWorlds()
        {
            this.LogMessage("Setting up the game world.");
            this.Worlds = new List<IWorld>();
            this.LogMessage("Game world set up.");
        }

        /// <summary>
        /// Broadcasts the specified message to the user.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="message">The message.</param>
        public virtual void BroadcastToPlayer(IMob sender, IMessage message)
        {
            Console.WriteLine(string.Format("Message => {0}", message.Message));
        }

        /// <summary>
        /// Formats the message for broadcasting.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns a string formatted.</returns>
        public virtual string FormatMessageForBroadcasting(IMessage message)
        {
            string[] messageContent = message.Message.Split('\n');
            string formattedMessage = string.Empty;

            foreach (string line in messageContent)
            {
                formattedMessage += line + Environment.NewLine;
            }

            return formattedMessage;
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void LogMessage(string message)
        {
            if (this.Logger != null)
            {
                this.Logger.Report(new InputMessage(message));
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="IGame.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Environment;

    /// <summary>
    /// The IGame interface provides all of properties required for a game to run.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiplayer.
        /// </summary>
        bool IsMultiplayer { get; set; }

        /// <summary>
        /// Gets or sets the name of the game being played.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the current version of the game.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Gets or sets the website that users can visit to get information on the game.
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
        /// Gets or sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        ICollection<IWorld> Worlds { get; set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        ILogger Logger { get; }

        /// <summary>
        /// The initialize method is responsible for restoring the world and state.
        /// </summary>
        /// <returns>Returns a Task representing the async operation.</returns>
        Task Initialize();
    }
}

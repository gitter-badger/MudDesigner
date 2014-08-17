//-----------------------------------------------------------------------
// <copyright file="DefaultGame.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mud.Engine.Core.Engine.Validation;
    using Mud.Engine.Core.Environment;

    /// <summary>
    /// The Default engine implementation of the IGame interface. This implementation provides validation support via ValidationBase.
    /// </summary>
    public abstract class DefaultGame : ValidatableBase, IGame
    {        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiplayer.
        /// </summary>
        public bool IsMultiplayer { get; set; }

        /// <summary>
        /// Gets or sets the name of the game being played.
        /// </summary>
        [ValidateValueIsNotNullOrEmpty(FailureMessage = "Name can not be left empty.", ValidationMessageType = typeof(ErrorMessage))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the current version of the game.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets the website that users can visit to get information on the game.
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
        /// Gets or sets the automatic save frequency in seconds.
        /// </summary>
        [ValidateNumberIsGreaterThan(GreaterThanValue = "59", FailureMessage = "Having an Auto-Save frequency of less than 60 seconds can have a serious performance impact on servers with large worlds or a large number of players.", ValidationMessageType = typeof(WarningMessage))]
        public int AutoSaveFrequency { get; set; }

        /// <summary>
        /// Gets or sets the last saved.
        /// </summary>
        public DateTime LastSaved { get; set; }

        /// <summary>
        /// Gets or sets the current World for the game. Contains all of the Realms, Zones and Rooms.
        /// </summary>
        public ICollection<IWorld> Worlds { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; protected set; }

        /// <summary>
        /// The initialize method is responsible for restoring the world and state.
        /// </summary>
        /// <returns>Returns the Task associated with the await call.</returns>
        public abstract Task Initialize();
    }
}

//-----------------------------------------------------------------------
// <copyright file="LoadFileCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;
using log4net;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Provides support for loading the game world.  This is an admin only command.
    /// </summary>
    public class LoadFileCommand : ICommand
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoadFileCommand));

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player who sent the command.</param>
        public void Execute(IPlayer player)
        {
            // if the player has the correct role..
            if (player != null && (player.Role == CharacterRoles.Owner || player.Role == CharacterRoles.Admin))
            {
                if (player.Director.Server.Game != null)
                {
                    // Restore the world to the state it was in prior to the last save.
                    player.Director.Server.Game.RestoreWorld();
                    player.SendMessage("World restoration completed.");
                    Log.Info("World restoration completed.");
                }
            }
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="SaveWorldFileCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.IO;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;
using log4net;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Saves the current state of the Game World.  This is an admin only command.
    /// </summary>
    public class SaveWorldFileCommand : ICommand
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SaveWorldFileCommand));

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player who sent the command.</param>
        public void Execute(IPlayer player)
        {
            // if the player exists and has the proper role...
            if (player != null && (player.Role == CharacterRoles.Admin || player.Role == CharacterRoles.Owner))
            {
                if (player.Director.Server.Game != null)
                {
                    // Save the world.
                    player.Director.Server.Game.SaveWorld();
                    player.SendMessage("Save completed.");
                    Log.Info("Save completed.");

                }
            }
        }
    }
}
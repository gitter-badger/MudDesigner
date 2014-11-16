//-----------------------------------------------------------------------
// <copyright file="InvalidCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Informs the player that an invalid command was used.
    /// </summary>
    [HelpAttribute("Invalid Command is a result of entering a command that the game does not recognize.")]
    public class InvalidCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player who sent the command.</param>
        public void Execute(IPlayer player)
        {
            if (player == null)
                return; // Can happen when the user connection is closed in the middle of a command or state executing
            else
                player.SendMessage("Invalid command used!");
        }
         
    }
}
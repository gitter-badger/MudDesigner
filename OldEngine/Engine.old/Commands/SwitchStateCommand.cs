//-----------------------------------------------------------------------
// <copyright file="SwitchStateCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// This command switches the state of a player.
    /// </summary>
    public class SwitchStateCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player who sent the command.</param>
        public void Execute(IPlayer player)
        {
            //Switch the players state.
            /*
            if (newState != null && currentPlayer != null)
                currentPlayer.SwitchState(newState);
             */
        }
    }
}
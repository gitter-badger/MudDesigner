//-----------------------------------------------------------------------
// <copyright file="InvalidCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Sockets;
using System.Text;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Commands
{
    public class InvalidCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player who sent the command.</param>
        public void Execute(IMob mob)
        {
            if (mob == null)
                return; // Can happen when the user connection is closed in the middle of a command or state executing
            else
                mob.Send(new InformationalMessage("Invalid command used!"));
        }
         
    }
}
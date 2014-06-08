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
        /// Gets the command inpupt.
        /// </summary>
        public string CommandInput { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this command is incomplete and needs to continue running.
        /// </summary>
        public bool IsIncomplete { get; private set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="mob">The mob.</param>
        /// <param name="input">The input.</param>
        public void Execute(IMob mob, string input)
        {
            if (mob == null)
                return; // Can happen when the user connection is closed in the middle of a command or state executing
            else
                mob.Send(new InformationalMessage("Invalid command used!"));
        }
    }
}
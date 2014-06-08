//-----------------------------------------------------------------------
// <copyright file="NoOpCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net.Sockets;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Commands
{
    /// <summary>
    /// This command performs nothing. It is used when the engine needs to continue a current state but perform no commands
    /// </summary>
    public class NoOpCommand : ICommand
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
        /// Executes the specified mob.
        /// </summary>
        /// <param name="mob">The mob.</param>
        /// <param name="input">The input.</param>
        public void Execute(IMob mob, string input)
        {
            return; // No operation performed.
        }
    }
}
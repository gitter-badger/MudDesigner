//-----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net.Sockets;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.Commands
{
    /// <summary>
    /// Provides the required method for implementing a engine command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the command inpupt.
        /// </summary>
        string CommandInput { get; }

        /// <summary>
        /// Gets a value indicating whether this command is incomplete and needs to continue running.
        /// </summary>
        bool IsIncomplete { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="mob">The mob.</param>
        /// <param name="input">The input.</param>
        void Execute(IMob mob, string input);
    }
}
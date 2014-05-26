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
        /// Executes the command.
        /// </summary>
        /// <param name="mob">The mob.</param>
        void Execute(IMob mob);
    }
}
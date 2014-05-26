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
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player who sent the command.</param>
        public void Execute(IPlayer player)
        {
            // We are doing nothing on purpose.
            // This is a No operation command, aka do nothing.
            // good for silently changing states or modes.
        }

        public void Execute(IMob mob)
        {
            throw new System.NotImplementedException();
        }
    }
}
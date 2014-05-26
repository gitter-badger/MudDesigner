//-----------------------------------------------------------------------
// <copyright file="IState.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MudEngine.Engine.Commands;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.GameObjects.Mob.States
{
    /// <summary>
    /// Allows for the creation of States that control the characters in the game.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Renders the current state to the players terminal.
        /// </summary>
        /// <param name="player">The player to render to</param>
        void Render(IMob mob);

        /// <summary>
        /// Gets the Command that the player entered and preps it for execution.
        /// </summary>
        /// <returns></returns>
        ICommand GetCommand(IMessage command);
    }
}
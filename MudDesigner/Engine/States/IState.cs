//-----------------------------------------------------------------------
// <copyright file="IState.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.States
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
        void Render(IPlayer player);

        /// <summary>
        /// Gets the Command that the player entered and preps it for execution.
        /// </summary>
        /// <returns></returns>
        ICommand GetCommand();
    }
}
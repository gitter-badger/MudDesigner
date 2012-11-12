/* SwitchStateCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: This command switches the state of a player.
 */

//Microsoft .NET using statements
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
        private IState newState;
        private IPlayer currentPlayer;

        public SwitchStateCommand(IState state, IPlayer player)
        {
            newState = state;
            currentPlayer = player;
        }

        public void Execute()
        {
            //Switch the players state.
            if (newState != null && currentPlayer != null)
                currentPlayer.SwitchState(newState);
        }
    }
}
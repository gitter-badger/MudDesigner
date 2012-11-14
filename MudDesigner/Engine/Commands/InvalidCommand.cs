/* InvalidCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Informs the player that an invalid command was used.
 */

//Microsoft .NET using statements
using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Informs the player that an invalid command was used.
    /// </summary>
    [HelpAttribute("Invalid Command is a result of entering a command that the game does not recognize.")]
    public class InvalidCommand : ICommand
    {
        public void Execute(IPlayer player)
        {
            if (player == null)
                return; //Can happen when the user connection is closed in the middle of a command or state executing
            else
                player.SendMessage("Invalid command used!");
        }
         
    }
}
using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    [HelpAttribute("Invalid Command is a result of entering a command that the game does not recognize.")]
    public class InvalidCommand : ICommand
    {
        private IPlayer player;

        public InvalidCommand(IPlayer connectedPlayer)
        {
            player = connectedPlayer;
        }

        public void Execute()
        {
            if (player == null)
                return; //Can happen when the user connection is closed in the middle of a command or state executing
            else
                player.SendMessage("Invalid command used!");
        }
         
    }
}
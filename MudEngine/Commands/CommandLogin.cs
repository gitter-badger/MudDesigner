using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.Commands;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public class CommandLogin : IGameCommand
    {
        public bool Override { get; set; }
        public string Name { get; set; }

        public CommandResults Execute(string command, BaseCharacter player)
        {
            player.ActiveGame.SendMessage(player.ActiveGame.GameTitle, player);
            player.ActiveGame.SendMessage(player.ActiveGame.Version, player);
            player.ActiveGame.SendMessage(player.ActiveGame.Story, player);
            player.ActiveGame.SendMessage("", player);
            player.ActiveGame.SendMessage("Enter Character Name: ", player, false);

            //TODO: Read  user input...

            return new CommandResults();
        }
    }
}

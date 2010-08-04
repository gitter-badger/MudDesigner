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
            player.Send(player.ActiveGame.GameTitle);
            player.Send(player.ActiveGame.Version);
            player.Send(player.ActiveGame.Story);
            player.Send("");

            bool isLegal = false;

            while (!isLegal)
            {
                player.Send("Enter Character Name: ", false);
                string input = player.ReadInput();
                bool foundName = false;

                foreach (BaseCharacter bc in player.ActiveGame.PlayerCollection)
                {
                    if (bc.Name == input)
                    {
                        player.Send("Character name already taken.");
                        foundName = true;
                        break;
                    }
                }

                if (!foundName)
                {
                    if (input == "")
                        continue;
                    else
                    {
                        isLegal = true;
                        player.Name = input;
                    }
                }
            }

            player.Send("Welcome " + player.Name + "!");
            player.CommandSystem.ExecuteCommand("Look", player);
            return new CommandResults();
        }
    }
}

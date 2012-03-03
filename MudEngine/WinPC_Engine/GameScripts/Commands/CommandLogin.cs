using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core.Interface;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Game.Environment;
using MudEngine.GameScripts;

namespace MudEngine.GameScripts.Commands
{
    public class CommandLogin : ICommand
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public CommandLogin()
        {
            Help = new List<string>();
            Name = "Login";
            Description = "Account login command.";
        }

        public void Execute(string command, Game.Characters.StandardCharacter character)
        {
            character.SendMessage("Please enter character name: ");
            String name = String.Empty;

            while (String.IsNullOrEmpty(name))
            {
                name = character.GetInput();
            }
            
        }
    }
}

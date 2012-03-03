using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
            StandardGame game = character.Game;

            character.SendMessage("Please enter character name: ");
            String name = String.Empty;

            while (String.IsNullOrEmpty(name))
            {
                character.SendMessage("Enter your character name: ", false);

                name = String.Empty;
                Boolean isFound = false;

                while (String.IsNullOrEmpty(name))
                {
                    name = character.GetInput();

                    if (String.IsNullOrEmpty(name))
                        continue;

                    //Look if the file exists.
                    if (File.Exists(game.SavePaths.Players + @"\" + name))
                        isFound = true;
                    else
                    {
                        character.SendMessage("Enter your password: ", false);

                        String password = character.GetInput();

                        if (String.IsNullOrEmpty(password))
                        {
                            //Reset the process if no password supplied.
                            name = String.Empty;
                            continue;
                        }
                        else
                        {

                        }
                    }
                }
            }
            
        }
    }
}

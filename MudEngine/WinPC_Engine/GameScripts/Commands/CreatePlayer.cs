using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

using MudEngine.Core;
using MudEngine.Core.Interfaces;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Game.Environment;
using MudEngine.GameScripts;

namespace MudEngine.GameScripts.Commands
{
    public class CreatePlayer : ICommand
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public CreatePlayer()
        {
            Help = new List<string>();
            Name = "CreatePlayer";
            Description = "Account login command.";
        }

        public Boolean Execute(string command, Game.Characters.StandardCharacter character)
        {
            //reference to the Characters Game.
            StandardGame game = character.Game;
            Boolean buildingPassword = true;

            //We need to check if the 3rd Frame on the stack is the CommandLogin Type.
            //If it isn't, then another Type executed this command and we don't allow it.
            StackTrace trace = new StackTrace();
            String callingType = trace.GetFrame(3).GetMethod().ReflectedType.Name;

            //Don't allow anything other than the Login command to start the
            //character creation process.
            if (callingType != "Login")
            {
                character.SendMessage("Invalid Command Used.");
                return false;
            }

            //Make sure we build a proper password.
            while (buildingPassword)
            {
                character.SendMessage("Please enter a password for this character: ", false);
                String password1, password2;
                password1 = character.GetInput();

                //We do not perform any IsLetterOrDigit() checks on passwords.  The more
                //special characters the better.
                //We do however want to make sure the length of the password is sufficient
                if (password1.Length < character.Game.MinimumPasswordSize)
                {
                    character.SendMessage("Passwords must have a minimum of " + character.Game.MinimumPasswordSize.ToString() + " characters!");
                    continue;
                }

                character.SendMessage("Please re-enter your password for confirmation: ", false);
                password2 = character.GetInput();

                if (password1 == password2)
                {
                    buildingPassword = false;
                    character.Password = password1;
                }
                else
                {
                    character.SendMessage("Passwords did not match!");
                    continue;
                }
            }

            try
            {

                character.Move(game.World.StartLocation);

                //TODO: Create a class and setup Stats.
                character.Save(character.Filename, false);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}

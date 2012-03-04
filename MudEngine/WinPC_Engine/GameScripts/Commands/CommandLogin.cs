using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public Boolean Execute(string command, Game.Characters.StandardCharacter character)
        {
            //reference to the Characters Game.
            StandardGame game = character.Game;

            //Store a reference to this character for other methods within this class.
            this._Character = character;

            if (character.LoggedIn)
            {
                character.SendMessage("You are already logged in!");
                return false;
            }

            character.SendMessage("Please enter character name: ");
            String name = String.Empty;

            //Repeat the login process until we get a valid name.
            while (String.IsNullOrEmpty(name))
            {
                character.SendMessage("Enter your character name: ", false);

                name = String.Empty;
                Boolean isFound = false;

                //Get the supplied name
                name = character.GetInput();

                //Entering their username is the first time input has been
                //made by the user.  Expect their Telnet client to send Header
                //information, so we strip it out by forcing only a whole
                //word to be saved and everything else discarded.
                //Note that this wouldn't work if first and last names were supported
                //as this only returns the first word found and that is all.
                Match m = Regex.Match(name, @"\w+");
                name = m.Value;

                //Make sure no illegal characters are in the name such as underbars or asteriks
                if (!name.All(Char.IsLetterOrDigit))
                {
                    character.SendMessage("Invalid character name supplied.  Only letters and numbers are allowed.");
                    name = String.Empty;
                    continue;
                }

                //Uncomment this if first/last name combinations are used in
                //the game.  Note that this does support numbers and other
                //special characters.  If you do not want them, you will need
                //to modify the Regular Expression Evaluator below.
                /*
                MatchCollection m = Regex.Matches(name, @"\w+");
                name = String.Empty;
                foreach (Match word in m)
                {
                    name += word.Value + " ";
                }
                */

                //Check if the name entered is blank. Ensure that we remove leading and ending spaces
                if (String.IsNullOrEmpty(name))
                    continue;

                //Look if the file exists.
                String filename = game.SavePaths.GetPath(DAL.DataTypes.Players) + name;

                if (File.Exists(game.SavePaths.GetPath(DAL.DataTypes.Players) + name))
                    isFound = true;

                //if the character name supplied exists, load it.
                if (isFound)
                {
                    //Perform a password check
                    character.SendMessage("Please enter a password for " + name);
                    String password = character.GetInput();

                    //If the password is empty, then restart the process.
                    if (String.IsNullOrEmpty(password))
                    {
                        name = String.Empty;
                        continue;
                    }

                    //Load the character from file.
                    character.Load(game.SavePaths.GetPath(DAL.DataTypes.Players) + name);

                    //Check if the characters password matches that of the saved player password
                    if ("1234" != password)
                    {
                        //No match, bail.
                        character.SendMessage("Invalid password provided.");
                        name = String.Empty;
                        continue;
                    }
                    else //End our loading.
                    {
                        character.SendMessage("Welcome back " + character.Name + "!");
                        return true;
                    }

                }
                else
                {
                    character.SendMessage("No character with that name was found.  Create a new one? (Yes/No)");
                    String result = character.GetInput();
                    if (result.ToLower() == "yes")
                    {
                        return CreateCharacter(name);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        private Boolean CreateCharacter(String name)
        {
            return false;
        }

        private StandardCharacter _Character;
    }
}

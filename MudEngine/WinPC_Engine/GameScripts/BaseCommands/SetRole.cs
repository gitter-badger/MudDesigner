using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using MudEngine.Core.Interfaces;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Networking;

namespace MudEngine.GameScripts.Commands
{
    public class SetRole : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public SetRole()
        {
            this.Name = "SetRole";
            this.Description = "Chat command that allows objects to communicate.";

            this.Help = new List<string>();
            this.Help.Add("Usage: SetRole TargetCharacterNameHere");
        }

        public Boolean Execute(string command, StandardCharacter character)
        {
            //Grab a reference to the character for simplifying access.
            StandardGame game = character.Game;
            MatchCollection matches = Regex.Matches(command.Substring("setrole".Length).Trim(), @"\w+");
            List<String> names = new List<string>();

            foreach (Match match in matches)
            {
                names.Add(match.Value.ToLower());
            }

            if (names.Count < 1 && character.Role == CharacterRoles.Admin)
            {
                character.SendMessage("You must provide a target character name.");
                ShowHelp(character);
                return false;
            }

            if (character.Role == CharacterRoles.Admin || character.Name == game.Server.ServerOwner)
            {
                StandardCharacter target = game.Server.ConnectionManager.GetConnectedCharacter(names[0].ToLower());

                this.ApplyRole(character, target);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void ApplyRole(StandardCharacter admin, StandardCharacter target)
        {
            admin.SendMessage("Please choose from one of the available Roles:");

            //Blow all of the available values up into an array.
            Array values = Enum.GetValues(typeof(CharacterRoles));
            List<String> roles = new List<string>();

            //Loop through each available value, converting it into a string.
            foreach (Int32 value in values)
            {
                //Get the string representation of the current value
                String displayName = Enum.GetName(typeof(CharacterRoles), value);
                roles.Add(displayName);

                admin.SendMessage(displayName);
            }
            admin.SendMessage("Selection: ", false);
            String result = admin.GetInput();

            if (String.IsNullOrEmpty(result))
            {
                admin.SendMessage("You did not select a valid Role.");
                return;
            }

            if (roles.Contains(result))
            {
                target.SetRole(admin, target, (CharacterRoles)Enum.Parse(typeof(CharacterRoles), result));
            }
        }

        public void ShowHelp(StandardCharacter character)
        {
            String help = String.Empty;

            foreach (String entry in this.Help)
            {
                help += entry + "\n";
            }

            character.SendMessage(help);
        }
    }
}

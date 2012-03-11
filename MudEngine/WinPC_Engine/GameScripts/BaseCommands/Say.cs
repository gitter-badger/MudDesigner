using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core.Interfaces;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Networking;

namespace MudEngine.GameScripts.Commands
{
    public class Say : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public Say()
        {
            this.Name = "Say";
            this.Description = "Chat command that allows objects to communicate.";
        }

        public Boolean Execute(string command, StandardCharacter character)
        {
            //Grab a reference to the character for simplifying access.
            StandardGame game = character.Game;
            //Remove the command "Say " from the string so we only have it's message
            String message = command.Substring(3).Trim();

            //Loop through each character on the server and broadcast the message.
            //TODO: This should only broadcast to characters that are in the same Environment.
            foreach (StandardCharacter c in character.Game.Server.ConnectionManager.GetConnectedCharacters())
            {
                //Only broadcast this message to those that are not the broadcastor.
                if (c != character)
                    c.SendMessage(character.ToString() + " says: " + message);
            }

            //Send a different copy of the message to the broadcastor.
            character.SendMessage("You say: " + message);

            return true;
        }
    }
}

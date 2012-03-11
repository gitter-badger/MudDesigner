using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using MudEngine.Core.Interfaces;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Networking;

namespace MudEngine.GameScripts.Commands
{
    public class Stop : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public Stop()
        {
            this.Name = "Stop";
            this.Description = "Chat command that allows objects to communicate.";
        }

        public Boolean Execute(string command, StandardCharacter character)
        {
            //Grab a reference to the character for simplifying access.
            StandardGame game = character.Game;

            //Stop the game.
            if (character.Role == CharacterRoles.Admin)
            {
                new Thread(game.Stop).Start();
                return true;
            }
            else
            {
                //Since a non-admin character attempted this command,
                //tell them they used a invalid command
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using MudEngine.Core.Interface;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Networking;

namespace MudEngine.GameScripts.Commands
{
    public class CommandStop : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public CommandStop()
        {
            this.Name = "Stop";
            this.Description = "Chat command that allows objects to communicate.";
        }

        public void Execute(string command, StandardCharacter character)
        {
            //Grab a reference to the character for simplifying access.
            StandardGame game = character.Game;

            //Stop the game.
            new Thread(game.Stop).Start();
        }
    }
}

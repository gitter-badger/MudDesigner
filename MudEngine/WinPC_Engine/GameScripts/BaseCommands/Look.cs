using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.Game.Environment;
using MudEngine.Core.Interfaces;

namespace MudEngine.GameScripts.Commands
{
    public class Look : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public Look()
        {
            this.Name = "Look";
        }

        public bool Execute(string command, StandardCharacter character)
        {
            character.SendMessage(character.CurrentRoom.Name);
            character.SendMessage(character.CurrentRoom.Description);
            return true;
        }
    }
}

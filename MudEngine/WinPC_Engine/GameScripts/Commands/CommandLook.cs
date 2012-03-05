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
    public class CommandLook : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Help { get; set; }

        public CommandLook()
        {
            this.Name = "Look";
        }

        public bool Execute(string command, StandardCharacter character)
        {
            return false;
        }
    }
}

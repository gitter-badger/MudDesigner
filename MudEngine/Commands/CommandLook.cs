using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;
using MudEngine.FileSystem;
using MudEngine.Commands;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects.Items;

namespace MudEngine.Commands
{
    public class CommandLook : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandResults Execute(string command, BaseCharacter player)
        {
            StringBuilder desc = new StringBuilder();

            if (player.CurrentRoom == null)
            {
                return new CommandResults("Not within a created Room.");
            }

            desc.AppendLine(player.CurrentRoom.Description);

            return new CommandResults(desc.ToString());
        }
    }
}

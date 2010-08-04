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
            if (player.CurrentRoom == null)
            {
                return new CommandResults("Not within a created Room.");
            }

            if (player.CurrentRoom.DetailedDescription.Count == 0)
                player.Send(player.CurrentRoom.Description);
            else
            {
                foreach(string entry in player.CurrentRoom.DetailedDescription)
                {
                    player.Send(entry);
                }
            }

            return new CommandResults();
        }
    }
}

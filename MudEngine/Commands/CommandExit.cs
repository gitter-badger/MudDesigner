using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.Commands;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public class CommandExit : IGameCommand
    {
        public bool Override { get; set; }
        public string Name { get; set; }

        public CommandResults Execute(string command, BaseCharacter player, Game game, Room room)
        {
            game.IsRunning = false;

            return new CommandResults();
        }
    }
}

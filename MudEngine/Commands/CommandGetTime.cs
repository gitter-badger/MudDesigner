using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;

namespace MudEngine.Commands
{
    public class CommandGetTime : MudEngine.GameManagement.IGameCommand
    {
        public string Name { get; set; }

        public bool Override { get; set; }

        public MudEngine.GameManagement.CommandResults Execute(string command, BaseCharacter player)
        {
            player.Send(player.ActiveGame.WorldTime.GetCurrentWorldTime());

            return new GameManagement.CommandResults();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;

namespace MudEngine.Commands
{
    public class CommandGetTime : MudEngine.GameManagement.IGameCommand
    {
        public String Name { get; set; }

        public Boolean Override { get; set; }

        public void Execute(String command, BaseCharacter player)
        {
            player.Send(player.ActiveGame.WorldTime.GetCurrentWorldTime());
        }
    }
}

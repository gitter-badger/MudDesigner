using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudEngine.FileSystem;
using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;

namespace MudEngine.Commands
{
    public class CommandSave : IGameCommand
    {
        public Boolean Override { get; set; }
        public String Name { get; set; }

        public void Execute(String command, BaseCharacter player)
        {
                player.Save(player.ActiveGame.DataPaths.Players);
        }
    }
}

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
        public bool Override { get; set; }
        public string Name { get; set; }

        public CommandResults Execute(string command, BaseCharacter player)
        {
            string path = player.ActiveGame.DataPaths.Players;
            string filename = Path.Combine(path, player.Filename);

            player.Save(filename);

            return new CommandResults();
        }
    }
}

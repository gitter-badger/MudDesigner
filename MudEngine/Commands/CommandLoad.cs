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
    public class CommandLoad : IGameCommand
    {
        public Boolean Override { get; set; }
        public String Name { get; set; }
        public List<String> Help { get; set; }
        public void Execute(String command, BaseCharacter player)
        {
            String path = player.ActiveGame.DataPaths.Players;
            String filename = Path.Combine(path, player.Filename);

            player.Load(filename);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using MudEngine.FileSystem;
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

        public CommandResults Execute(string command, BaseCharacter player)
        {
            if (player.ActiveGame.IsMultiplayer)
                player.Disconnect();
            else
            {
                //Save the player prior to attempting to shutdown.
                //Player saving is handled in the server disconnect code but not in game shutdown.
                player.Save(Path.Combine(player.ActiveGame.DataPaths.Players, player.Filename));
                player.ActiveGame.Shutdown();
            }
            return new CommandResults();
        }
    }
}

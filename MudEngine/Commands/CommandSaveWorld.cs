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
    public class CommandSaveWorld : IGameCommand
    {
        public Boolean Override { get; set; }
        public String Name { get; set; }

        public CommandResults Execute(String command, BaseCharacter player)
        {
            if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
            {
                player.ActiveGame.Save();
            }

            return new CommandResults();
        }
    }
}

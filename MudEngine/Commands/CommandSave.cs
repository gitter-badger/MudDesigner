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

        public CommandResults Execute(String command, BaseCharacter player)
        {
            /*
            if (player.ActiveGame.PlayerCollection.Length != 0)
            {
                if (player.GetType().Name != "BaseCharacter")
                {
                    Scripting.GameObject obj = player.ActiveGame.scriptEngine.GetObject(player.ActiveGame.PlayerCollection.GetType().Name);

                    obj.InvokeMethod("Save", new object[] { player.ActiveGame.DataPaths.Players });
                }
            }
            else */
                player.Save(player.ActiveGame.DataPaths.Players);

            return new CommandResults();
        }
    }
}

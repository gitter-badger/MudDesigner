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

            //Temporary hack
            if (player.ActiveGame.PlayerCollection.Length != 0)
            {
                if (player.ActiveGame.PlayerCollection[0].GetType().Name != "BaseCharacter")
                {
                    Scripting.GameObject obj = player.ActiveGame.scriptEngine.GetObject(player.ActiveGame.PlayerCollection.GetType().Name);

                    obj.InvokeMethod("Save", new object[] { player.Name });
                }
            }
            else
                player.Save(filename); //Should never  be called?

            return new CommandResults();
        }
    }
}

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
            {
                //Let other players know that the user walked in.
                for (int i = 0; i != player.ActiveGame.PlayerCollection.Length; i++)
                {
                    if (player.ActiveGame.PlayerCollection[i].Name == player.Name)
                        continue;

                    string room = player.ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                    string realm = player.ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                    string zone = player.ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                    if ((room == player.CurrentRoom.Name) && (realm == player.CurrentRoom.Realm) && (zone == player.CurrentRoom.Zone))
                    {
                        player.ActiveGame.PlayerCollection[i].Send(player.Name + " has left.");
                    }
                }

                player.Disconnect();
            }
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

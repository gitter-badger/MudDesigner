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
        public Boolean Override { get; set; }
        public String Name { get; set; }
        public List<String> Help { get; set; }

        public CommandExit()
        {
            Help = new List<string>();
            Help.Add("Exits the game cleanly.");
        }

        public void Execute(String command, BaseCharacter player)
        {
            if (player.ActiveGame.IsMultiplayer)
            {
                //Let other players know that the user walked in.
                for (Int32 i = 0; i != player.ActiveGame.PlayerCollection.Length; i++)
                {
                    if (player.ActiveGame.PlayerCollection[i].Name == player.Name)
                        continue;

                    String room = player.ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                    String realm = player.ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                    String zone = player.ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

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
                player.Save(player.ActiveGame.DataPaths.Players);
                player.ActiveGame.Shutdown();
            }
        }
    }
}

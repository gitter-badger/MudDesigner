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
    public class CommandLogin : IGameCommand
    {
        public bool Override { get; set; }
        public string Name { get; set; }

        public CommandResults Execute(string command, BaseCharacter player)
        {
            player.Send(player.ActiveGame.GameTitle);
            player.Send(player.ActiveGame.Version);
            player.Send(player.ActiveGame.Story);
            player.Send("");

            player.Send("Enter Character Name: ", false);
            
            string input = player.ReadInput();
            Boolean playerFound = false;
            string savedFile = "";

            //See if this character already exists.
            foreach (string filename in Directory.GetFiles(player.ActiveGame.DataPaths.Players))
            {
                if (Path.GetFileNameWithoutExtension(filename).ToLower() == input.ToLower())
                {
                    //TODO: Ask for password.
                    savedFile = filename;
                    playerFound = true;
                    break;
                }
            }

            //Next search if there is an existing player already logged in with this name, if so disconnect them.
            if (player.ActiveGame.IsMultiplayer)
            {
                for (int i = 0; i <= player.ActiveGame.PlayerCollection.Length - 1; i++)
                {
                    if (player.ActiveGame.PlayerCollection[i].Name.ToLower() == input.ToLower())
                    {
                        player.ActiveGame.PlayerCollection[i].Disconnect();
                    }
                }
            }

            //Now assign this name to this player if this is a new toon or load the player if the file exists.
            if (!playerFound)
            {
                player.Name = input;
                player.Send("Welcome " + player.Name + "!");
            }
            else
            {
                player.Load(savedFile);
                player.Send("Welcome back " + player.Name + "!");
            }
            
            //Look to see if there are players in the Room
            //Let other players know that the user walked in.
            if (player.ActiveGame.IsMultiplayer)
            {
                for (int i = 0; i != player.ActiveGame.PlayerCollection.Length; i++)
                {
                    if (player.ActiveGame.PlayerCollection[i].Name == player.Name)
                        continue;

                    string room = player.ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                    string realm = player.ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                    string zone = player.ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                    if ((room == player.CurrentRoom.Name) && (realm == player.CurrentRoom.Realm) && (zone == player.CurrentRoom.Zone))
                    {
                        player.ActiveGame.PlayerCollection[i].Send(player.Name + " arrived.");
                    }
                }
            }

            return new CommandResults();
        }
    }
}

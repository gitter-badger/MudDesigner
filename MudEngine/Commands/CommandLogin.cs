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
        public Boolean Override { get; set; }
        public String Name { get; set; }
        public List<String> Help { get; set; }
        public void Execute(String command, BaseCharacter player)
        {
            player.Send(player.ActiveGame.GameTitle);
            player.Send(player.ActiveGame.Version);
            player.Send(player.ActiveGame.Story);
            player.Send("");

            Boolean playerFound = false;
            Boolean playerLegal = false;
            String savedFile = "";
            String playerName = "";

            while (!playerLegal)
            {
                player.Send("Enter Character Name: ", false);
                 playerName = player.ReadInput();

                if (!String.IsNullOrEmpty(playerName))
                    playerLegal = true;
            }
            
            //See if this character already exists.
            if (!Directory.Exists(player.ActiveGame.DataPaths.Players))
                Directory.CreateDirectory(player.ActiveGame.DataPaths.Players);

            Boolean passwordLegal = false;
            String playerPwrd = "";

            while (!passwordLegal)
            {
                player.Send("Enter Password: ", false);
                playerPwrd = player.ReadInput();

                if (!String.IsNullOrEmpty(playerPwrd))
                    passwordLegal = true;

                if (playerPwrd.Length < 6)
                {
                    passwordLegal = false;
                    player.Send("Invalid Password, minimum password length is 6 characters");
                }

                if (passwordLegal)
                {
                    foreach (String filename in Directory.GetFiles(player.ActiveGame.DataPaths.Players))
                    {
                        if (Path.GetFileNameWithoutExtension(filename).ToLower() == playerName.ToLower())
                        {
                            //TODO: Ask for password.
                            savedFile = filename;
                            playerFound = true;
                            break;
                        }
                    }

                    //Loop through all the players currently logged in and disconnect anyone that is currently logged 
                    //in with this account.
                    if (playerFound)
                    {
                        if (player.ActiveGame.IsMultiplayer)
                        {
                            for (Int32 i = 0; i <= player.ActiveGame.PlayerCollection.Length - 1; i++)
                            {
                                if (player.ActiveGame.PlayerCollection[i].Name.ToLower() == playerName.ToLower())
                                {
                                    player.ActiveGame.PlayerCollection[i].Disconnect();
                                }
                            }
                        }
                    }

                    //Now assign this name to this player if this is a new toon or load the player if the file exists.
                    if (!playerFound)
                    {
                        player.Create(playerName, playerPwrd);
                        player.Send("Welcome " + player.Name + "!");
                    }
                    else
                    {
                        BaseCharacter p = new BaseCharacter(player.ActiveGame);
                        p.Load(savedFile);
                        if (p.VarifyPassword(playerPwrd))
                        {
                            player.Load(savedFile);
                            player.Send("Welcome back " + player.Name + "!");
                        }
                        else
                        {
                            passwordLegal = false;
                            player.Send("Invalid password.");
                        }
                    }
                }
            }
            
            //Look to see if there are players in the Room
            //Let other players know that the user walked in.
            if (player.ActiveGame.IsMultiplayer)
            {
                for (Int32 i = 0; i != player.ActiveGame.PlayerCollection.Length; i++)
                {
                    if (player.ActiveGame.PlayerCollection[i].Name == player.Name)
                        continue;

                    String room = player.ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                    String realm = player.ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                    String zone = player.ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                    if ((room == player.CurrentRoom.Name) && (realm == player.CurrentRoom.Realm) && (zone == player.CurrentRoom.Zone))
                    {
                        player.ActiveGame.PlayerCollection[i].Send(player.Name + " arrived.");
                    }
                }
            }
        }
    }
}

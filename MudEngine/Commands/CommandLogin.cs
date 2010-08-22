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
    /// <summary>
    /// The Login command is used internally by the game engine.
    /// This command will not be available as a Script due to the engine requiring that it exists.
    /// Any changes needing to be made to this command to customize it in some manor will need to be done
    /// by modifying the source. 
    /// TODO: Update the engine to dynamically look for a Login script rather than relying on CommandLogin.
    /// </summary>
    public class CommandLogin : IGameCommand
    {
        /// <summary>
        /// Used by the Command Engine to allow for overriding any other commands that contain the same name.
        /// TODO: Does Overriding Commands still work? This is part of some old code I wrote several years back and might be broke.
        /// </summary>
        public Boolean Override { get; set; }

        /// <summary>
        /// The name of the command.
        /// If Override is set to true, this command will override any other command that contains the same name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A collection of strings that contains helpfull information for this Command.
        /// When the user enteres 'Help Exit' the game will print the content of this collection.
        /// This is treated like a virtual book, each entry in the collection is printed as a new line.
        /// </summary>
        public List<String> Help { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CommandLogin()
        {
            Help = new List<string>();
            Help.Add("Logs the player into their respective account.");
        }

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        public void Execute(String command, BaseCharacter player)
        {
            //Print the basic Active Game's information to the connecting player.
            player.Send(player.ActiveGame.GameTitle);
            player.Send(player.ActiveGame.Version);
            player.Send(player.ActiveGame.Story);
            player.Send("");

            //Setup some fields that we'll need.
            Boolean playerFound = false;
            String savedFile = "";
            String playerName = "";

            //Loop until we have a valid player name entered.
            while (String.IsNullOrEmpty(playerName))
            {
                player.Send("Enter Character Name: ", false);
                 playerName = player.ReadInput();
            }
            
            //Check to make sure the saved players directory actually exists. If not, create it.
            if (!Directory.Exists(player.ActiveGame.DataPaths.Players))
                Directory.CreateDirectory(player.ActiveGame.DataPaths.Players);

            //Setup some password fields we'll need
            Boolean passwordLegal = false;
            String playerPwrd = "";

            //Loop through the password creation/entering process until we have a legal password
            //entered by the player logging in.
            while (!passwordLegal)
            {
                //Acquire a password from the player.
                player.Send("Enter Password: ", false);
                playerPwrd = player.ReadInput();

                //Check if we have a legal password, meaning it's not empty and it's at least as long as the 
                //current Active Game's minimum PasswordSize property has been set to.
                if ((!String.IsNullOrEmpty(playerPwrd)) && (playerPwrd.Length >= player.ActiveGame.MinimumPasswordSize))
                {
                    passwordLegal = true;
                }
                else
                {
                    //Let the user know that their password was invalid and they need to make sure that it conforms to the
                    //current Active Game's minumum password size requirement.
                    passwordLegal = false;
                    player.Send("Invalid Password, minimum password length is " + player.ActiveGame.MinimumPasswordSize + " characters");
                }

                //Check if the password is legal. If so, we need to find the file associated with this player.
                //If no file is found, then we will create a new one.
                if (passwordLegal)
                {
                    //Iterate through the saved players directory.
                    foreach (String filename in Directory.GetFiles(player.ActiveGame.DataPaths.Players))
                    {
                        if (Path.GetFileNameWithoutExtension(filename).ToLower() == playerName.ToLower())
                        {
                            //We found a file that matched the supplied user name. We set savedFile to the filename
                            //found so that we can later varify the password before loading.
                            savedFile = filename;
                            playerFound = true;
                            break;
                        }
                    }

                    //Load the player from the saved file, if the password is legal. Once loaded 
                    //we need to look for any other player that might be logged in with this account. If found,
                    //disconnect them from the server. Multiple log-ins of the same account is not permitted.
                    if (playerFound)
                    {
                        //Load the player from file using a temporary character reference.
                        //If we load the real player now, he will get disconnected when we check to see
                        //if any player exists on the server already with this name.
                        BaseCharacter p = new BaseCharacter(player.ActiveGame);
                        p.Load(savedFile);

                        //Varify their password. If their password is matching the one on file, and the 
                        //current Active Game is a multiplayer game, then scan for other other currently
                        //logged in with this account.
                        if ((p.VarifyPassword(playerPwrd)) && (p.ActiveGame.IsMultiplayer))
                        {
                            for (Int32 i = 0; i <= player.ActiveGame.GetPlayerCollection().Length - 1; i++)
                            {
                                //Check if the current player in the iteration has a matching name as the player that
                                //just logged in with his credentials.
                                if (player.ActiveGame.GetPlayerCollection()[i].Name.ToLower() == p.Name.ToLower())
                                {
                                    //If we found a match then we need to disconnect the 
                                    //previously logged in player from the server.
                                    player.ActiveGame.GetPlayerCollection()[i].Disconnect();
                                }
                            }

                            //Now that we have no duplicate connections, load the real player.
                            //no need to re-varify password as we have already done this above.
                            player.Load(savedFile);
                            player.Send("Welcome back " + player.Name + "!");
                        }
                        else
                        {
                            if (!p.VarifyPassword(playerPwrd))
                            {
                                player.Send("Invalid password!");
                                passwordLegal = false;
                            }
                        }
                    }
                    else
                    {
                        player.Create(playerName, playerPwrd);
                        player.Send("Welcome " + playerName + "!");
                    }
                }

            }
            
            //Look to see if there are players in the Room, if there are then we need to let 
            //other players know that the user walked/logged in.
            if (player.ActiveGame.IsMultiplayer)
            {
                for (Int32 i = 0; i != player.ActiveGame.GetPlayerCollection().Length; i++)
                {
                    //Check if the current player in the iteration is the currently logging in player
                    //or a un-used player slot. If it is we will skip them as the logging in player
                    //doesn't need to be told that he has just logged in, and un-used players slots
                    //in the collection array does not need to have anything broadcasted to them.
                    if (player.ActiveGame.GetPlayerCollection()[i].Name == player.Name)
                        continue;
                    else if (player.ActiveGame.GetPlayerCollection()[i].Name == "New BaseCharacter")
                        continue;

                    //If the current player in the iteration is within the same location as the player logging in
                    //we can broadcast to them that the newly logged in player has entered the same Room.
                    if (player.ActiveGame.GetPlayerCollection()[i].CurrentWorldLocation == player.CurrentWorldLocation)
                    {
                        player.ActiveGame.GetPlayerCollection()[i].Send(player.Name + " arrived.");
                    }
                }
            }
        }
    }
}

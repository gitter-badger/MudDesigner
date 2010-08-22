//Microsoft.NET Framework 
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

//Mud Designer Game Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.Commands;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    /// <summary>
    /// The Exit command is used to exit the MUD game.
    /// Using this command while connected to a MUD server will perform a disconnect from the server.
    /// Using the command while running the game in offline mode will simply shut down the game.
    /// </summary>
    public class CommandExit : IGameCommand
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
        /// Constructor for the class.
        /// </summary>
        public CommandExit()
        {
            //Instance the help collection and add our help information to it.
            //Typically the Help content is placed within the constructor, but this particular help document
            //needs to access information from the player, so we will build our Help collection in the Execute command.
            Help = new List<string>();
        }

        /// <summary>
        /// Executes the command.
        /// This method is called from the Command Engine, it is not recommended that you call this method directly.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="player"></param>
        public void Execute(String command, BaseCharacter player)
        {
            //Check if the game is multiplayer. 
            //Multiplayer games require disconnecting from the server and letting other players in the same Room know
            //that this player has left.
            if (player.ActiveGame.IsMultiplayer)
            {
                //Query the Active Games Player collection so that we can build a collection of Players that need to be
                //informed of the Player disconnecting from the Server.
                var playerQuery =
                    from p in player.ActiveGame.GetPlayerCollection()
                    where !p.Name.StartsWith("New") && p.Name != player.Name && p.CurrentWorldLocation == player.CurrentWorldLocation
                    select p;

                //Inform each player found in our LINQ query that the player has disconnected from the Server.
                foreach (BaseCharacter p in playerQuery)
                    p.Send(player.Name + " has left."); ;

                //TODO: If a player is in a Group then s/he needs to be removed upon disconnecting.
                player.Disconnect();
            }
            else
            {
                //Call the game's shutdown method which will save all objects and exit the game gracefully.
                player.ActiveGame.Shutdown();
            }
        }
    }
}

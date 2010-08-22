//Microsoft.NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Mud Designer Game Engine
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;

namespace MudEngine.Commands
{
    /// <summary>
    /// The GetTime command is used to print the current in-game time to the player.
    /// This command will print the day, month and year along with hour, minute and seconds.
    /// </summary>
    public class CommandGetTime : IGameCommand
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
        public CommandGetTime()
        {
            //Instance the help collection and add our help information to it.
            Help = new List<string>();
            Help.Add("Gives you the current time and date in the game world.");
        }

        /// <summary>
        /// Executes the Command.
        /// This method is called from the Command Engine, it is not recommended that you call this method directly.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="player"></param>
        public void Execute(String command, BaseCharacter player)
        {
            //Send the returned String containing the World Time to the player.
            player.Send(player.ActiveGame.WorldTime.GetCurrentWorldTime());
        }
    }
}

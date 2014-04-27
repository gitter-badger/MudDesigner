using System.ComponentModel;

using Mud.Core;
using Mud.Models.Mobs;

namespace Mud.Commands
{
    /// <summary>
    /// The Say command posts a message from an IMob object to other IPlayer objects.
    /// </summary>
    [ShorthandName("Say", "s")]
    [DisplayName("Say")]
    public class SayCommand : IMudCommand
    {
        /// <summary>
        /// Executes the Say command, supplying an IMob object as the sender along with the string used to enter the command.
        /// </summary>
        /// <param name="sender">The sender sending the message.</param>
        /// <param name="input">The command input including the message content.</param>
        public void Execute(IMob sender, string input)
        {
            // Loop through each player in the current sender's location and broadcast the message.
            foreach (IPlayer player in sender.Location.Players)
            {
                player.SendMessage(input);
            }
        }
    }
}

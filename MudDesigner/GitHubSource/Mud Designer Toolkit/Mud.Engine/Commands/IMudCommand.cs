using System.ComponentModel;    

using Mud.Core;
using Mud.Models.Mobs;

namespace Mud.Commands
{
    /// <summary>
    /// The IMudCommand interface exposes members required for objects to implement a Mud based in-game command.
    /// </summary>
    public interface IMudCommand
    {
        /// <summary>
        /// Executes the command, supplying an IMob object as the sender along with the string used to enter the command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="input">The command input.</param>
        void Execute(IMob sender, string input);
    }
}

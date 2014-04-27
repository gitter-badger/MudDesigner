using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mud.Models.Environment;

namespace Mud.Models.Mobs
{
    /// <summary>
    /// A enumerator for determing at what level this character can broadcast messages to.
    /// </summary>
    public enum MessageBroadcastLevels
    {
        Zone,
        Room,
    }

    public interface IMob
    {
        IGender Gender { get; set; }

        IRoom Location { get; set; }

        /// <summary>
        /// Gets or Sets if this character can talk or not.
        /// </summary>
        bool IsMute { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of items this character can hold in their inventory
        /// </summary>
        int MaximumInventorySize { get; set; }

        // Non-Player based Mobs can use commands as well.
        bool ProcessCommand(byte[] data);
        bool ProcessCommand(string command);

        /// <summary>
        /// Sends a message from the character, to all occupants within the broadcast range specified.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="broadcastLevel"></param>
        void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room);

        /// <summary>
        /// Sends a message from the character, to the target character
        /// </summary>
        /// <param name="message"></param>
        /// <param name="target"></param>
        void Talk(string message, IMob target);

        /// <summary>
        /// Sends a message from the character, to the target group of characters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="group"></param>
        void Talk(string message, IMob[] group);
    }
}

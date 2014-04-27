using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mud.Models.Mobs;

namespace Mud.Models.Environment
{
    /// <summary>
    /// IRealm provides methods for managing a collection of Zones and broadcasting messages to the users that inhabit the environments associated with the Realm.
    /// Realm's are not used to physically traverse in the world; they are used to segregate IRoom's from each other to help make the world more modular.
    /// </summary>
    public interface IRealm : IGameObject
    {
        /// <summary>
        /// Gets a collection of Zones belonging to the Realm.
        /// </summary>
        List<IZone> Zones { get; }

        /// <summary>
        /// Gets or Sets the World that owns the Realm. Only 1 World can ever exist in the Game.
        /// </summary>
        IWorld World { get; set; }

        /// <summary>
        /// Broadcasts a chat message to the Realm.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="zones">A collection of Zones to broadcast to. Any Zone not included in the collection will not receive the message</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        void BroadcastMessage(string message, IZone[] zones, IPlayer playersToOmit = null);

        /// <summary>
        /// Broadcasts a chat message to the Realm.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="rooms">A collection of Rooms to broadcast to. Any Room not included in the collection will not receive the message.</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        void BroadcastMessage(string message, IRoom[] rooms, IPlayer playersToOmit = null);
    }
}

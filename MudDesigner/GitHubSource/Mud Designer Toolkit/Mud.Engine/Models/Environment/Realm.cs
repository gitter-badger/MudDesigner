// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Mud Designer Framework
using Mud.Models.Mobs;

namespace Mud.Models.Environment
{
    /// <summary>
    /// Realm provides methods for managing a collection of Zones and broadcasting messages to the users that inhabit the environments associated with the Realm.
    /// Realm's are not used to physically traverse in the world; they are used to segregate IRoom's from each other to help make the world more modular.
    /// </summary>
    public class Realm : Gameobject
    {
        /// <summary>
        /// Gets a collection of Zones belonging to the Realm.
        /// </summary>
        public List<IZone> Zones { get; private set; }

        /// <summary>
        /// Gets or Sets the World that owns the Realm. Only 1 World can ever exist in the Game.
        /// </summary>
        public IWorld World { get; set; }

        public Realm()
        {
            this.Zones = new List<IZone>();
        }

        public Realm(IWorld world) : this()
        {
            this.World = world;
        }

        /// <summary>
        /// Broadcasts a chat message to the Realm.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="zones">A collection of Zones to broadcast to. Any Zone not included in the collection will not receive the message</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        public void BroadcastMessage(string message, IZone[] zones, IPlayer playersToOmit = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a chat message to the Realm.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="rooms">A collection of Rooms to broadcast to. Any Room not included in the collection will not receive the message.</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        public void BroadcastMessage(string message, IRoom[] rooms, IPlayer playersToOmit = null)
        {
            throw new NotImplementedException();
        }
    }
}

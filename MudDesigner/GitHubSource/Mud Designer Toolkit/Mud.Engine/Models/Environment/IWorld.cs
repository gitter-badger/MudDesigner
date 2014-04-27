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
    /// The World is responsible for managing the Realms contained within the game. The World acts as a central repository for all of the Environment objects.
    /// There can only ever be 1 World associated with the Game.
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Gets or Sets the name of the world that the player has entered.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description for the world.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets a collection of Realms associated with the World.
        /// It is not guaranteed that any given Realm will contain Zones.
        /// </summary>
        List<IRealm> Realms { get; }

        /// <summary>
        /// Adds a Realm to the World. It is not guaranteed that a Realm will be added; 
        /// Validation is performed to ensure a Realm is properly configured prior to being added to the World. 
        /// If a Realm is not properly configured, it will not be added. 
        /// Note that Realms must have a unique name and ID.
        /// </summary>
        /// <param name="realm">The Realm that needs to be added to the World.</param>
        /// <param name="overwrite">If true, any existing Realm matching the supplied Realm will be overwrote.</param>
        /// <returns>Returns true if the Realm was added.</returns>
        bool Add(IRealm realm, bool overwrite = false);

        /// <summary>
        /// Removes a Realm from the World.
        /// </summary>
        /// <param name="realm">The Realm that needs to be removed.</param>
        /// <returns>Returns true if the Realm was removed.</returns>
        bool Remove(IRealm realm);

        /// <summary>
        /// Removes a Realm from the World.
        /// </summary>
        /// <param name="name">The unique name of the Realm to be removed.</param>
        /// <returns>Returns true if the Realm was removed.</returns>
        bool Remove(string name);

        /// <summary>
        /// Removes a Realm from the World.
        /// </summary>
        /// <param name="identifier">The unique identifier of the Realm to be removed.</param>
        /// <returns>Returns true if the Realm was removed.</returns>
        bool Remove(int identifier);

        /// <summary>
        /// Clears the World of all Realms.
        /// This will remove all of the Zones and Rooms associated with the Realm.
        /// </summary>
        /// <returns>Returns true if the clear is completed.</returns>
        bool Clear();

        /// <summary>
        /// Inserts a new Realm at a specific index within the World's Realm collection.
        /// The Realm is inserted into the IWorld.Realms collection property.
        /// </summary>
        /// <param name="realm">The Realm to insert into the World.</param>
        /// <param name="index">The index that the Realm needs to be inserted into.</param>
        /// <returns></returns>
        bool Insert(IRealm realm, int index);

        /// <summary>
        /// Moves a Realm from one index in the IWorld.Realms collection to another index.
        /// </summary>
        /// <param name="realm">The Realm to move.</param>
        /// <param name="originalIndex">The original index location in the IWorld.Realms collection.</param>
        /// <param name="newIndex">The desired index location within the IWorld.Realms collection.</param>
        /// <returns></returns>
        bool Move(IRealm realm, int originalIndex, int newIndex);

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        void BroadcastMessage(string message, IPlayer[] playersToOmit = null);

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="zones">A collection of Realms to broadcast to. Any Realm not included in the collection will not receive the message</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        void BroadcastMessage(string message, IRealm[] realms, IPlayer[] playersToOmit = null);

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="zones">A collection of Zones to broadcast to. Any Zone not included in the collection will not receive the message</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        void BroadcastMessage(string message, IZone[] zones, IPlayer[] playersToOmit = null);

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="rooms">A collection of Rooms to broadcast to. Any Room not included in the collection will not receive the message.</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        void BroadcastMessage(string message, IRoom[] rooms, IPlayer[] playersToOmit = null);
    }
}

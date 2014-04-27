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
    public class World : IWorld
    {
        /// <summary>
        /// Gets or Sets the name of the Gameobject
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description for the world.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the Identifier for the Game object
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets a collection of Realms associated with the World.
        /// It is not guaranteed that any given Realm will contain Zones.
        /// </summary>
        public List<IRealm> Realms { get; set; }

        /// <summary>
        /// Gets or Sets if the Game object can be edited during run-time.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Adds a Realm to the World. It is not guaranteed that a Realm will be added; 
        /// Validation is performed to ensure a Realm is properly configured prior to being added to the World. 
        /// If a Realm is not properly configured, it will not be added. 
        /// Note that Realms must have a unique name and ID.
        /// </summary>
        /// <param name="realm">The Realm that needs to be added to the World.</param>
        /// <param name="overwrite">If true, any existing Realm matching the supplied Realm will be overwrote.</param>
        /// <returns>Returns true if the Realm was added.</returns>
        public bool Add(IRealm realm, bool overwrite = false)
        {
            if (realm.ID == 0 || string.IsNullOrWhiteSpace(realm.Name))
                return false;

            // Check if we have any existing realms.
            var realms = this.Realms.Select(r => r.ID == realm.ID && r.Name == realm.Name);

            // If there are no existing realms matching the new realm, we add it.
            if (realms.Count() == 0)
            {
                this.Realms.Add(realm);
                return true;
            }
            else
            {
                // If we are supposed to overwrite the existing realm, we search for it, remove it and then add the new realm.
                if (overwrite)
                {
                    // Remove the old realm.
                    IRealm existinRealm = this.Realms.Single(r => r.ID == realm.ID && r.Name == realm.Name);
                    this.Realms.Remove(existinRealm);
                    // Add the new.
                    this.Realms.Add(realm);
                }
                // If we are not supposed to overwrite, then we just return as uncompleted.

                return false;
            }
        }

        /// <summary>
        /// Removes a Realm from the World.
        /// </summary>
        /// <param name="realm">The Realm that needs to be removed.</param>
        /// <returns>Returns true if the Realm was removed.</returns>
        public bool Remove(IRealm realm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a Realm from the World.
        /// </summary>
        /// <param name="name">The unique name of the Realm to be removed.</param>
        /// <returns>Returns true if the Realm was removed.</returns>
        public bool Remove(string name)
        {
            // Search for the realm requested.
            IRealm existingRealm = this.Realms.Single(r => r.Name == name);

            // If it doesn't exist, then we can't remove it.
            // We don't return true because we need to let the user know in some way that the requested Realm does not exist.
            if (existingRealm == null)
                return false;

            // Remove the realm
            this.Realms.Remove(existingRealm);
            return true;
        }

        /// <summary>
        /// Removes a Realm from the World.
        /// </summary>
        /// <param name="indentifier">The unique identifier of the Realm to be removed.</param>
        /// <returns>Returns true if the Realm was removed.</returns>
        public bool Remove(int identifier)
        {
            // Search for the realm requested.
            IRealm existingRealm = this.Realms.Single(r => r.ID == identifier);

            // If it doesn't exist, then we can't remove it.
            // We don't return true because we need to let the user know in some way that the requested Realm does not exist.
            if (existingRealm == null)
                return false;

            // Remove the realm
            this.Realms.Remove(existingRealm);
            return true;
        }

        /// <summary>
        /// Clears the World of all Realms.
        /// This will remove all of the Zones and Rooms associated with the Realm.
        /// </summary>
        /// <returns>Returns true if the clear is completed.</returns>
        public bool Clear()
        {
            // TODO: All players should be disconnected when this is performed or moved into a purgetory state.
            this.Realms.Clear();
            return true;
        }

        /// <summary>
        /// Inserts a new Realm at a specific index within the World's Realm collection.
        /// The Realm is inserted into the IWorld.Realms collection property.
        /// </summary>
        /// <param name="realm">The Realm to insert into the World.</param>
        /// <param name="index">The index that the Realm needs to be inserted into.</param>
        /// <returns></returns>
        public bool Insert(IRealm realm, int index)
        {
            // We do not insert anything in the event that the realm is null...
            if (realm == null)
                return false;

            // ... or the index is greater than the collection count.
            if (index > this.Realms.Count)
                return false;

            // Insert the realm into the World.
            // Technically the order of Realms in the collection has no effect on the game while running.
            this.Realms.Insert(index, realm);
            return true;
        }

        /// <summary>
        /// Moves a Realm from one index in the IWorld.Realms collection to another index.
        /// </summary>
        /// <param name="realm">The Realm to move.</param>
        /// <param name="originalIndex">The original index location in the IWorld.Realms collection.</param>
        /// <param name="newIndex">The desired index location within the IWorld.Realms collection.</param>
        /// <returns></returns>
        public bool Move(IRealm realm, int originalIndex, int newIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        public void BroadcastMessage(string message, IPlayer[] playersToOmit = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="zones">A collection of Realms to broadcast to. Any Realm not included in the collection will not receive the message</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        public void BroadcastMessage(string message, IRealm[] realms, IPlayer[] playersToOmit = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="zones">A collection of Zones to broadcast to. Any Zone not included in the collection will not receive the message</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        public void BroadcastMessage(string message, IZone[] zones, IPlayer[] playersToOmit = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a chat message to the World.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="rooms">A collection of Rooms to broadcast to. Any Room not included in the collection will not receive the message.</param>
        /// <param name="playersToOmit">A collection of players that are prohibited from seeing the message.</param>
        public void BroadcastMessage(string message, IRoom[] rooms, IPlayer[] playersToOmit = null)
        {
            throw new NotImplementedException();
        }
    }
}

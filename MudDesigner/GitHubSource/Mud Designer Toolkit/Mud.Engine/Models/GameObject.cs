// Microsoft .NET Framework
using System;

// Mud Designer Framework
using Mud.Models.Mobs;

namespace Mud.Models
{
    /// <summary>
    /// The base game object within the mud games
    /// </summary>
    public class Gameobject : IGameObject
    {
        /// <summary>
        /// Gets or Sets the name of the Gameobject
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Description that represents what this object is. The default scripts use this to output [Description objectNameInInventory] command results.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the Identifier for the Game object
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or Sets if the Game object can be edited during run-time.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or Sets if this object is permanent and can not be destroyed or removed during run-time.
        /// </summary>
        public bool IsPermanent { get; set; }

        /// <summary>
        /// Gets if this object has been destroyed.
        /// </summary>
        public bool IsDestroyed { get; protected set; }

        /// <summary>
        /// Gets or sets the last saved.
        /// </summary>
        /// <value>
        /// The last saved.
        /// </value>
        public DateTime LastSaved { get; set; }

        /// <summary>
        /// Destroys the object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Destroy()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the state.
        /// </summary>
        /// <param name="copyFrom">The copy from.</param>
        /// <param name="ignoreNullProperties">if set to <c>true</c> [ignore null properties].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void CopyState(ref IGameObject copyFrom, bool ignoreNullProperties = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="player">The player.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BroadcastMessage(string message, IPlayer player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="players">The players.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BroadcastMessage(string message, IPlayer[] players)
        {
            throw new NotImplementedException();
        }
    }
}

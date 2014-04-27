// Microsoft .NET Framework
using System;

// Mud Designer Framework
using Mud.Models.Mobs;

namespace Mud.Models
{
    /// <summary>
    /// Gameobject is the base object of all object's in the Game. 
    /// Any class that implements this interface can be persisted to disk using any class implementing Mud.Data.IDataContext.
    /// 
    /// If an object needs to be saved to disk but does not implement IGameObject, it will need to be saved using manual methods.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Gets or Sets the name of the Gameobject
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Description that represents what this object is. The default scripts use this to output [Description objectNameInInventory] command results.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or Sets the Identifier for the Game object
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets or Sets if the Game object can be edited during run-time.
        /// </summary>
        bool IsEditable { get; set; }

        /// <summary>
        /// Gets or Sets if this object is permanent and can not be destroyed or removed during run-time.
        /// </summary>
        bool IsPermanent { get; set; }

        /// <summary>
        /// Gets if this object has been destroyed.
        /// </summary>
        bool IsDestroyed { get; }

        /// <summary>
        /// Gets or sets the last saved.
        /// </summary>
        /// <value>
        /// The last saved.
        /// </value>
        DateTime LastSaved { get; set; }

        /// <summary>
        /// Destroys the object.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Copies the properties from one object to the receiver.
        /// </summary>
        /// <param name="copyFrom">The object to copy properties from.</param>
        /// <param name="ignoreNullProperties">If true then existing property values on the receiver (this) will not be overwrote with the properties from copyFrom..</param>
        void CopyState(ref IGameObject copyFrom, bool ignoreExistingPropertyValues = false);

        /// <summary>
        /// Broadcasts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="player">The player.</param>
        void BroadcastMessage(string message, IPlayer player);

        /// <summary>
        /// Broadcasts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="players">The players.</param>
        void BroadcastMessage(string message, IPlayer[] players);
    }
}

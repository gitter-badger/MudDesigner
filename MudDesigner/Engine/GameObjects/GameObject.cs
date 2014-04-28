using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects
{
    /// <summary>
    /// The base object of all game objects in the engine.
    /// </summary>
    public class GameObject : IGameObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [StorageFilename]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editable.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is permanent.
        /// </summary>
        public bool IsPermanent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is destroyed.
        /// </summary>
        public bool IsDestroyed { get; set; }
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Copies the state of one IGameObject to this one..
        /// </summary>
        /// <param name="copyFrom">The object to copy from.</param>
        /// <param name="ignoreExistingPropertyValues">if set to <c>true</c> [ignore existing property values].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void CopyState(IGameObject copyFrom, bool ignoreExistingPropertyValues = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a message to a player, regardless of its location.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="player">The player.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BroadcastMessage(string message, Mob.IPlayer player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Broadcasts a message to a group of players, regardless of their location.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="players">The players.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BroadcastMessage(string message, Mob.IPlayer[] players)
        {
            throw new NotImplementedException();
        }
    }
}

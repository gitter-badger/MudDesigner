using System;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Objects
{
    public interface IGameObject : ISaveable, ILoadable
    {
        /// <summary>
        /// The GUID for this object
        /// </summary>
        Guid ID { get;}

        /// <summary>
        /// Name of the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description of this object.
        /// </summary>
        string Description { get; set; }
        
        /// <summary>
        /// Gets or Sets if the object is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or Sets if this object can never be deleted from the game.
        /// </summary>
        bool Permanent { get; set; }

        /// <summary>
        /// Gets if this GameObject has been destroyed.  If True, Save() code is ignored.
        /// </summary>
        bool Destroyed { get; }
        
        /// <summary>
        /// Called when the object is no longer needed.
        /// </summary>
        void Destroy();
    }
}
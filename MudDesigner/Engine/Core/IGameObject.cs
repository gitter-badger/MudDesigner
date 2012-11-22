/* IGameObject
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: This is the base class for all Game Objects in the engine.
 */

//Microsoft .NET using statements
using System;
using System.ComponentModel;
using System.Globalization;

namespace MudDesigner.Engine.Core
{
    public interface IGameObject
    {
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

        /// <summary>
        /// Copies the current values of this object to a new object
        /// </summary>
        /// <param name="copyTo">The object that should have it's properties overwritten with the values of the calling Object</param>
        void CopyState(ref IGameObject copyFrom);
    }
}
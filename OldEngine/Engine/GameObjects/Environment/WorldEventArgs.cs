//-----------------------------------------------------------------------
// <copyright file="WorldEventArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.GameObjects.Environment
{
    /// <summary>
    /// An event argument provided when a World fires an event
    /// </summary>
    public class WorldEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldEventArgs"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public WorldEventArgs(IWorld world)
        {
            this.World = world;
        }

        /// <summary>
        /// Gets the world associated with this event.
        /// </summary>
        public IWorld World { get; private set; }
    }
}

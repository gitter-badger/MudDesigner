using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    public interface IZone : IEnvironment
    {
        /// <summary>
        /// Gets or sets the rooms within this Zone.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        IEnumerable<IRoom> Rooms { get; set; }
    }
}

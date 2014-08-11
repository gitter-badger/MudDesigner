using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    public interface IRealm : IEnvironment
    {
        /// <summary>
        /// Gets or sets the zones within this Realm.
        /// </summary>
        /// <value>
        /// The zones.
        /// </value>
        IEnumerable<IZone> Zones { get; set; }
    }
}

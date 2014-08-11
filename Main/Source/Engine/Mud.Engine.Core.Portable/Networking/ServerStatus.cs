using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Networking
{
    public enum ServerStatus
    {
        /// <summary>
        /// The server has stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// Server is in the process of starting.
        /// </summary>
        Starting,

        /// <summary>
        /// Server is up and running.
        /// </summary>
        Running
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    /// <summary>
    /// Allows a specific time of day to be given a state.
    /// </summary>
    public interface ITimeOfDayState
    {
        /// <summary>
        /// Gets the name of this state.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the time of day in the game that this state begins in hours.
        /// </summary>
        /// <value>
        /// The state start time.
        /// </value>
        float StateStartTime { get; }

        /// <summary>
        /// Gets or sets the state transition time.
        /// </summary>
        /// <value>
        /// The state transition time.
        /// </value>
        float StateTransitionTime { get; set; }
    }
}

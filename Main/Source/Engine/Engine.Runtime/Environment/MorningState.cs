//-----------------------------------------------------------------------
// <copyright file="MorningState.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Shared.Environment;
namespace Mud.Engine.Runtime.Environment
{
    /// <summary>
    /// A state representing mornings for a specific time frame.
    /// </summary>
    public class MorningState : TimeOfDayState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MorningState"/> class.
        /// </summary>
        public MorningState()
        {
            this.StateStartTime = new TimeOfDay();
            this.StateStartTime.Hour = 5;
        }

        /// <summary>
        /// Gets the name of this state.
        /// </summary>
        public override string Name 
        { 
            get 
            { 
                return "Morning"; 
            } 
        }

        /// <summary>
        /// Gets the time of day in the game that this state begins.
        /// </summary>
        /// <value>
        /// The state start time.
        /// </value>
        public override ITimeOfDay StateStartTime { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="NightState.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Shared.Environment;
namespace Mud.Engine.Runtime.Environment
{
    /// <summary>
    /// A state representing night time.
    /// </summary>
    public class NightState : TimeOfDayState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NightState"/> class.
        /// </summary>
        public NightState()
        {
            this.StateStartTime = new TimeOfDay();
            this.StateStartTime.Hour = 18;
        }

        /// <summary>
        /// Gets the name of this state.
        /// </summary>
        public override string Name 
        { 
            get 
            { 
                return "Night"; 
            } 
        }

        /// <summary>
        /// Gets the time of day in the game that this state begins.
        /// </summary>
        public override ITimeOfDay StateStartTime { get; set; }
    }
}

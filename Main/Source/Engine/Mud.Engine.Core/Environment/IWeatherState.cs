using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    /// <summary>
    /// Defines a state that weather can be in.
    /// </summary>
    public interface IWeatherState
    {
        /// <summary>
        /// Gets or sets the occurance probability of this weather state occuring in an environment.
        /// The higher the probability relative to other weather states, the more likely it is going to occure.
        /// </summary>
        /// <value>
        /// The occurance probability.
        /// </value>
        int OccuranceProbability { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="TimeOfDayChangedEventArgs.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Environment
{
    using System;

    /// <summary>
    /// Event arguments for when the time of day changes.
    /// </summary>
    public class TimeOfDayChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeOfDayChangedEventArgs"/> class.
        /// </summary>
        /// <param name="transitionFrom">The transition from.</param>
        /// <param name="transitionTo">The transition to.</param>
        public TimeOfDayChangedEventArgs(ITimeOfDayState transitionFrom, ITimeOfDayState transitionTo)
        {
            this.TransitioningFrom = transitionFrom;
            this.TransitioningTo = transitionTo;
        }

        /// <summary>
        /// Gets the state that is being transitioned away from.
        /// </summary>
        public ITimeOfDayState TransitioningFrom { get; private set; }

        /// <summary>
        /// Gets the state that is being transitioned to.
        /// </summary>
        public ITimeOfDayState TransitioningTo { get; private set; }
    }
}

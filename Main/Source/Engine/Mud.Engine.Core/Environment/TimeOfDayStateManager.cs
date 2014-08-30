using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    internal class TimeOfDayStateManager
    {
        private IEnumerable<ITimeOfDayState> timeOfDayStates;

        internal TimeOfDayStateManager(IEnumerable<ITimeOfDayState> states)
        {
            this.timeOfDayStates = states;
        }

        /// <summary>
        /// Looks at a supplied time of day and figures out what TimeOfDayState needs to be returned that matches the time of day.
        /// </summary>
        /// <param name="currentTime">The current time.</param>
        /// <returns>
        /// Returns an instance of ITimeOfDayState that represents the current time of day in the game.
        /// </returns>
        internal ITimeOfDayState GetTimeOfDayState(DateTime? currentTime = null)
        {
            TimeOfDay time = new TimeOfDay();
            time.Hour = currentTime.Value.Hour;
            time.Minute = currentTime.Value.Minute;

            return this.GetTimeOfDayState(time);
        }

        /// <summary>
        /// Looks at a supplied time of day and figures out what TimeOfDayState needs to be returned that matches the time of day.
        /// </summary>
        /// <param name="currentGameTime">The current time.</param>
        /// <returns>
        /// Returns an instance of ITimeOfDayState that represents the current time of day in the game.
        /// </returns>
        internal ITimeOfDayState GetTimeOfDayState(TimeOfDay currentGameTime = null)
        {
            ITimeOfDayState inProgressState = null;
            ITimeOfDayState nextState = null;

            Parallel.Invoke(
                () => inProgressState = this.GetInProgressState(currentGameTime),
                () => nextState = this.GetNextState(currentGameTime));

            if (inProgressState != null)
            {
                return inProgressState;
            }
            else if (nextState != null && nextState.StateStartTime.Hour <= currentGameTime.Hour && nextState.StateStartTime.Minute <= currentGameTime.Minute)
            {
                return nextState;
            }

            return null;
        }

        /// <summary>
        /// Gets a state if there is one already in progress.
        /// </summary>
        /// <param name="currentTime">The current time.</param>
        /// <returns>
        /// Returns an instance of ITimeOfDayState that represents the current time of day if an instance with a StartTime 
        /// before the current world-time can be found 
        /// </returns>
        private ITimeOfDayState GetInProgressState(TimeOfDay currentTime)
        {
            ITimeOfDayState inProgressState = null;
            foreach (ITimeOfDayState state in this.timeOfDayStates)
            {
                // If the state is already in progress, w
                if (state.StateStartTime.Hour <= currentTime.Hour ||
                    (state.StateStartTime.Hour <= currentTime.Hour && state.StateStartTime.Minute <= currentTime.Minute))
                {
                    if (inProgressState == null)
                    {
                        inProgressState = state;
                        continue;
                    }
                    else
                    {
                        if (inProgressState.StateStartTime.Hour <= currentTime.Hour &&
                            inProgressState.StateStartTime.Minute <= currentTime.Minute)
                        {
                            inProgressState = state;
                        }
                    }
                }
            }

            return inProgressState;
        }

        /// <summary>
        /// Gets the state that is up next.
        /// </summary>
        /// <param name="currentTime">The current time.</param>
        /// <returns>
        /// Returns an instance of ITimeOfDayState that represents the up coming time of day if an instance with a StartTime 
        /// after the current world-time can be found 
        /// </returns>
        private ITimeOfDayState GetNextState(TimeOfDay currentTime)
        {
            ITimeOfDayState nextState = null;
            foreach (ITimeOfDayState state in this.timeOfDayStates)
            {
                // If this state is a future state, then preserve it as a possible next state.
                if (state.StateStartTime.Hour > currentTime.Hour ||
                    (state.StateStartTime.Hour >= currentTime.Hour && state.StateStartTime.Minute > currentTime.Minute))
                {
                    // If we do not have a next state, set it.
                    if (nextState == null)
                    {
                        nextState = state;
                        continue;
                    }
                    else
                    {
                        // We have a next state, so we must check which is sooner.
                        if (nextState.StateStartTime.Hour > state.StateStartTime.Hour &&
                            nextState.StateStartTime.Minute >= state.StateStartTime.Minute)
                        {
                            nextState = state;
                        }
                    }
                }
            }

            return nextState;
        }
    }
}

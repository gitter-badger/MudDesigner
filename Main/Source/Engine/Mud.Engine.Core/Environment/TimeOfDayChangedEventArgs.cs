using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class TimeOfDayChangedEventArgs : EventArgs
    {
        public TimeOfDayChangedEventArgs(ITimeOfDayState transitionFrom, ITimeOfDayState transitionTo)
        {
            this.TransitioningFrom = transitionFrom;
            this.TransitioningTo = transitionTo;
        }

        public ITimeOfDayState TransitioningFrom { get; private set; }

        public ITimeOfDayState TransitioningTo { get; private set; }
    }
}

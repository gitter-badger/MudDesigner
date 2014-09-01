using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Time
{
    public class AfternoonState : TimeOfDayState
    {
        public AfternoonState()
        {
            this.StateStartTime = new TimeOfDay();
            this.StateStartTime.Hour = 12;
        }

        public override string Name { get { return "Afternoon"; } }

        public override TimeOfDay StateStartTime { get; set; }
    }
}

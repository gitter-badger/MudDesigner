using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class NightState : TimeOfDayState
    {
        public NightState()
        {
            this.StateStartTime = new TimeOfDay();
            this.StateStartTime.Hour = 18;
        }

        public override string Name { get { return "Night"; } }

        public override TimeOfDay StateStartTime { get; set; }
    }
}

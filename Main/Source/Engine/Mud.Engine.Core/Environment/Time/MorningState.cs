using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mud.Engine.Core.Engine;

namespace Mud.Engine.Core.Environment.Time
{
    public class MorningState : TimeOfDayState
    {
        public MorningState()
        {
            this.StateStartTime = new TimeOfDay();
            this.StateStartTime.Hour = 5;
        }

        public override string Name { get { return "Morning"; } }

        public override TimeOfDay StateStartTime { get; set; }
    }
}

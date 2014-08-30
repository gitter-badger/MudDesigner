using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class SilenceRule : IZoneRule
    {
        public string Name { get; private set; }

        public bool Enabled { get; set; }

        public void Execute()
        {
            // Stub
        }
    }
}

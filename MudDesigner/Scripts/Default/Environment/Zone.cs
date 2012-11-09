using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Environment
{
    public class Zone : BaseZone
    {
        public Zone(): base()
        {
        }

        public Zone(string name)
            : base(name)
        {
        }


        public Zone(string name, IRealm realm) : base(name, realm)
        {
        }
    }
}

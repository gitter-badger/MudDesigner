using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Environment
{
    public class DefaultZone : BaseZone
    {
        public DefaultZone(): base()
        {
        }

        public DefaultZone(string name)
            : base(name)
        {
        }


        public DefaultZone(string name, IRealm realm) : base(name, realm)
        {
        }
    }
}

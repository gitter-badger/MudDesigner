using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Environment
{
    class DefaultRoom : BaseRoom
    {
        public DefaultRoom() : base()
        {
            
        }
        public DefaultRoom(string name, IZone zone) : base(name, zone)
        {
        }
    }
}

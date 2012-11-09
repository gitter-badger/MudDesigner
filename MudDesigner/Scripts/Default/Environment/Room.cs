using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Environment
{
    class Room : BaseRoom
    {
        public Room() : base()
        {
            
        }
        public Room(string name, IZone zone) : base(name, zone)
        {
        }
    }
}

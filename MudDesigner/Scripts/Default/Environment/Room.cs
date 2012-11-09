using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Environment
{
    class Room : MudDesigner.Engine.Environment.BaseRoom
    {
        public Room() : base()
        {
            
        }
        public Room(string name, IZone zone) : base(name, zone)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Environment
{
    public class DefaultDoor : Engine.Environment.Door
    {
        public DefaultDoor() :base()
        {
            
        }
        public DefaultDoor(AvailableTravelDirections direction, IRoom departing, IRoom arrival)
            : base(direction, departing, arrival)
        {
        }
    }
}

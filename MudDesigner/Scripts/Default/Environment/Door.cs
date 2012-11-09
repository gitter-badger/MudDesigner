using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Default.Environment
{
    public class Door : Engine.Environment.Door
    {
        public Door() :base()
        {
            
        }
        public Door(AvailableTravelDirections direction, IRoom departing, IRoom arrival)
            : base(direction, departing, arrival)
        {
        }
    }
}

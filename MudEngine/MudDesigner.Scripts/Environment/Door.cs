using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Environment
{
    public class Door : MudDesigner.Engine.Environment.Door
    {
        public Door(AvailableTravelDirections direction, IRoom departing, IRoom arrival)
            : base(direction, departing, arrival)
        {
        }
    }
}

using Mud.Engine.Core.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class OccupancyChangedEventArgs : EventArgs
    {
        public OccupancyChangedEventArgs(ICharacter occupant, IRoom departureRoom, IRoom arrivalRoom)
        {
            this.Occupant = occupant;
            this.DepartureRoom = departureRoom;
            this.ArrivalRoom = arrivalRoom;
        }

        public ICharacter Occupant { get; private set; }

        public IRoom DepartureRoom { get; private set; }

        public IRoom ArrivalRoom { get; private set; }
    }
}

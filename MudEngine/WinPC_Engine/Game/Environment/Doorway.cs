using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameScripts;

namespace MudEngine.Game.Environment
{
    public class Doorway
    {
        public Boolean Locked { get; set; }

        public BaseScript RequiredKey { get; private set; }

        public Int32 LevelRequirement { get; set; }

        public AvailableTravelDirections TravelDirection { get; set; }

        public Room ArrivalRoom { get; private set; }

        public Room DepartureRoom { get; private set; }

        public Doorway(Room arrival, Room departure, AvailableTravelDirections direction)
        {
            this.TravelDirection = direction;
            this.ArrivalRoom = arrival;
            this.DepartureRoom = departure;

            this.LevelRequirement = 0;
        }

        public override string ToString()
        {
            return "{" + this.GetType().Name + "}: " + this.DepartureRoom.Name + "->" + this.TravelDirection.ToString() + "->" + this.ArrivalRoom.Name;
        }
    }
}

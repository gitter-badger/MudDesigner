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
            if (this.RequiredKey == null)
            {
                return
                    "DepartureRoom-" + this.DepartureRoom.Filename +
                    ">DepartureZone-" + this.DepartureRoom.Zone.Filename + 
                    ">ArrivalRoom-" + this.ArrivalRoom.Filename +
                    ">ArrivalZone-" + this.ArrivalRoom.Zone.Filename + 
                    ">Locked-" + this.Locked.ToString() +
                    ">RequiredKey-None" +
                    ">LevelRequirement-" + this.LevelRequirement.ToString() +
                    ">TravelDirection-" + this.TravelDirection.ToString();
            }
            else
                return
                    "DepartureRoom-" + this.DepartureRoom.Filename +
                    ">DepartureZone-" + this.DepartureRoom.Zone.Filename +
                    ">ArrivalRoom-" + this.ArrivalRoom.Filename +
                    ">ArrivalZone-" + this.ArrivalRoom.Zone.Filename + 
                    ">Locked-" + this.Locked.ToString() +
                    ">RequiredKey-" + this.RequiredKey.Filename +
                    ">LevelRequirement-" + this.LevelRequirement.ToString() +
                    ">TravelDirection-" + this.TravelDirection.ToString();
        }
    }
}

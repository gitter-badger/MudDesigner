using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core;

namespace MudEngine.World
{
    public class Room : BaseEnvironment
    {
        /// <summary>
        /// Gets or Sets the type of terrain that this room contains.
        /// </summary>
        public TerrainTypes Terrain { get; set; }

        /// <summary>
        /// Gets a reference to the collection of doorways present within this room
        /// </summary>
        public List<Door> Doorways { get; private set; }

        public Room(BaseGame game) : base(game)
        {
            Doorways = new List<Door>();
        }

        /// <summary>
        /// Installs a doorway into the room.  Multiple doorways for the same travel direction is not allowed.
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public bool InstallDoor(Door door)
        {
            //Anonymous method to check if a door already exists for the travel direction supplied.
            Door dr = Doorways.Find(delegate(Door d)
            {
                return d.TravelDirection == door.TravelDirection;
            }
            );

            if (dr != null)
                return false;

            Doorways.Add(door);
            return true;
        }

        /// <summary>
        /// Removes a doorway from the room.
        /// </summary>
        /// <param name="travelDirection"></param>
        public void RemoveDoor(AvailableTravelDirections travelDirection)
        {
            Door dr = Doorways.Find(delegate(Door d)
            {
                return d.TravelDirection == travelDirection;
            }
            );

            if (dr != null)
                Doorways.Remove(dr);
        }

        /// <summary>
        /// Checks to see if a doorway exists for the supplied travel direction.
        /// </summary>
        /// <param name="travelDirection"></param>
        /// <returns></returns>
        public bool DoorwayExists(AvailableTravelDirections travelDirection)
        {
            return Doorways.Exists(d => d.TravelDirection == travelDirection);
        }

        /// <summary>
        /// Returns a reference to an installed doorway, for the supplied travel direction.
        /// </summary>
        /// <param name="travelDirection"></param>
        /// <returns></returns>
        public Door GetDoorway(AvailableTravelDirections travelDirection)
        {
            return Doorways.Find(d => d.TravelDirection == travelDirection);
        }
    }
}

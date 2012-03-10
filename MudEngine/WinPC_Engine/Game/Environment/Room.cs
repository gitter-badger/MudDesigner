using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core.Interfaces;
using MudEngine.Core;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.GameScripts;
namespace MudEngine.Game.Environment
{
    public class Room : Environment
    {
        public Zone Zone { get; private set; }

        public Boolean Safe { get; set; }

        public List<StandardCharacter> Occupants { get; private set; }

        public Room(StandardGame game, String name, String description, Zone zone)
            : base(game, name, description)
        {
            this._Doors = new List<Doorway>();
            this.Occupants = new List<StandardCharacter>();
            this.Zone = zone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departureDirection"></param>
        /// <param name="connectedRoom"></param>
        /// <returns></returns>
        public Boolean LinkRooms(AvailableTravelDirections departureDirection, Room connectedRoom)
        {
            foreach (Doorway door in this._Doors)
            {
                if (door.TravelDirection == departureDirection)
                {
                    return false;
                }
            }

            //Create a new doorway and link it to this room
            Doorway d = new Doorway(connectedRoom, this, departureDirection);
            this._Doors.Add(d);

            //Link the connected room
            Boolean successful = connectedRoom.FinishLink(d);
            if (successful)
                return true;
            else
            {
                this._Doors.Remove(d);
                return false;
            }
        }

        protected Boolean FinishLink(Doorway door)
        {
            foreach (Doorway d in this._Doors)
            {
                if (d.TravelDirection == door.TravelDirection)
                {
                    return false;
                }
            }

            Doorway newDoor = new Doorway(door.DepartureRoom, this, TravelDirections.GetReverseDirection(door.TravelDirection));
            this._Doors.Add(newDoor);
            return true;
        }

        public String[] GetDescription()
        {
            return new List<String>().ToArray();
        }

        /// <summary>
        /// Returns a array of Doorways that are currently associated with this Room.
        /// </summary>
        /// <returns></returns>
        public Doorway[] GetDoorways()
        {
            return this._Doors.ToArray();
        }

        public Doorway GetDoorway(AvailableTravelDirections direction)
        {
            foreach (Doorway door in this._Doors)
            {
                if (door.TravelDirection == direction)
                    return door;
            }

            //No direction that matched was found
            return null;
        }

        /// <summary>
        /// Checks if the specified travel direction has a doorway within this Room.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Boolean DoorwayExists(AvailableTravelDirections direction)
        {
            foreach (Doorway door in this._Doors)
            {
                if (door.TravelDirection == direction)
                    return true;
            }

            //No direction that matched was found
            return false;
        }

        public void SendMessageToOccupants(String message)
        {
            foreach (StandardCharacter character in this.Occupants)
            {
                character.SendMessage(message);
            }
        }

        public void SendMessageToOccupants(String message, StandardCharacter exemptCharacter)
        {
            foreach (StandardCharacter character in this.Occupants)
            {
                if (character.Name != exemptCharacter.Name)
                    character.SendMessage(message);
            }
        }

        public void AddOccupant(StandardCharacter character)
        {
            foreach (StandardCharacter c in this.Occupants)
            {
                if (character.Name == character.Name)
                    return;
            }

            this.Occupants.Add(character);
        }

        public override string ToString()
        {
            return "{" + this.GetType().Name + "}:" + this.Zone.Realm + "." + this.Zone.Name + "." + this.Name;
        }

        private List<Doorway> _Doors;
    }
}

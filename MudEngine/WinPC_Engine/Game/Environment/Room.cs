using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudEngine.Core.Interfaces;
using MudEngine.Core;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.GameScripts;
namespace MudEngine.Game.Environment
{
    public class Room : Environment
    {
        public new String Filename
        {
            get
            {
                String path = Path.Combine(this.Game.SavePaths.GetPath(DAL.DataTypes.Environments), this.Zone.Realm.Name, "Zones", this.Zone.Name, "Rooms", this.Name + "." + this.GetType().Name);
                return path;
            }
        }

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

        public override bool Save()
        {
            if (!base.Save(true))
                return false;

            this.SaveData.AddSaveData("Safe", this.Safe.ToString());

            foreach (Doorway door in this._Doors)
            {
                this.SaveData.AddSaveData("Doorway", door.ToString());
            }

            return this.SaveData.Save(this.Filename);
        }

        public override void Load(string filename)
        {
            String path = Path.GetDirectoryName(filename);
            if (!Directory.Exists(path))
                return;

            base.Load(filename);

            Logger.WriteLine("Loading " + this.Name + "...");

            try { this.Safe = Convert.ToBoolean(this.SaveData.GetData("Safe")); }
            catch { this.LoadFailedMessage("Safe"); }

            try
            {
                String[] data = this.SaveData.GetDataCollection("Doorway");

                foreach (String entry in data)
                {
                    String[] values = entry.Split('>');

                    Room departure = new Room(this.Game, String.Empty, String.Empty, this.Zone);
                    Room arrival = new Room(this.Game, String.Empty, String.Empty, this.Zone);

                    foreach (String value in values)
                    {
                        String[] propertyInfo = value.Split('-');

                        switch (propertyInfo[0])
                        {
                            case "DepartureRoom":
                                departure.Load(propertyInfo[1]);
                                break;
                            case "DepartureZone":
                                if (propertyInfo[1] != this.Zone.Filename)
                                    departure.Zone.Load(propertyInfo[1]);
                                else
                                    departure.Zone = this.Zone;
                                break;
                            case "ArrivalRoom":
                                arrival.Load(propertyInfo[1]);
                                break;
                            case "ArrivalZone":
                                if (propertyInfo[1] != this.Zone.Filename)
                                    arrival.Zone.Load(propertyInfo[1]);
                                else
                                    arrival.Zone = this.Zone;
                                break;
                        }
                    }
                }
            }
            catch
            {
                this.LoadFailedMessage("Doorway");
            }
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

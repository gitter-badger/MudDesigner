using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core.Interfaces;
using MudEngine.GameScripts;
using MudEngine.Game.Characters;
using MudEngine.Core;

namespace MudEngine.Game.Environment
{
    public class Zone : BaseScript, IGameComponent, ISavable, IUpdatable
    {
        /// <summary>
        /// Gets or Sets the what stats 
        /// </summary>
        public CharacterStats StatDrain { get; set; }

        public Boolean Safe { get; set; }

        public Realm Realm { get; set; }

        public Zone(StandardGame game, String name, String description) : base(game, name, description)
        {
            this._RoomCollection = new List<Room>();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public string Filename
        {
            get { throw new NotImplementedException(); }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public Room CreateRoom(String name, String description)
        {
            Room room = new Room(this.Game, name, description, this);
            foreach(Room r in this._RoomCollection)
            {
                if (r.Name == name)
                {
                    Logger.WriteLine("An attempt to create a duplicate Room was stopped.  Room '" + name + "' was not created within the Zone '" + this.Name + "'");
                    return null;
                }
            }

            this._RoomCollection.Add(room);
            return room;
        }

        public Boolean LinkRooms(String departingRoom, String arrivalRoom, AvailableTravelDirections direction)
        {
            Room depart, arrival;

            depart = this.GetRoom(departingRoom);
            arrival = this.GetRoom(arrivalRoom);

            if (depart == null || arrivalRoom == null)
                return false;

            Boolean result = depart.LinkRooms(direction, arrival);

            return result;
        }

        public Room GetRoom(String room)
        {
            foreach (Room r in this._RoomCollection)
            {
                if (r.Name == room)
                    return r;
            }

            return null;
        }

        private List<Room> _RoomCollection;
    }
}

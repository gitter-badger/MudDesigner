using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudEngine.Core.Interfaces;
using MudEngine.GameScripts;
using MudEngine.Game.Characters;
using MudEngine.Core;

namespace MudEngine.Game.Environment
{
    public class Zone : Environment
    {
        public new String Filename
        {
            get
            {
                String path = Path.Combine(Path.GetDirectoryName(this.Realm.Filename), "Zones", this.Name + "." + this.GetType().Name);
                return path;
            }
        }

        /// <summary>
        /// Gets or Sets the what stats 
        /// </summary>
        public CharacterStats StatDrain { get; set; }

        public Boolean Safe { get; set; }

        public Realm Realm { get; set; }

        public Zone(StandardGame game, String name, String description, Realm realm) : base(game, name, description)
        {
            this.Realm = realm;
            this._RoomCollection = new List<Room>();
        }

        public override bool Save()
        {
            if (!base.Save(true))
                return false;

            this.SaveData.AddSaveData("Safe", this.Safe.ToString());
            this.SaveData.AddSaveData("CharacterStats", this.StatDrain.ToString());

            return this.SaveData.Save(this.Filename);
        }

        public override void Load(string filename)
        {
            String path = Path.GetDirectoryName(filename);
            if (!Directory.Exists(path))
                return;

            base.Load(filename);

            try { this.Safe = Convert.ToBoolean(this.SaveData.GetData("Safe")); }
            catch { this.LoadFailedMessage("Safe"); }

            try
            {
                String data = this.SaveData.GetData("CharacterStats");
                String[] stats = data.Split('.');
                CharacterStats charStats = new CharacterStats();

                foreach (String stat in stats)
                {
                    String[] value = stat.Split(':');

                    switch (value[0])
                    {
                        case "Strength":
                            charStats.Strength = Convert.ToInt32(value[1]);
                            break;
                        case "Dexterity":
                            charStats.Dexterity = Convert.ToInt32(value[1]);
                            break;
                        case "Constitution":
                            charStats.Constitution = Convert.ToInt32(value[1]);
                            break;
                        case "Wisdom":
                            charStats.Wisdom = Convert.ToInt32(value[1]);
                            break;
                        case "Charisma":
                            charStats.Charisma = Convert.ToInt32(value[1]);
                            break;
                        case "Experience":
                            charStats.Experience = Convert.ToInt32(value[1]);
                            break;
                    }
                }
            }
            catch
            {
                this.LoadFailedMessage("CharacterStats");
            }
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

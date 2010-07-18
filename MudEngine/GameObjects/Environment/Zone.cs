//Microsoft .NET Framework
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects;

namespace MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(Room))]
    public class Zone : BaseObject
    {
        [Category("Environment Information")]
        [DefaultValue(0)]
        [Description("The amount to drain each stat by if StatDrain is enabled.")]
        public int StatDrainAmount
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [Description("Enable or Disable the ability for draining stats while traversing.")]
        [DefaultValue(false)]
        public bool StatDrain
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("Environment Information")]
        [Description("The Realm that this Zone is assigned to. It is not required to be contained within a Realm.")]
        public string Realm
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [Description("Determins if the Player can be attacked within this Room or not.")]
        [DefaultValue(false)]
        public bool IsSafe
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the ability for this Zone to be the initial starting Zone for the game.
        /// </summary>
        [Category("Environment Information")]
        [Description("Sets that this Zone is a starting Zone for the game.")]
        [DefaultValue(false)]
        public bool IsStartingZone
        {
            get;
            set;
        }

        [Category("Environment Information")]
        //[EditorAttribute(typeof(UIRoomEditor), typeof(UITypeEditor))]
        [Description("Collection of Rooms that have been created. Editing the Rooms Collection lets you manage the Zones rooms.")]
        public List<Room> Rooms { get; set; }

        [Category("Environment Information")]
        public string EntranceRoom { get; set; }

        public Zone()
        {
            Rooms = new List<Room>();
            IsSafe = false;
            Realm = "No Realm Associated.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoomName"></param>
        /// <returns></returns>
        public Room GetRoomByName(string name)
        {
            var filterQuery =
                from room in Rooms
                where room.Name == name
                select room;

            foreach (Room room in filterQuery)
            {
                Room r = new Room();
                return (Room)r.Load(room.Name, this.Name);
            }
            return null;
        }

        /// <summary>
        /// Clears out the Zones room collection and re-builds it.
        /// This is a time consuming process if there are a large amount of
        /// of rooms, use sparingly.
        /// </summary>
        public void RebuildRoomCollection()
        {
            Rooms = new List<Room>();
            //Create our collection of Rooms.
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), this.Realm);
            string zonePath = Path.Combine(realmPath, this.Name);

            //incase the zone hasn't been saved yet.
            if (!Directory.Exists(zonePath))
                return;

            //Zone exists, so it's already been saved.
            string[] rooms = Directory.GetFiles(zonePath, "*.room");

            //Clear the existing collection of Rooms
            this.Rooms.Clear();
            //Build a new one based off of the files 
            foreach (string file in rooms)
            {
                Room r = new Room();
                r = (Room)r.Load(Path.GetFileNameWithoutExtension(file));
                //r = (Room)FileManager.Load(file, r);
                this.Rooms.Add(r);
            }

            //Save the re-built Room collection
            this.Save(Path.Combine(zonePath, this.Filename));
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(Room))]
    public class Zone : BaseObject
    {
        [Category("Environment Information")]
        [DefaultValue(0)]
        public int StatDrainAmount
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [DefaultValue(false)]
        public bool StatDrain
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("Environment Information")]
        public string Realm
        {
            get;
            set;
        }
        
        internal List<Room> Rooms { get; set; }

        public Zone()
        {
            throw new NotSupportedException("Parameterless constructors of Type " + this.GetType().FullName + " is not supported.");
        }

        public Zone(Realm realm)
        {
            Rooms = new List<Room>();
            //incase a realm hasn't been assigned to it yet.
            if (this.Realm == null)
                return;

            //Create our collection of Rooms.
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), realm.Name);
            string zonePath = Path.Combine(realmPath, this.Name);
            string[] rooms = Directory.GetFiles(zonePath, "*.room");

            foreach (string file in rooms)
            {
                Room r = new Room();
                r = (Room)FileManager.Load(file, r);
                this.Rooms.Add(r);
            }
        }

        public Room GetRoom(string RoomName)
        {
            foreach (Room r in Rooms)
            {
                if (r.Name == RoomName)
                    return r;
            }

            return null;
        }
    }
}

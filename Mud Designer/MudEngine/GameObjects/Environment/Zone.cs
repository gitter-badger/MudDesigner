using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing.Design;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.UITypeEditors;

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

        [Category("Environment Information")]
        [DefaultValue(false)]
        public bool IsSafe
        {
            get;
            set;
        }
        
        [Category("Environment Information")]
        [EditorAttribute(typeof(UIRoomEditor), typeof(UITypeEditor))]
        [ReadOnly(false)]
        public List<Room> Rooms { get; set; }

        public Zone()
        {
            Rooms = new List<Room>();
            //throw new NotSupportedException("Parameterless constructors of Type " + this.GetType().FullName + " is not supported.");
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

        public void RefreshRoomList()
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

            foreach (string file in rooms)
            {
                Room r = new Room();
                r = (Room)FileManager.Load(file, r);
                this.Rooms.Add(r);
            }
        }
    }
}

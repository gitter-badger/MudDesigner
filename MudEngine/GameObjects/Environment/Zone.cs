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
        public bool IsInitialZone
        {
            get;
            set;
        }

        [Category("Environment Information")]
        //[EditorAttribute(typeof(UIRoomEditor), typeof(UITypeEditor))]
        [Description("Collection of Rooms that have been created. Editing the Rooms Collection lets you manage the Zones rooms.")]
        public List<Room> RoomCollection { get; private set; }

        /// <summary>
        /// Gets the initial Room for this Zone.
        /// </summary>
        [Category("Environment Information")]
        public Room InitialRoom { get; private set; }

        public Zone()
        {
            RoomCollection = new List<Room>();
            IsSafe = false;
            Realm = "No Realm Associated.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoomName"></param>
        /// <returns></returns>
        public Room GetRoom(string name)
        {
            foreach (Room room in RoomCollection)
            {
                if (room.Name == name)
                    return room;
            }
            return null;
        }

        /// <summary>
        /// Adds the supplied room into the Zones Room collection.
        /// </summary>
        /// <param name="room"></param>
        public void AddRoom(Room room)
        {
            if (room.IsInitialRoom)
            {
                //Look if we already have a initial room. If so change it. Only 1 InitialRoom per Zone permitted.
                foreach (Room r in RoomCollection)
                {
                    if (r.IsInitialRoom)
                    {
                        r.IsInitialRoom = false;
                        break;
                    }
                }
            }

            RoomCollection.Add(room);
            if (room.IsInitialRoom)
                InitialRoom = room;
        }
    }
}

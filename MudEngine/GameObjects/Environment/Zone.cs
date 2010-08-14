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
using MudEngine.GameObjects.Items;

namespace MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(Room))]
    public class Zone : BaseObject
    {
        [Category("Environment Information")]
        [DefaultValue(0)]
        [Description("The amount to drain each stat by if StatDrain is enabled.")]
        public Int32 StatDrainAmount
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [Description("Enable or Disable the ability for draining stats while traversing.")]
        [DefaultValue(false)]
        public Boolean StatDrain
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("Environment Information")]
        [Description("The Realm that this Zone is assigned to. It is not required to be contained within a Realm.")]
        public String Realm
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [Description("Determins if the Player can be attacked within this Room or not.")]
        [DefaultValue(false)]
        public Boolean IsSafe
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
        public Boolean IsInitialZone
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

        public Zone(GameManagement.Game game) : base(game)
        {
            RoomCollection = new List<Room>();
            IsSafe = false;
            Realm = "No Realm Associated.";
        }

        public override void Save(String path)
        {
            path = Path.Combine(path, Path.GetFileNameWithoutExtension(Filename));

            base.Save(path);

            String filename  = Path.Combine(path, Filename);

            FileManager.WriteLine(filename, this.IsInitialZone.ToString(), "IsInitialZone");
            FileManager.WriteLine(filename, this.IsSafe.ToString(), "IsSafe");
            FileManager.WriteLine(filename, this.Realm, "Realm");
            FileManager.WriteLine(filename, this.StatDrain.ToString(), "StatDrain");
            FileManager.WriteLine(filename, this.StatDrainAmount.ToString(), "StatDrainAmount");
            FileManager.WriteLine(filename, this.InitialRoom.Filename, "InitialRoom");

            foreach (Room r in RoomCollection)
            {
                FileManager.WriteLine(filename, r.Filename, "Room");
                r.Save(path);
            }
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

            if (room.IsInitialRoom)
                InitialRoom = room;

            //TODO: Check for duplicate Rooms.
            RoomCollection.Add(room);
            room.Zone = Filename;
            room.Realm = Realm;
        }

        public List<Room> GetRoomByFilename(String filename)
        {

            List<Room> rooms = new List<Room>();

            foreach (Room r in RoomCollection)
            {
                if (r.Filename == filename)
                {
                    rooms.Add(r);
                }
            }

            return rooms;
        }

        public void LinkRooms(AvailableTravelDirections departureDirection, Room arrivalRoom, Room departureRoom)
        {
            LinkRooms(departureDirection, arrivalRoom, departureRoom, 0);
        }

        public void LinkRooms(AvailableTravelDirections departureDirection, Room arrivalRoom, Room departureRoom, Int32 requiredLevel)
        {
            LinkRooms(departureDirection, arrivalRoom, departureRoom, requiredLevel, false, null);
        }

        public void LinkRooms(AvailableTravelDirections departureDirection, Room arrivalRoom, Room departureRoom, Int32 requiredLevel, Boolean isLocked, BaseItem requiredKey)
        {
            Door door = new Door(ActiveGame);
            door.ArrivalRoom = arrivalRoom;
            door.DepartureRoom = departureRoom;

            if (isLocked)
            {
                door.IsLocked = isLocked;
                door.RequiredKey = requiredKey;
            }

            door.TravelDirection = departureDirection;

            departureRoom.Doorways.Add(door);
            
            //Now we set up the door for the opposite room.
            door = new Door(ActiveGame);

            door.DepartureRoom = arrivalRoom;
            door.ArrivalRoom = departureRoom;
            if (isLocked)
            {
                door.IsLocked = isLocked;
                door.RequiredKey = requiredKey;
            }

            door.TravelDirection = TravelDirections.GetReverseDirection(departureDirection);
            arrivalRoom.Doorways.Add(door);
        }
    }
}

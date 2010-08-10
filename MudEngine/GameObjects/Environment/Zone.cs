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

        public Zone(GameManagement.Game game) : base(game)
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
        public Room GetRoomByID(Guid id)
        {
            foreach (Room room in RoomCollection)
            {
                if (room.ID == id)
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

            if (room.IsInitialRoom)
                InitialRoom = room;

            //TODO: Check for duplicate Rooms.
            RoomCollection.Add(room);
            room.Zone = Name;
            room.Realm = Realm;
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

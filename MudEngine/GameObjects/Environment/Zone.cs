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
using MudEngine.GameManagement;
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
        public Room InitialRoom { get; set; }

        protected override String SavePath
        {
            get
            {
                return Path.Combine(ActiveGame.DataPaths.Environment, Path.GetFileNameWithoutExtension(this.Realm), "Zones", Path.GetFileNameWithoutExtension(Filename));
            }
        }

        public Zone(GameManagement.Game game)
            : base(game)
        {
            RoomCollection = new List<Room>();
            InitialRoom = new Room(game);
            IsSafe = false;
            Realm = "No Realm Associated.";
        }

        public override void Save()
        {
            base.Save();

            String filename = Path.Combine(SavePath, Filename);

            FileManager.WriteLine(filename, this.IsInitialZone.ToString(), "IsInitialZone");
            FileManager.WriteLine(filename, this.IsSafe.ToString(), "IsSafe");
            FileManager.WriteLine(filename, this.Realm, "Realm");
            FileManager.WriteLine(filename, this.StatDrain.ToString(), "StatDrain");
            FileManager.WriteLine(filename, this.StatDrainAmount.ToString(), "StatDrainAmount");

            if (this.InitialRoom.Name != "New Room")
                FileManager.WriteLine(filename, this.InitialRoom.Filename, "InitialRoom");

            String roomPath = Path.Combine(SavePath, "Rooms");
            foreach (Room r in RoomCollection)
            {
                FileManager.WriteLine(filename, r.Filename, "RoomCollection");
                 r.Save();
            }
        }

        public override void Load(string filename)
        {
            base.Load(filename);

            this.IsInitialZone = Convert.ToBoolean(FileManager.GetData(filename, "IsInitialZone"));
            this.IsSafe = Convert.ToBoolean(FileManager.GetData(filename, "IsSafe"));
            this.Realm = FileManager.GetData(filename, "Realm");
            this.StatDrain = Convert.ToBoolean(FileManager.GetData(filename, "StatDrain"));
            this.StatDrainAmount = Convert.ToInt32(FileManager.GetData(filename, "StatDrainAmount"));

            //Now get the rooms in the zone
            foreach (String room in FileManager.GetCollectionData(filename, "RoomCollection"))
            {
                Room r = new Room(ActiveGame);
                String path = Path.Combine(ActiveGame.DataPaths.Environment, Path.GetFileNameWithoutExtension(this.Realm), "Zones", Path.GetFileNameWithoutExtension(this.Filename), "Rooms");
                r.Load(Path.Combine(path, room));
                RoomCollection.Add(r);

                if (r.IsInitialRoom)
                    this.InitialRoom = r;
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

            //Set the Rooms default senses to that of the Zones provided the Room does 
            //not already have a sense description assigned to it.
            if ((!String.IsNullOrEmpty(this.Feel)) && (String.IsNullOrEmpty(room.Feel)))
                room.Feel = this.Feel;
            if ((!String.IsNullOrEmpty(this.Listen)) && (String.IsNullOrEmpty(room.Listen)))
                room.Listen = this.Listen;
            if ((!String.IsNullOrEmpty(this.Smell)) && (String.IsNullOrEmpty(room.Smell)))
                room.Smell = this.Smell;
        }

        public List<Room> GetRoom(String filename)
        {

            List<Room> rooms = new List<Room>();
            if (!filename.ToLower().EndsWith(".room"))
                filename += ".Room";

            foreach (Room r in RoomCollection)
            {
                if (r.Filename.ToLower() == filename.ToLower())
                {
                    rooms.Add(r);
                }
            }

            return rooms;
        }

        public void RestoreLinkedRooms()
        {
            //Iterate through each Room within this Zones collection and link it with it's corresponding Room.
            foreach (Room r in RoomCollection)
            {
                String filename = ActiveGame.DataPaths.Environment + "\\" + Path.GetFileNameWithoutExtension(r.Realm) + "\\Zones\\" + Path.GetFileNameWithoutExtension(r.Zone) + "\\" + "Rooms\\" + r.Filename;
                //Get how many doors this Room contains
                Int32 count = Convert.ToInt32(FileManager.GetData(filename, "DoorwayCount"));

                List<String> data = new List<string>();

                data = FileManager.GetDataSpan(filename, 5, "DoorwayArrivalRoom", true);

                //If no doors, then skip to the next room in the collection.
                if ((count == 0) || (data.Count == 0))
                    continue;

                for (int x = 0; x < (count * 5); x += 5)
                {
                    Door d = new Door(ActiveGame);
                    Int32 index = x;

                    //Restore the Arrival Room first.
                    if (!d.SetRoom(Door.RoomTravelType.Arrival, data[index]))
                    {
                        Log.Write("Error: Failed to set the Arrival Doorway for Room " + r.RoomLocationWithoutExtension);
                    }

                    if (!d.SetRoom(Door.RoomTravelType.Departure, data[index + 1]))
                    {
                        Log.Write("Error: Failed to set the departure Doorway for Room " + r.RoomLocationWithoutExtension);
                    }

                    //Restore settings.
                    d.IsLocked = Convert.ToBoolean(data[index + 2]);
                    d.LevelRequirement = Convert.ToInt32(data[index + 3]);

                    //Restore the travel direction enum value.
                    Array values = Enum.GetValues(typeof(AvailableTravelDirections));
                    foreach (Int32 value in values)
                    {
                        //Since enum values are not strings, we can't simply just assign the String to the enum
                        String displayName = Enum.GetName(typeof(AvailableTravelDirections), value);

                        //If the value = the String saved, then perform the needed conversion to get our data back
                        if (displayName.ToLower() == data[index + 4].ToLower())
                        {
                            d.TravelDirection = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
                            break;
                        }
                    }

                    r.Doorways.Add(d);
                }
            }
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

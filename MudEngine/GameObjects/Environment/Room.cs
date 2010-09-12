//Microsoft .NET Framework
using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects.Items;
using MudEngine.GameManagement;

namespace MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(Door))]
    public class Room : BaseObject
    {
        [Category("Environment Information")]
        [Description("Allows for linking of Rooms together via Doorways")]
        //[EditorAttribute(typeof(UIDoorwayEditor), typeof(UITypeEditor))]
        [RefreshProperties(RefreshProperties.All)]
        public List<Door> Doorways { get; internal set; }

        [ReadOnly(true)]
        [Description("This is the Zone that the Room is currently assigned to.")]
        [Category("Environment Information")]
        public String Zone
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Description("This is the Realm that the Room belongs to.")]
        [Category("Environment Information")]
        public String Realm
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [DefaultValue(false)]
        [Description("Determins if the Player can be attacked within this Room or not.")]
        public Boolean IsSafe
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets if this is the starting room for the Zone that contains it.
        /// </summary>
        [Browsable(true)]
        [Description("Sets if this is the starting room for the Zone that contains it.")]
        public Boolean IsInitialRoom
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current complete location for this Room, including Realm and Zone paths.
        /// This includes the objects Filenames.
        /// </summary>
        public String RoomLocation
        {
            get
            {
                return this.Realm + ">" + this.Zone + ">" + this.Filename;
            }
        }

        /// <summary>
        /// Gets the current complete location for this Room, including Realm and Zone paths.
        /// This includes the objects Filenames without their file extension.
        /// </summary>
        public String RoomLocationWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Realm) + ">" + Path.GetFileNameWithoutExtension(Zone) + ">" + Path.GetFileNameWithoutExtension(Filename);
            }
        }

        public Room(Game game) :base(game)
        {
            Doorways = new List<Door>();

            IsSafe = false;
        }

        /// <summary>
        /// Saves the Room to file within the Game.DataPath.Environment path.
        /// Room is saved within a Realm/Zone/Room directory structure
        /// </summary>
        /// <param name="path"></param>
        public override void Save(String path)
        {
            base.Save(path);

            String filename = Path.Combine(path, Filename);

            FileManager.WriteLine(filename, IsInitialRoom.ToString(), "IsInitialRoom");
            FileManager.WriteLine(filename, this.IsSafe.ToString(), "IsSafe");
            FileManager.WriteLine(filename, this.RoomLocationWithoutExtension, "RoomLocation");

            FileManager.WriteLine(filename, Doorways.Count.ToString(), "DoorwayCount");

            foreach (Door d in Doorways)
            {
                FileManager.WriteLine(filename, d.ArrivalRoom.RoomLocation, "DoorwayArrivalRoom");
                FileManager.WriteLine(filename, d.DepartureRoom.RoomLocation, "DoorwayDepartureRoom");
                FileManager.WriteLine(filename, d.IsLocked.ToString(), "DoorwayIsLocked");
                FileManager.WriteLine(filename, d.LevelRequirement.ToString(), "DoorwayLevelRequirement");
                FileManager.WriteLine(filename, d.TravelDirection.ToString(), "DoorwayTravelDirection");
                //TODO: Save Doorway Keys
            }
        }

        public override void Load(string filename)
        {
            base.Load(filename);

            this.IsInitialRoom = Convert.ToBoolean(FileManager.GetData(filename, "IsInitialRoom"));
            this.IsSafe = Convert.ToBoolean(FileManager.GetData(filename, "IsSafe"));
            String[] env = FileManager.GetData(filename, "RoomLocation").Split('>');

            if (env.Length != 3)
            {
                Log.Write("ERROR: Room " + filename + " does not contain a proper location path in Room.RoomLocation. Path is " + FileManager.GetData(filename, "RoomLocation"));
                return;
            }

            this.Realm = env[0] + ".Realm";
            this.Zone = env[1] + ".Zone";

            //SetRoomToDoorNorth
            //SetRoomToDoorEast
            //etc...

            //Restoring Doorways performed by Zone.RestoreLinkedRooms() Called via GameWorld.Load();
        }

        /// <summary>
        /// Checks to see if a doorway in the travelDirection exists.
        /// </summary>
        /// <param name="travelDirection"></param>
        /// <returns></returns>
        public Boolean DoorwayExist(AvailableTravelDirections travelDirection)
        {
            foreach (Door door in Doorways)
            {
                if (door.TravelDirection == travelDirection)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets reference to the Rooms door connected in the supplied travelDirection
        /// </summary>
        /// <param name="travelDirection"></param>
        /// <returns></returns>
        public Door GetDoor(AvailableTravelDirections travelDirection)
        {
            foreach (Door door in this.Doorways)
            {
                if (door.TravelDirection == travelDirection)
                    return door;
            }
            return null;
        }
    }
}

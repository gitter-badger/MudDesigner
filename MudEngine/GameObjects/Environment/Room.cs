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

        [Browsable(false)]
        public String InstallPath
        {
            get
            {
                String zonePath = "";
                if (this.Realm == null || this.Realm == "No Realm Associated.")
                {
                    zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
                    zonePath = Path.Combine(zonePath, this.Zone);
                }
                else
                    zonePath = FileManager.GetDataPath(this.Realm, this.Zone);

                String roomPath = Path.Combine(zonePath, "Rooms");
                String filename = Path.Combine(roomPath, this.Filename);
                return filename;
            }
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

        public Room(Game game) :base(game)
        {
            Doorways = new List<Door>();

            IsSafe = false;
        }

        public override void Save(String path)
        {
            base.Save(path);

            String filename = Path.Combine(path, Filename);

            FileManager.WriteLine(filename, IsInitialRoom.ToString(), "IsInitialRoom");
            FileManager.WriteLine(filename, this.IsSafe.ToString(), "IsSafe");
            FileManager.WriteLine(filename, this.Realm, "Realm");
            FileManager.WriteLine(filename, this.Zone, "Zone");
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

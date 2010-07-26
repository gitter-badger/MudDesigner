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

namespace MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(Door))]
    public class Room : BaseObject
    {
        [Category("Environment Information")]
        [Description("Shows what rooms are currently created and linked to within this Room.")]
        [ReadOnly(true)]
        public string InstalledDoorways
        {
            get
            {
                string installed = "";
                if (this.Doorways.Count != 0)
                {
                    foreach (Door d in Doorways)
                    {
                        installed += d.TravelDirection.ToString() + ",";
                    }
                    if (Doorways.Count >= 2)
                    {
                        installed = installed.Substring(0, installed.Length - 1);
                    }
                    return installed;
                }
                else
                    return "None Installed.";
            }
        }

        [Category("Environment Information")]
        [Description("Allows for linking of Rooms together via Doorways")]
        //[EditorAttribute(typeof(UIDoorwayEditor), typeof(UITypeEditor))]
        [RefreshProperties(RefreshProperties.All)]
        public List<Door> Doorways
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Description("This is the Zone that the Room is currently assigned to.")]
        [Category("Environment Information")]
        public string Zone
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Description("This is the Realm that the Room belongs to.")]
        [Category("Environment Information")]
        public string Realm
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [DefaultValue(false)]
        [Description("Determins if the Player can be attacked within this Room or not.")]
        public bool IsSafe
        {
            get;
            set;
        }

        [Browsable(false)]
        public string InstallPath
        {
            get
            {
                string zonePath = "";
                if (this.Realm == null || this.Realm == "No Realm Associated.")
                {
                    zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
                    zonePath = Path.Combine(zonePath, this.Zone);
                }
                else
                    zonePath = FileManager.GetDataPath(this.Realm, this.Zone);

                string roomPath = Path.Combine(zonePath, "Rooms");
                string filename = Path.Combine(roomPath, this.Filename);
                return filename;
            }
        }

        /// <summary>
        /// Gets or Sets if this is the starting room for the Zone that contains it.
        /// </summary>
        [Browsable(true)]
        [Description("Sets if this is the starting room for the Zone that contains it.")]
        public bool IsInitialRoom
        {
            get;
            set;
        }

        public Room()
        {
            Doorways = new List<Door>();

            IsSafe = false;
        }

        public bool DoorwayExist(AvailableTravelDirections travelDirection)
        {
            foreach (Door door in Doorways)
            {
                if (door.TravelDirection == travelDirection)
                    return true;
            }

            return false;
        }

        public Door GetDoor(AvailableTravelDirections travelDirection)
        {
            foreach (Door door in this.Doorways)
            {
                if (door.TravelDirection == travelDirection)
                    return door;
            }
            return null;
        }

        /// <summary>
        /// Load a Room that exists within the same Zone as the current Room
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public override object Load(string roomName)
        {
            //Correct the filename incase it doesnt contain a file extension
            if (!roomName.ToLower().EndsWith(this.GetType().Name.ToLower()))
                roomName.Insert(roomName.Length, this.GetType().Name.ToLower());

            //If the current room does not belong within a Realm, then load it from the
            //Zones root directory
            if (this.Realm != null || this.Realm != "No Realm Associated.")
            {
                return this.Load(roomName, this.Zone);
            }
            //This Zone is contained within a Realm so we have to load it from within the
            //Realm and not from within the Zones root directory
            else
                return this.Load(roomName, this.Zone, this.Realm);
        }

        public object Load(string roomName, string zoneName)
        {
            string filename = "";
            if (!roomName.ToLower().EndsWith(".room"))
                roomName += ".room";

            if (this.Realm != null && this.Realm != "No Realm Associated.")
            {
                return this.Load(roomName, zoneName, this.Realm);
            }
            else
                filename = FileManager.GetDataPath(SaveDataTypes.Zones);

            filename = Path.Combine(filename, zoneName);
            filename = Path.Combine(filename, "Rooms");
            filename = Path.Combine(filename, roomName);

            return base.Load(filename);
        }

        public object Load(string roomName, string zoneName, string realmName)
        {
            if (!roomName.ToLower().EndsWith(".room"))
                roomName += ".room";

            string filename = FileManager.GetDataPath(realmName, zoneName);
            filename = Path.Combine(filename, "Rooms");
            filename = Path.Combine(filename, roomName);

            if (realmName == null || realmName == "No Realm Associated.")
                return this.Load(roomName, zoneName);

            return base.Load(filename);
        }
    }
}

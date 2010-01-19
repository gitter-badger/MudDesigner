using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    public class Room : BaseObject
    {
        [Category("Environment Information")]
        [Description("Shows what rooms are currently created and linked to within this Room.")]
        [ReadOnly(true)]
        public string DoorList
        {
            get
            {
                string installed = "";
                if (this.InstalledDoors.Count != 0)
                {
                    foreach (Door d in InstalledDoors)
                    {
                        installed += d.TravelDirection.ToString() + ",";
                    }
                    if (InstalledDoors.Count >= 2)
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
        public List<Door> InstalledDoors;

        [ReadOnly(true)]
        [Description("This is the Zone that the Room is currently assigned to.")]
        [Category("Environment Information")]
        public string Zone
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

        public Room TestRoom { get; set; }
        public Room()
        {
            InstalledDoors = new List<Door>();
            IsSafe = false;
        }
    }
}

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    public class Room : BaseObject
    {
        [Category("Room Information")]
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

        [Browsable(false)]
        public List<Door> InstalledDoors;

        [Browsable(false)]
        public string Zone
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

        public Room TestRoom { get; set; }
        public Room()
        {
            InstalledDoors = new List<Door>();
        }
    }
}

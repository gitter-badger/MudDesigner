using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing.Design;

using MudDesigner.MudEngine.UITypeEditors;

namespace MudDesigner.MudEngine.GameObjects.Environment
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
        [EditorAttribute(typeof(UIDoorwayEditor), typeof(UITypeEditor))]
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

        public Room()
        {
            Doorways = new List<Door>();
            
            IsSafe = false;
        }

        public bool DoorwayExist(string travelDirection)
        {
            foreach (Door door in Doorways)
            {
                if (door.TravelDirection.ToString().ToLower() == travelDirection.ToLower())
                    return true;
            }

            return false;
        }
    }
}

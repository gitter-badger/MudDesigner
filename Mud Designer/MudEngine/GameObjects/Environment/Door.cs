using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

using MudDesigner.MudEngine.UITypeEditors;
using MudDesigner.MudEngine.GameObjects.Items;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(BaseItem))]
    [Serializable]
    public class Door
    {
        public struct DoorwayLinkInformation
        {
            public string Realm;
            public string Zone;
            public string Room;

            public override string ToString()
            {
                if (string.IsNullOrEmpty(Room))
                    return "Doorway to no where.";
                else
                    return "Doorway to " + Room;
            }
        }

        [Category("Door Settings")]
        [DefaultValue(false)]
        public bool IsLocked
        {
            get;
            set;
        }

        [Category("Door Settings")]
        [Browsable(false)]
        public BaseItem RequiredKey
        {
            get;
            set;
        }

        [Category("Door Settings")]
        [DefaultValue(0)]
        public int LevelRequirement
        {
            get;
            set;
        }

        [Category("Door Settings")]
        [EditorAttribute(typeof(UIDoorwayEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public DoorwayLinkInformation DoorwayLink
        {
            get;
            set;
        }

        [Browsable(false)]
        public AvailableTravelDirections TravelDirection;

        public Door()
        {
            LevelRequirement = 0;
            IsLocked = false;
            RequiredKey = new BaseItem();
            DoorwayLink = new DoorwayLinkInformation();
        }

        public Door(DoorwayLinkInformation linkInformation) : this()
        {
            this.DoorwayLink = linkInformation;
        }

        public override string ToString()
        {
            return this.TravelDirection.ToString() + " Doorway";
        }
    }
}

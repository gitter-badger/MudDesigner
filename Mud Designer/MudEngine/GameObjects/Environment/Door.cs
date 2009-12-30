using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(ConnectedRoom))]
    public class Door
    {
        public struct ConnectedRoom
        {
            public string Realm;
            public string Zone;
            public string Room;
            public AvailableTravelDirections TravelDirection;
        }

        [Category("Door Settings")]
        [DefaultValue(false)]
        public bool IsLocked
        {
            get;
            set;
        }

        [Category("Door Settings")]
        public string RequiredKey
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

        [Browsable(false)]
        public AvailableTravelDirections TravelDirection
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("Door Settings")]
        public ConnectedRoom TravelRoom
        {
            get;
            set;
        }

        public Door(AvailableTravelDirections TravelDirection)
        {
            this.TravelDirection = TravelDirection;
        }

        public Door()
        {
        }
    }
}

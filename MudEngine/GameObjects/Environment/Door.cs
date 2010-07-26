//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

//MUD Engine
using MudEngine.GameObjects.Items;

namespace MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(BaseItem))]
    [Serializable]
    public class Door
    {
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
        public AvailableTravelDirections TravelDirection { get; set; }

        /// <summary>
        /// Gets or Sets the Room that the player will be arriving.
        /// </summary>
        public Room ArrivalRoom { get; set; }

        /// <summary>
        /// Gets or Sets the Room that the user is leaving.
        /// </summary>
        public Room DepartureRoom { get; set; }

        public Door()
        {
            LevelRequirement = 0;
            IsLocked = false;
            RequiredKey = new BaseItem();
        }
    }
}

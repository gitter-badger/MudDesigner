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

        public string ConnectedRoom { get; set; }

        [Category("Environment Information")]
        [Description("The description displayed to the user when a 'Look' command is used.")]
        public string Description { get; set; }

        public Door()
        {
            LevelRequirement = 0;
            IsLocked = false;
            RequiredKey = new BaseItem();
        }

        public Door(AvailableTravelDirections travelDirection, string connectedRoom)
            : this()
        {
            ConnectedRoom = connectedRoom;
            TravelDirection = travelDirection;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    public class Door
    {
        public enum AvailableDoorStates
        {
            Uninstalled,
            Installed,
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

        [Category("Door Settings")]
        [DefaultValue(false)]
        public bool IsRealmEntrance
        { get; set; }

        [Category("Door Settings")]
        [DefaultValue(false)]
        public bool IsRealmExit { get; set; }

        [Category("Door Settings")]
        [Description("Sets if the door is installed and useable within the room or not.")]
        [DefaultValue(AvailableDoorStates.Uninstalled)]
        public AvailableDoorStates DoorState
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

        public Door(AvailableTravelDirections TravelDirection)
        {
            this.TravelDirection = TravelDirection;
        }

        public Door()
        {
        }
    }
}

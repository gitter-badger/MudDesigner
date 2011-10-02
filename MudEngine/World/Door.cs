using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using MudEngine.Core;

namespace MudEngine.World
{
    public class Door
    {
        /// <summary>
        /// Gets or Sets if this doorway is locked, requiring a key to enter.
        /// </summary>
        [Category("Door Settings")]
        [DefaultValue(false)]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets or Sets what item is required to travel through this doorway if IsLocked
        /// </summary>
        [Category("Door Settings")]
        [Browsable(false)]
        public BaseItem RequiredKey { get; set; }

        /// <summary>
        /// Gets or Sets the minimum level a character must be in order to travel through this doorway
        /// </summary>
        [Category("Door Settings")]
        [DefaultValue(0)]
        public int LevelRequirement { get; set; }

        /// <summary>
        /// Gets or Sets the direction a character must travel in order to move through this doorway
        /// </summary>
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
    }

}

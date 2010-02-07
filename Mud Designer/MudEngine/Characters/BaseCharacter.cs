using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudDesigner.MudEngine.Interfaces;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameCommands;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameObjects.Items;

namespace MudDesigner.MudEngine.Characters
{
    public class BaseCharacter : BaseObject
    {
        public Room CurrentRoom { get; set; }

        public virtual void OnTravel(AvailableTravelDirections travelDirection)
        {
            if (CurrentRoom.DoorwayExist(travelDirection.ToString()))
            {
                string connectedRoom = CurrentRoom.GetDoor(travelDirection).ConnectedRoom;
                CurrentRoom = (Room)CurrentRoom.Load(connectedRoom);
            }
        }
    }
}

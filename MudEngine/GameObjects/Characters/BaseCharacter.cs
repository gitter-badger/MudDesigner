//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.Commands;
using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects.Items;

namespace MudEngine.GameObjects.Characters
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

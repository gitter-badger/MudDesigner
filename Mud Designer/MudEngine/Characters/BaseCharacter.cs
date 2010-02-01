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
                string fileName = "";
                if (CurrentRoom.Realm == "No Realm Associated.")
                {
                    fileName = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), CurrentRoom.Zone);
                    fileName = Path.Combine(fileName, "Rooms");
                }
                else 
                {
                    fileName = Path.Combine(FileManager.GetDataPath(CurrentRoom.Realm, CurrentRoom.Zone), "Rooms");
                }
                string connectedRoom = CurrentRoom.GetDoor(travelDirection).ConnectedRoom;
                fileName = Path.Combine(fileName, connectedRoom);
                fileName += ".room";
                CurrentRoom = (Room)CurrentRoom.Load(fileName);
            }
        }
    }
}

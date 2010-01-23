using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameCommands;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameObjects.Items;
using MudDesigner.MudEngine.Interfaces;

namespace MudDesigner.MudEngine.GameCommands
{
    public class CommandLook : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public object Execute(params object[] parameters)
        {
            Room room = new Room() ;
            StringBuilder desc = new StringBuilder();

            foreach (object obj in parameters)
            {
                if (obj is Room)
                    room = (Room)obj;
            }

            if (room == null)
                return null;

            foreach (Door door in room.Doorways)
            {
                if (door.TravelDirection != MudDesigner.MudEngine.GameObjects.AvailableTravelDirections.Down && door.TravelDirection != MudDesigner.MudEngine.GameObjects.AvailableTravelDirections.Up)
                {
                    desc.AppendLine(door.Description);
                }
            }

            return desc.ToString();
        }
    }
}

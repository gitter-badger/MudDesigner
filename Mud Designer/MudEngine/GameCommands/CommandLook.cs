using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.MudEngine.Characters;
using MudDesigner.MudEngine.Characters.Controlled;
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

        public CommandResults Execute(BaseCharacter player, ProjectInformation project, Room room, string command)
        {
            StringBuilder desc = new StringBuilder();

            if (room == null)
            {
                return new CommandResults("Not within a created Room.");
            }

            desc.AppendLine(room.Description);
            foreach (Door door in room.Doorways)
            {
                if (door.TravelDirection != MudDesigner.MudEngine.GameObjects.AvailableTravelDirections.Down && door.TravelDirection != MudDesigner.MudEngine.GameObjects.AvailableTravelDirections.Up)
                {
                    desc.AppendLine(door.Description);
                }
            }

            return new CommandResults(desc.ToString());
        }
    }
}

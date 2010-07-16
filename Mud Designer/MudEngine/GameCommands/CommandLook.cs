using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Characters.Controlled;
using MudEngine.FileSystem;
using MudEngine.Commands;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects.Items;
using MudDesigner.Engine.Interfaces;

namespace MudEngine.Commands
{
    public class CommandLook : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandResults Execute(BaseCharacter player, GameSetup project, Room room, string command)
        {
            StringBuilder desc = new StringBuilder();

            if (room == null)
            {
                return new CommandResults("Not within a created Room.");
            }

            desc.AppendLine(room.Description);
            foreach (Door door in room.Doorways)
            {
                if (door.TravelDirection != MudEngine.GameObjects.AvailableTravelDirections.Down && door.TravelDirection != MudEngine.GameObjects.AvailableTravelDirections.Up)
                {
                    desc.AppendLine(door.Description);
                }
            }

            return new CommandResults(desc.ToString());
        }
    }
}

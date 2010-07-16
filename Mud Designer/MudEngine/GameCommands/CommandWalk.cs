using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudDesigner.Engine.Interfaces;
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects;
using MudEngine.FileSystem;

namespace MudEngine.Commands
{
    public class CommandWalk : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandResults Execute(BaseCharacter player, GameSetup project, Room room, string command)
        {
            string[] words = command.Split(' ');
            List<string> directions = new List<string>();

            if (words.Length == 1)
                return new CommandResults("No direction supplied");
            else
            {
                foreach (Door door in room.Doorways)
                {
                    AvailableTravelDirections direction = TravelDirections.GetTravelDirectionValue(words[1]);

                    if (door.TravelDirection == direction)
                    {
                        room = (Room)room.Load(door.ConnectedRoom);

                        CommandResults cmd = CommandEngine.ExecuteCommand("Look", player, project, room, "Look");
                        string lookValue = "";

                        if (cmd.Result.Length != 0)
                            lookValue = cmd.Result[0].ToString();

                        return new CommandResults(new object[] { lookValue, room });
                    }
                }
            }
            return new CommandResults("Unable to travel in that direction.");
        }
    }
}

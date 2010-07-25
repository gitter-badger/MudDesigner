﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

        public CommandResults Execute(string command, BaseCharacter player)
        {
            string[] words = command.Split(' ');
            List<string> directions = new List<string>();

            if (words.Length == 1)
                return new CommandResults("No direction supplied");
            else
            {
                foreach (Door door in player.CurrentRoom.Doorways)
                {
                    AvailableTravelDirections direction = TravelDirections.GetTravelDirectionValue(words[1]);

                    if (door.TravelDirection == direction)
                    {
                        //TODO: Player.Move() method needed so room loading is handled by player code.
                        player.CurrentRoom = (Room)player.CurrentRoom.Load(door.ConnectedRoom);

                        CommandResults cmd = CommandEngine.ExecuteCommand("Look", player);
                        string lookValue = "";

                        if (cmd.Result.Length != 0)
                            lookValue = cmd.Result[0].ToString();

                        return new CommandResults(new object[] { lookValue, player.CurrentRoom });
                    }
                }
            }
            return new CommandResults("Unable to travel in that direction.");
        }
    }
}

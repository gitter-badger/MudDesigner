using System;
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
        public String Name { get; set; }
        public Boolean Override { get; set; }
        public List<String> Help { get; set; }

        public void Execute(String command, BaseCharacter player)
        {
            String[] words = command.Split(' ');
            List<String> directions = new List<String>();

            if (words.Length == 1)
            {
                player.Send("No direction provided!");
                return;
            }
            else
            {
                foreach (Door door in player.CurrentRoom.Doorways)
                {
                    if (door.TravelDirection == TravelDirections.GetTravelDirectionValue(words[1]))
                    {
                        //Move the player into their new room
                        player.Move(door.TravelDirection);

                        player.CommandSystem.ExecuteCommand("Look", player);

                        if (player.ActiveGame.AutoSave)
                            player.Save(player.ActiveGame.DataPaths.Players);

                        return;
                    }
                }
            }
            player.Send("Unable to travel in that direction.");
        }
    }
}

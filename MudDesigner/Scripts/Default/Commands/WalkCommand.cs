using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Scripting;

using MudDesigner.Scripts.Default.States;

namespace MudDesigner.Scripts.Default.Commands
{
    public class WalkCommand : ICommand
    {
        public void Execute(IPlayer player)
        {
            if (String.IsNullOrEmpty(player.ReceivedInput))
                return;

            string[] args = player.ReceivedInput.Split(' ');
            string direction = String.Empty;

            if (args.Length >= 1)
                direction = args[1]; //Assume Walk North, so [1] = North (or any other direction)
            else
            {
                player.SendMessage("Please specify which direction you would like to walk.");
                return;
            }

            AvailableTravelDirections travelDirection = TravelDirections.GetTravelDirectionValue(direction);

            if (travelDirection == AvailableTravelDirections.None)
            {
                player.SendMessage("Invalid direction!");
                return;
            }

            if (player.Location.DoorwayExists(travelDirection))
            {
                IDoor door = player.Location.GetDoorway(travelDirection);
                player.Move(door.Arrival);


                //Make sure we have a valid save path
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "saves", EngineSettings.Default.PlayerSavePath, player.Username + ".char");
                var path = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the player using our serialization class
                FileIO fileSave = new FileIO();
                fileSave.Save(player, filePath);
            }

            player.SwitchState(new LookingState());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudEngine.FileSystem;
using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public class CommandLinkRoom : IGameCommand
    {
        public Boolean Override { get; set; }
        public String Name { get; set; }
        public List<String> Help { get; set; }

        public CommandLinkRoom()
        {
            Help = new List<string>();
            Help.Add("Use this to link two previously created Rooms together");
            Help.Add("Note that this command is not fully implemented and does not work.");
        }

        public void Execute(String command, BaseCharacter player)
        {
            player.Send("Room linkage tool");
            player.Send("Please select an option.");
            player.Send("1: Link Room");
            player.Send("2: Exit");

            string input = player.ReadInput();
            Int32 value = 0;

            try
            {
                value = Convert.ToInt32(input);
            }
            catch (Exception)
            {
                player.Send("Invalid selection. Please use numeric values.");
                return;
            }

            player.Send("");
            player.Send("Please select which Realm your departing Room resides within:");
            player.Send("");
            
            Boolean isValidRealm = false;
            Realm realm = new Realm(player.ActiveGame);

            while (!isValidRealm)
            {
                isValidRealm = true;//Default to true, assume the user entered a valid name.
                foreach (Realm r in player.ActiveGame.World.RealmCollection)
                {
                    player.Send(r.Filename + " | ", false);
                }

                player.Send("");
                player.Send("Selection: ", false);

                input = player.ReadInput();

                if (input.ToLower() == "cancel")
                {
                    player.Send("Room Linking aborted.");
                    return;
                }

                //Ensure it's a valid name, if not then loop back and try again.
                foreach (Realm r in player.ActiveGame.World.RealmCollection)
                {
                    if (r.Filename.ToLower() == input.ToLower())
                    {
                        isValidRealm = true;
                        realm = r;
                        break;
                    }
                    else
                    {
                        isValidRealm = false;
                    }
                }

                if (!isValidRealm)
                    player.Send("That Realm does not exist! Please try again.");
            }

            player.Send("");
            player.Send("Please select which Zone your departing Room resides within:");
            player.Send("");

            Boolean isValidZone = false;
            Zone zone = new Zone(player.ActiveGame);

            while (!isValidZone)
            {
                isValidZone = true;//Default to true, assume the user entered a valid name.
                foreach (Zone z in realm.ZoneCollection)
                {
                    player.Send(z.Filename + " | ", false);
                }

                player.Send("");
                player.Send("Selection: ", false);

                input = player.ReadInput();

                if (input.ToLower() == "cancel")
                {
                    player.Send("Room Linking aborted.");
                    return;
                }

                //Ensure it's a valid name, if not then loop back and try again.
                foreach (Zone z in realm.ZoneCollection)
                {
                    if (z.Filename.ToLower() == input.ToLower())
                    {
                        isValidZone = true;
                        zone = z;
                        break;
                    }
                    else
                    {
                        isValidZone = false;
                    }
                }

                if (!isValidZone)
                    player.Send("That Zone does not exist! Please try again.");
            }

            player.Send("");
            player.Send("Please select which Room that you wish to be the departing Room:");
            player.Send("");

            Boolean isValidRoom = false;
            Room departingRoom = new Room(player.ActiveGame);

            while (!isValidRoom)
            {
                isValidRoom = true;//Default to true, assume the user entered a valid name.
                foreach (Room r in zone.RoomCollection)
                {
                    player.Send(r.Filename + " | ", false);
                }

                player.Send("");
                player.Send("Selection: ", false);

                input = player.ReadInput();

                if (input.ToLower() == "cancel")
                {
                    player.Send("Room Linking aborted.");
                    return;
                }

                //Ensure it's a valid name, if not then loop back and try again.
                foreach (Room r in zone.RoomCollection)
                {
                    if (r.Filename.ToLower() == input.ToLower())
                    {
                        isValidRoom = true;
                        departingRoom = r;
                        break;
                    }
                    else
                    {
                        isValidRoom = false;
                    }
                }

                if (!isValidRoom)
                    player.Send("That Room does not exist! Please try again.");
            }

            player.Send("");
            player.Send("Please select which Room that you wish to be the Arrival Room:");
            player.Send("");

            isValidRoom = false;
            Room arrivalRoom = new Room(player.ActiveGame);

            while (!isValidRoom)
            {
                isValidRoom = true;//Default to true, assume the user entered a valid name.
                foreach (Room r in zone.RoomCollection)
                {
                    player.Send(r.Filename + " | ", false);
                }

                player.Send("");
                player.Send("Selection: ", false);

                input = player.ReadInput();

                if (input.ToLower() == "cancel")
                {
                    player.Send("Room Linking aborted.");
                    return;
                }

                //Ensure it's a valid name, if not then loop back and try again.
                foreach (Room r in zone.RoomCollection)
                {
                    if (r.Filename.ToLower() == input.ToLower())
                    {
                        isValidRoom = true;
                        departingRoom = r;
                        break;
                    }
                    else
                    {
                        isValidRoom = false;
                    }
                }

                if (!isValidRoom)
                    player.Send("That Room does not exist! Please try again.");
            }

            player.Send("");
            player.Send("Please select which Room that you wish to be the departing Room:");
            player.Send("");

            player.Send("Please select which direction you would like to travel while departing the departure Room.");
            Array values = Enum.GetValues(typeof(AvailableTravelDirections));
            foreach (Int32 v in values)
            {
                //Since enum values are not strings, we can't simply just assign the String to the enum
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), v);
                player.Send(displayName + " | ");
            }

            player.Send("Enter Selection: ", false);
            input = player.ReadInput();

            AvailableTravelDirections direction = new AvailableTravelDirections();
            Boolean isValidDirection = false;

            foreach (Int32 v in values)
            {
                //Since enum values are not strings, we can't simply just assign the String to the enum
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), v);

                //If the value = the String saved, then perform the needed conversion to get our data back
                if (displayName.ToLower() == input.ToLower())
                {
                    direction = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
                    isValidDirection = true;
                    break;
                }
            }

            if (!isValidDirection)
            {
                player.Send("Invalid direction supplied!");
            }
            else
            {
                zone.LinkRooms(direction, arrivalRoom, departingRoom);
            }
        }
    }
}

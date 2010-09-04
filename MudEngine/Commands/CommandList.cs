using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using MudEngine.FileSystem;
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.Commands;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    /// <summary>
    /// The List command is used to list filenames of a specified object type.
    /// </summary>
    public class CommandList : IGameCommand
    {
        /// <summary>
        /// Used by the Command Engine to allow for overriding any other commands that contain the same name.
        /// TODO: Does Overriding Commands still work? This is part of some old code I wrote several years back and might be broke.
        /// </summary>
        public Boolean Override { get; set; }

        /// <summary>
        /// The name of the command.
        /// If Override is set to true, this command will override any other command that contains the same name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A collection of strings that contains helpfull information for this Command.
        /// When the user enteres 'Help Exit' the game will print the content of this collection.
        /// This is treated like a virtual book, each entry in the collection is printed as a new line.
        /// </summary>
        public List<String> Help { get; set; }

        public CommandList()
        {
            Help = new List<string>();
            Help.Add("Using the List command, you can view a generated list of filenames that pertain to a supplied object type.");
            Help.Add("Usage: List 'ItemType'");
            Help.Add("Usage: List 'ItemName>ItemType'");
            Help.Add("");
            Help.Add("Supported Listable ItemTypes are as follows:");
            Help.Add("Players");
            Help.Add("Realms");
            Help.Add("Zones");
            Help.Add("RealmName>Rooms");
            Help.Add("RealmName>Zones");
            Help.Add("RealmName>ZoneName>Rooms");
        }

        public void Execute(String command, BaseCharacter player)
        {
            if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
            {
                //Player must be a admin or GM to view all the objects on the server like this.

                command = command.Substring("List".Length).Trim();
                String[] data = command.ToLower().Split('>');

                if ((data.Length == 0) || (String.IsNullOrEmpty(data[0])))
                {
                    player.Send("Invalid command usage. Enter 'help List' for usage examples.");
                    return;
                }
                else if (data.Length == 1)
                {
                    switch (data[0])
                    {
                        case "realms":
                            player.Send("Currently loaded Realm files:");
                            foreach (Realm r in player.ActiveGame.World.RealmCollection)
                                player.Send(r.Filename + " | ", false);
                            break;
                        case "players":
                            player.Send("Players with created characters:");
                            BaseCharacter p = new BaseCharacter(player.ActiveGame);
                            foreach (String file in Directory.GetFiles(player.ActiveGame.DataPaths.Players, "*.character"))
                            {
                                p.Load(file);
                                player.Send(p.Name + " | ", false);
                            }
                            break;
                        case "zones":
                            player.Send("Currently loaded Zones. This spans across every Realm in the world.");
                            foreach (Realm r in player.ActiveGame.World.RealmCollection)
                            {
                                foreach (Zone z in r.ZoneCollection)
                                {
                                    player.Send(Path.GetFileNameWithoutExtension(r.Filename) + ">" + Path.GetFileNameWithoutExtension(z.Filename));
                                }
                            }
                            break;
                        default:
                            player.Send("Invalid token supplied. Enter 'Help List' for usage examples.");
                            break;
                    }
                }
                else if (data.Length == 2)
                {
                    if (data[1] == "zones")
                    {
                        if (player.ActiveGame.World.GetRealm(data[0] + ".realm") == null)
                        {
                            player.Send("Invalid Realm, unable to list Zones.");
                            return;
                        }
                        player.Send("Displaying Currently loaded Zones within Realm " + data[0]);
                        foreach (Zone z in player.ActiveGame.World.GetRealm(data[0] + ".realm").ZoneCollection)
                            player.Send(z.Filename + " | ", false);
                    }
                    else if (data[1] == "rooms")
                    {
                        if (player.ActiveGame.World.GetRealm(data[0] + ".realm") == null)
                        {
                            player.Send("Invalid Realm, unable to list Rooms.");
                            return;
                        }

                        player.Send("Displaying currently loaded Rooms within Realm " + data[0] + ". These Rooms span multiple Zones.");
                        foreach (Zone z in player.ActiveGame.World.GetRealm(data[0] + ".realm").ZoneCollection)
                        {
                            foreach (Room r in z.RoomCollection)
                            {
                                player.Send(Path.GetFileNameWithoutExtension(z.Filename) + ">" + Path.GetFileNameWithoutExtension(r.Filename));
                            }
                        }
                    }
                }
                else if (data.Length == 3)
                {
                    if (data[2] == "rooms")
                    {
                        if (player.ActiveGame.World.GetRealm(data[0] + ".realm") == null)
                        {
                            player.Send("Invalid Realm, unable to list Rooms.");
                            return;
                        }

                        if (player.ActiveGame.World.GetRealm(data[0] + ".realm").GetZone(data[1] + ".zone")[0] == null)
                        {
                            player.Send("Invalid Zone, unable to list Rooms.");
                            return;
                        }

                        player.Send("Displaying Currently loaded Rooms within " + data[0] + ">" + data[1]);
                        foreach (Room r in player.ActiveGame.World.GetRealm(data[0] + ".realm").GetZone(data[1] + ".zone")[0].RoomCollection)
                            player.Send(r.Filename + " | ", false);
                    }
                }
            }
        }
    }
}

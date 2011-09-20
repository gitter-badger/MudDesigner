/// <summary>
/// The List command is used to list filenames of a specified object type.
/// </summary>
public class CommandList : BaseCommand
{
    public CommandList()
    {
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

    public override void Execute(String command, BaseCharacter player)
    {
        command = command.Substring("List".Length).Trim();
        String[] data = command.ToLower().Split('>');

        //Admin || GM only item listings.
        if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
        {
            //Player must be a admin or GM to view all the objects on the server like this.
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
                        if (player.ActiveGame.World.RealmCollection.Count == 0)
                            player.Send("There are currently no loaded Realm files.");
                        else
                        {
                            player.Send("Currently loaded Realm files:");
                            foreach (Realm r in player.ActiveGame.World.RealmCollection)
                                player.Send(r.Filename + " | ", false);
                        }
                        break;
                    case "players":
                        if (System.IO.Directory.GetFiles(player.ActiveGame.DataPaths.Players, "*.character").Length == 0)
                            player.Send("There are currently no characters created on this server.");
                        else
                        {
                            player.Send("Players with created characters:");
                            BaseCharacter p = new BaseCharacter(player.ActiveGame);
                            foreach (String file in System.IO.Directory.GetFiles(player.ActiveGame.DataPaths.Players, "*.character"))
                            {
                                p.Load(file);
                                player.Send(p.Name + " | ", false);
                            }
                        }
                        break;
                    case "zones":
                        if (player.ActiveGame.World.RealmCollection.Count == 0)
                        {
                            player.Send("There are currently no Zones created on this server.");
                        }
                        else
                        {
                            List<String> names = new List<String>();
                            foreach (Realm r in player.ActiveGame.World.RealmCollection)
                            {
                                foreach (Zone z in r.ZoneCollection)
                                {
                                    names.Add(System.IO.Path.GetFileNameWithoutExtension(r.Filename) + ">" + System.IO.Path.GetFileNameWithoutExtension(z.Filename));
                                }
                            }

                            if (names.Count == 0)
                            {
                                player.Send("There are currently no Zones created on this server.");
                            }
                            else
                            {
                                player.Send("Currently loaded Zones. This spans across every Realm in the world.");
                                foreach (String name in names)
                                {
                                    player.Send(name);
                                }
                            }
                        }
                        break;
                    case "commands":
                        player.Send("The following commands are available for use:");

                        foreach (String cmd in CommandEngine.GetCommands())
                        {
                            IGameCommand gc = CommandEngine.GetCommand(cmd);
                            player.Send(gc.Name);
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
                            player.Send(System.IO.Path.GetFileNameWithoutExtension(z.Filename) + ">" + System.IO.Path.GetFileNameWithoutExtension(r.Filename));
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
        } //End Admin || GM only item listings.
        //Begin normal player item listings
        else
        {
            //Player must be a admin or GM to view all the objects on the server like this.
            if ((data.Length == 0) || (String.IsNullOrEmpty(data[0])))
            {
                player.Send("Invalid command usage. Enter 'help List' for usage examples.");
                return;
            }
            else if (data.Length == 1)
            {
                if (data[0] == "commands")
                {
                    player.Send("The following commands are available for use:");

                    foreach (String cmd in CommandEngine.GetCommands())
                    {
                        IGameCommand gc = CommandEngine.GetCommand(cmd);
                        if ((gc.Name == "CommandCreate") || (gc.Name == "CommandCreateRoom") || (gc.Name == "CommandLinkRoom") || (gc.Name == "CommandRestart") || (gc.Name == "CommandSaveWorld"))
                            continue;

                        player.Send(gc.Name);
                    }
                }
            }
        }
    }
}
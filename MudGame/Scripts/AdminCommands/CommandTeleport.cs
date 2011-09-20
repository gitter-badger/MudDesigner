/// <summary>
/// The List command is used to list filenames of a specified object type.
/// </summary>
public class CommandTeleport : BaseCommand
{
    public CommandTeleport()
    {
        Help.Add("The Teleport command will teleport a player to a specified Room, regardless of where they are at.");
        Help.Add("Usage: Teleport playername FullyQualifiedRoomPath");
        Help.Add("Example: Teleport Billy MyRealm>MyZone>MyRoom");
    }

    public override void Execute(String command, BaseCharacter player)
    {
        command = command.Substring("teleport".Length).Trim();
        String[] data = command.ToLower().Split(' ');

        //Admin || GM only item listings.
        if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
        {
            if (data.Length == 0)
            {
                player.Send("Invalid operation. You must supply a username along with a fully qualified path name for the user to be teleported to.");
                return;
            }
            else if (data.Length == 1)
            {
                player.Send("Invalid operation. You must supply a fully qualified path name for the user to be teleported.");
                return;
            }
            else
            {
                foreach (BaseCharacter p in player.ActiveGame.GetPlayerCollection())
                {
                    if (p.Name.ToLower() == data[0].ToLower())
                    {
                        String[] values = data[1].Split('>');

                        if (values.Length != 3)
                        {
                            player.Send("Invalid Operation. You must supply a fully qualified path name for the user to be teleported.");
                            return;
                        }
                        else
                        {
                            Realm r = player.ActiveGame.World.GetRealm(values[0]);
                            if (r == null)
                            {
                                player.Send("Invalid Operation. Supplied Realm does not exist.");
                                return;
                            }

                            Zone z = r.GetZone(values[1])[0];
                            if (z == null)
                            {
                                player.Send("Invalid operation. Supplied Zone does not exist.");
                                return;
                            }

                            Room rm = z.GetRoom(values[2])[0];
                            if (rm == null)
                            {
                                player.Send("Invalid operation. Supplied Room does not exist.");
                                return;
                            }

                            p.CurrentRoom = rm;

                            //Tell the teleported player that they have been moved.
                            p.Send("You have been teleported by some higher power.");
                            IGameCommand gc = CommandEngine.GetCommand("CommandLook");
                            gc.Execute("Look", p);

                            player.Send("Teleporting completed.");
                        }
                    }
                }
            }
        }
    }
}
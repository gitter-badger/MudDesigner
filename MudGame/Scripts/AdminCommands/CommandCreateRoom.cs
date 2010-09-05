/// <summary>
/// This command creates a Room within the players current Realm>Zone.
/// Admins using this command will not need to supply a fully qualified path like the 'Create' command requires.
/// However, they are restricted to creating Rooms only within their current Realm>Zone.
/// </summary>
class CommandCreateRoom : IGameCommand
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

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public CommandCreateRoom()
    {
        Help = new List<string>();
        Help.Add("Creates a Room within the Admin's current Realm>Zone");
    }

    public void Execute(String command, BaseCharacter player)
    {
        if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
        {
            String roomname = command.Substring("Createroom".Length).Trim();

            if (String.IsNullOrEmpty(roomname))
            {
                player.Send("You must supply a Room name! Refer to 'Help CreateRoom' for usage examples.");
                return;
            }


            if ((String.IsNullOrEmpty(player.CurrentRoom.Realm)) || (String.IsNullOrEmpty(player.CurrentRoom.Zone)))
            {
                player.Send("You are not currently within a pre-existing Zone.");
                player.Send("Use the Teleport command to teleport yourself into a valid Zone before using the CreateRoom command.");
                return;
            }

            Room r = new Room(player.ActiveGame);
            r.Realm = player.CurrentRoom.Realm;
            r.Zone = player.CurrentRoom.Zone;
            r.Name = roomname;
            player.ActiveGame.World.GetRealm(r.Realm).GetZone(r.Zone)[0].AddRoom(r);

            player.Send(r.Name + " created within " + r.Realm + ">" + r.Zone + ".");
            Log.Write(player.Name + " created a new Room in " + r.RoomLocation);
        }
    }
}
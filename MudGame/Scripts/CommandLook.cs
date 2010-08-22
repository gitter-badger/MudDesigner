/// <summary>
/// The Look command is used to print the contents of the players current Room to the screen.
/// The Room.Description property is printed to the players screen if it contains content.
/// If the Room.DetailedDescription collection property contains content, it will be printed to the screen
/// after the Room.Description property is printed (provided Room.Description is not empty.)
/// </summary>
public class CommandLook : IGameCommand
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
    public CommandLook()
    {
        Help = new List<String>();
        Help.Add("Prints a description of the current Room and the objects that reside within it.");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //If the players Room is null, then we need to let them know that they are
        //currently not residing within a Room. If this occures for some reason, the player will
        //need to be moved into an existing Room by an Admin.
        //TODO: Allow Admins to move Characters from one Room to another.
        if (player.CurrentRoom == null)
        {
            player.Send("You are not within any Room.");
            return;
        }

        //Check if the players current Room has a blank Description. If not, we print it for the player to read.
        if (!String.IsNullOrEmpty(player.CurrentRoom.Description))
            player.Send(player.CurrentRoom.Description);

        //Check if the players current Room has a detailed description.
        //If the collection contains content, it will loop through each entry and print it to the screen as a new line.
        if (player.CurrentRoom.DetailedDescription.Count != 0)
        {
            foreach (String entry in player.CurrentRoom.DetailedDescription)
                player.Send(entry);
        }
    }
}
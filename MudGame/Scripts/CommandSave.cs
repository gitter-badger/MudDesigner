/// <summary>
/// The Save command will save the current player to a hard-disk file.
/// </summary>
public class CommandSave : IGameCommand
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
    public CommandSave()
    {
        Help = new List<String>();
        Help.Add("Saves your character immediately.");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //Save the player to the hard-disk.
        player.Save(player.ActiveGame.DataPaths.Players);
    }
}
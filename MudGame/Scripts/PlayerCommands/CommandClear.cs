/// <summary>
/// The Clear command is used to clear the players terminal screen of all text.
/// </summary>
public class CommandClear : IGameCommand
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
    public CommandClear()
    {
        Help = new List<String>();
        Help.Add("The Clear command is used to clear the screen of all text.");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //Call the flush method  to clear the players console screen of all text.
        player.FlushConsole();
    }
}
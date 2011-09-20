/// <summary>
/// The Clear command is used to clear the players terminal screen of all text.
/// </summary>
public class CommandClear : BaseCommand
{
    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public CommandClear()
    {
        Help.Add("The Clear command is used to clear the screen of all text.");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public override void Execute(String command, BaseCharacter player)
    {
        //Call the flush method  to clear the players console screen of all text.
        player.FlushConsole();
    }
}
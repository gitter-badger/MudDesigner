/// <summary>
/// The GetTime command is used to print the current in-game time to the player.
/// This command will print the day, month and year along with hour, minute and seconds.
/// </summary>
public class CommandGetTime : BaseCommand
{
    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public CommandGetTime()
    {
        Help.Add("Gives you the current time and date in the game world.");
    }

    /// <summary>
    /// Executes the Command.
    /// This method is called from the Command Engine, it is not recommended that you call this method directly.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="player"></param>
    public override void Execute(String command, BaseCharacter player)
    {
        //Send the returned String containing the World Time to the player.
        player.Send(player.ActiveGame.WorldTime.GetCurrentWorldTime());
    }
}
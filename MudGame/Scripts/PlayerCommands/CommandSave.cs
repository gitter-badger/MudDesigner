/// <summary>
/// The Save command will save the current player to a hard-disk file.
/// </summary>
public class CommandSave : BaseCommand
{
    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public CommandSave()
    {
        Help.Add("Saves your character immediately.");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //Save the player to the hard-disk.
        player.Save();
    }
}
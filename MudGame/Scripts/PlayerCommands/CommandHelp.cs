/// <summary>
/// Help command provides information on all the existing game commands, including script based commands.
/// </summary>
public class CommandHelp : IGameCommand
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
    public CommandHelp()
    {
        Help = new List<String>();
        Help.Add("Provides help on various topics.");
        Help.Add("You may ask for help on any game command.");
        Help.Add("Usage: Help");
        Help.Add("Usage: Help 'command' where command = a game command");
        Help.Add("Example: Help Look");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //Check if we have a topic that the player wants help with. If there is nothing after the Help word
        //in the command, then the user didn't supply us with a topic.
        String topic = command.Substring("Help".Length);

        //if the user did not supply us with a topic, we will print every command currently loaded in the engine
        //and tell the user how they can access help information regarding that command.
        //TODO: Help command should have self contained help topics.
        if (topic.Length == 0)
        {
            player.Send("Available commands: ", false);

            //Print each command found within the command engine.
            foreach (String cmd in CommandEngine.GetCommands())
            {
                //We will get a reference to the current command in the iteration
                IGameCommand g = CommandEngine.GetCommand(cmd);
                //Print the name of the command to the user, place a comma after the command
                //so that the next command can be placed afterwards.
                player.Send(CommandEngine.GetCommandName(g) + ", ", false);
            }
            
            //Let the player know how to use the help command to access help regarding any of the aformentioned commands.
            player.Send("");
            player.Send("Usage: Help 'Command'");
            return;
        }
            //The player supplied a topic, lets trip out all the white spaces from it caused by the Substring method
        else
            topic = topic.Trim();

        //Get a reference to the command the player wants help with. We must insert the 'Command' String into the topic,
        //as all Commands start with the word Command, however the player never sees the word Command. It's internal only.
        IGameCommand gc = CommandEngine.GetCommand("Command" + topic);

        //Iterate through each entry in the commands help collection and print it to the player.
        foreach (String help in gc.Help)
            player.Send(help);
    }
}
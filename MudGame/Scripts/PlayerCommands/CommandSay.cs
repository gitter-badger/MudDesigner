/// <summary>
/// The Say command provides the player with the ability to chat with other players within the game world.
/// Players using the Say command can only talk to players that are currently within the same Room.
/// </summary>
public class CommandSay : IGameCommand
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
    public CommandSay()
    {
        Help = new List<String>();
        Help.Add("Allows you to Chat with with players around you.");
        Help.Add("Note that you can only chat with players that reside within the same Room as yourself.");
        Help.Add("Usage: Say Hello everyone!");
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //Check if the user only said 'Say ' or 'Say' with no message content.
        if (command.Length <= 4)
        {
            //If no message content, print the help for the command to the player.
            CommandHelp help = new CommandHelp();
            help.Execute("Help Say", player);
            return; //nothing to say, don't say anything at all.
        }

        //Get the message out of the command String.
        String message = command.Substring("Say ".Length);

        //Query the game world and find what players are within the same location as the chatting player.
        var playerQuery =
            from p in player.ActiveGame.GetPlayerCollection()
            where p.CurrentWorldLocation == player.CurrentWorldLocation
            select p;

        //Send the message to each player found within our player query.
        foreach (var p in playerQuery)
            p.Send(player.Name + " says: " + message);

        //Print the same message but in a alternate format to the player that sent the message originally.
        player.Send("You say: " + message);

    }
}
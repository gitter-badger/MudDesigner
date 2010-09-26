/// <summary>
/// The Walk command allows players to walk from Room to Room, traversing the Mud world.
/// The command supports all of the available travel directions found within the 
/// MudEngine.GameObjects.Environment.AvailableTravelDirections enum.
/// </summary>
public class CommandWalk : IGameCommand
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
    public CommandWalk()
    {
        Help = new List<String>();
        Help.Add("Allows you to traverse through the world.");
        Help.Add("You may walk in any direction available within the current Room.");
        Help.Add("You may use the Look command to see a description of the current Room, and decide where you would like to walk.");
        Help.Add("Usage: Walk 'Direction' where Direction may equal one of the following:");

        //We will construct a string that contains all of the available travel directions for the player.
        StringBuilder directions = new StringBuilder();
        
        //Store a array of existing values within the AvailableTravelDirection enum. 
        //These values are the legal travel directions that are supported by the game.
        Array values = Enum.GetValues(typeof(AvailableTravelDirections));

        //Loop through the array, printing each travel direction we found in the enum array collection
        //to the screen for the user to see and select from.
        foreach (Int32 v in values)
        {
            //Since enum values are not strings, we can't simply assign a string value to the enum.
            //The enum needs to be queried to retrieve a value that matches that of 'v' and convert it to a String
            String displayName = Enum.GetName(typeof(AvailableTravelDirections), v);

            //Add the current travel direction to our string for later use.
            directions.Append(displayName + ", ");
        }

        //Place the newly constructed String with all of our available travel directions into the help collection.
        Help.Add(directions.ToString());

        //Note that you could have placed each direction in manually "Help.Add("West")" etc. however this would
        //prove an issue with maintanence if directions were changed within the engine, new ones added or old ones removed.
        //Getting the travel directions the way we did always ensures that the help command will always show every 
        //available travel direction contained within the engine, regardless if we add or remove directions internally or not.
        //It's a dynamic way of displaying our directions.
    }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public void Execute(String command, BaseCharacter player)
    {
        //Since the walk command requires a second word (ex: walk north)
        //we split the words into an array so they are seperate.
        String[] words = command.Split(' ');
        List<String> directions = new List<String>();

        //We only 1 word was found within the array, then the user failed to provide a second word
        //which would be the travel direction they are wanting to go.
        if (words.Length == 1)
        {
            player.Send("No direction provided!");
            return;
        }
        //The user supplied a traveling direction for us, lets ensure it's a valid one.
        else
        {
            //iterate through each door within the current Room and see if we have a Door that
            //contains a exit in the direction that the player supplied.
            foreach (Door door in player.CurrentRoom.Doorways)
            {
                //See if the current door has the same travel direction value as that of the users entered direction.
                if (door.TravelDirection == TravelDirections.GetTravelDirectionValue(words[1]))
                {
                    //The matches the users direction, so move the player in the direction supplied by the user.
                    player.Move(door.TravelDirection);

                    //BAD - Don't invoke commands directly by the player as it causes issues for them client-side.
                    //Always invoke the command internally, passing a reference to the player instead.
                    //player.ExecuteCommand("Look");
                    
                    //Use the Look command to print the contents of the new Room to the Player.
                    //Good - Correct way of invoking commands automatically for the player.
                    //The command will be executed for which ever player is passed as a reference.
                    IGameCommand look = CommandEngine.GetCommand("CommandLook");
                    if (look != null)
                        look.Execute("look", player);
                    
                    //If the current Active Game has Auto-Save enabled, we will save the player.
                    if (player.ActiveGame.AutoSave)
                        player.Save();

                    return;
                }
            }
        }

        //If the user entered a travel direction that does not exist within the current Room, they will be told so
        //and no moving will be performed.
        player.Send("Unable to travel in that direction.");
    }
}

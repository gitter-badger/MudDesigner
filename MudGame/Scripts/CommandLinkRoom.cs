/// <summary>
/// The LinkRoom command is used to Link two previously created Rooms together.
/// Rooms linked together can be traversed by players. 
/// This command is used to link Rooms dynamically during run-time by Admins, allowing environments to be created
/// and traversed on-the-fly without the need to modify scripts and re-start the server.
/// </summary>
public class CommandLinkRoom : IGameCommand
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
    public CommandLinkRoom()
    {
        //Instance the help collection and add our help information to it.
        Help = new List<string>();
        Help.Add("Use this to link two previously created Rooms together.");
        //Incase Admins try to use the command, they will know that it's broken.
        //Don't convert this class into a Script until it is fully completed.
        Help.Add("NOTE: This command is not fully implemented. You may link two Rooms together provided they exist within the same Zone.");
        Help.Add("You may experience some Rooms not being traversable even after linking.");
        Help.Add("This command is a work-in-progress and will contain bugs.");
    }

    /// <summary>
    /// Executes the command.
    /// This method is called from the Command Engine, it is not recommended that you call this method directly.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="player"></param>
    public void Execute(String command, BaseCharacter player)
    {
        //Check if the Player has the correct privileges to Link Rooms together.
        //If they are not a Admin or GM then the command will bail.
        //This creates the illusion that this command possibly doesn't exist.
        if ((player.Role != SecurityRoles.Admin) && (player.Role != SecurityRoles.GM))
        {
            player.Send("Invalid Command.");
            return;
        }

        //Build the Menu system that will be displayed to Admins.
        player.Send("Room linkage tool");
        player.Send("Please select an option.");
        player.Send("1: Link Room");
        player.Send("2: Exit");

        //Read the input from the Admin.
        string input = player.ReadInput();
        Int32 value = 0;

        //Attempt to convert their input from a String into a numerical value.
        //If they entered a non-numerical value, then the method will exit
        try
        {
            value = Convert.ToInt32(input);
        }
        catch (Exception)
        {
            player.Send("Invalid selection. Please use numeric values.");
            return;
        }

        //Ask what Room the Admin wants to use as the Departing Room. 
        //Departing Rooms will be where the travel direction originates from.
        //meaning if the Admin selects West as the traveling direction, then traveling West will exit the 
        //Departing Room and enter the Arrival Room. 
        player.Send("");
        player.Send("Please select which Realm your departing Room resides within:");
        player.Send("");

        //Instance a new Realm that we can use to reference an existing Realm so that we can find the
        //Rooms within it that the Admin is looking for.
        Boolean isValidRealm = false;
        Realm realm = new Realm(player.ActiveGame);

        //Create a Loop incase the Admin enters an invalid Realm, we can just loop
        //back and re-ask the Admin the enter a valid Realm name.
        while (!isValidRealm)
        {
            //Now that we are in the loop, set the isValidRealm to true, as we will assume that
            //the user will enter a valid Realm name on the first shot. If we can't find a matching
            //Realm, then this will get set to false once iterating through Realms is completed.
            isValidRealm = true;

            //Print each Realm out to the Admin so they may see a complete list to choose from.
            //This is the Realm that their first Room will reside within.
            //TODO: Currently only linking Rooms within the same Realm/Zone is supported. Need to fix that.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
                player.Send(r.Filename + " | ", false);

            //As for the Admins selection, we will place the Admins input on the same line
            //as the last message sent to the Admin by setting the newLine parameter to false.
            player.Send("");
            player.Send("Selection: ", false);

            //Get the Admins input that should specify what Realm they are wanting.
            input = player.ReadInput();

            //Check if the Admin entered 'Cancel'. If so, then cancel the Link process.
            if (input.ToLower() == "cancel")
            {
                player.Send("Room Linking aborted.");
                return;
            }

            //Query the Active Games world collection, finding Realms that match the filename entered by the Admin
            var realmQuery =
                from r in player.ActiveGame.World.RealmCollection
                where r.Filename.ToLower() == input.ToLower()
                select r;

            //Check if the query contains a Realm.
            if (realmQuery.FirstOrDefault() != null)
            {
                //The query does in-fact contain a valid Realm. Assign it to our realm field for use later.
                realm = realmQuery.FirstOrDefault();
                //We can set this to true, allowing us to exit out of the loop.
                isValidRealm = true;
            }
            //If the query does not contain a Realm, then we ensure that the loop will continue by forcing
            //isValidRealm back to false.
            else
            {
                isValidRealm = false;
                //Let the Admin know that they entered an invalid Realm name and that they need to try again.
                player.Send("That Realm does not exist! Please try again.");
            }
        }

        //Let the Admin know that they need to now select which Zone the Room they are wanting to link resides within.
        player.Send("");
        player.Send("Please select which Zone your departing Room resides within:");
        player.Send("");

        //Instance a new Zone that we can use to reference an existing Zone so that we can find the
        //Rooms within it that the Admin is looking for.
        Boolean isValidZone = false;
        Zone zone = new Zone(player.ActiveGame);

        //Create a Loop incase the Admin enters an invalid Zone, we can just loop
        //back and re-ask the Admin the enter a valid Zone name.
        while (!isValidZone)
        {
            //Now that we are in the loop, set the isValidZone to true, as we will assume that
            //the user will enter a valid Zone name on the first shot. If we can't find a matching
            //Zone, then this will get set to false once iterating through Zone is completed.
            isValidZone = true;

            //Print each Zone out to the Admin so they may see a complete list to choose from.
            //This is the Zone that their first Room will reside within.
            //TODO: Currently only linking Rooms within the same Realm/Zone is supported. Need to fix that.
            foreach (Zone z in realm.ZoneCollection)
            {
                player.Send(z.Filename + " | ", false);
            }

            //As for the Admins selection, we will place the Admins input on the same line
            //as the last message sent to the Admin by setting the newLine parameter to false.
            player.Send("");
            player.Send("Selection: ", false);

            //Get the Admins input that should specify what Zone they are wanting.
            input = player.ReadInput();

            //Check if the Admin entered 'Cancel'. If so, then cancel the Link process.
            if (input.ToLower() == "cancel")
            {
                player.Send("Room Linking aborted.");
                return;
            }

            //Query the stored Realm's Zone collection, finding Zones that match the filename entered by the Admin
            var zoneQuery =
                from z in realm.ZoneCollection
                where z.Filename.ToLower() == input.ToLower()
                select z;

            //Check if the query contains a Zone.
            if (zoneQuery.FirstOrDefault() != null)
            {
                //The query does in-fact contain a valid Zone. Assign it to our zone field for use later.
                zone = zoneQuery.FirstOrDefault();
                //We can set this to true, allowing us to exit out of the loop.
                isValidZone = true;
            }
            //If the query does not contain a Zone, then we ensure that the loop will continue by forcing
            //isValidZone back to false.
            else
            {
                isValidZone = false;
                //Let the Admin know that they entered an invalid Zone name and that they need to try again.
                player.Send("That Zone does not exist! Please try again.");
            }
        }

        //Let the Admin know that they need to now select Room they are wanting to link as the departure Room
        player.Send("");
        player.Send("Please select which Room that you wish to be the departing Room:");
        player.Send("");

        //Instance a new Room that we can store a reference to a existing Room.
        //We will use this reference to link the departure and arrival Rooms together.
        Boolean isValidRoom = false;
        Room departingRoom = new Room(player.ActiveGame);

        //Create a Loop incase the Admin enters an invalid Room, we can just loop
        //back and re-ask the Admin the enter a valid Room name.
        while (!isValidRoom)
        {
            //Now that we are in the loop, set the isValidRoom to true, as we will assume that
            //the user will enter a valid Room name on the first shot. If we can't find a matching
            //Room, then this will get set to false once iterating through Room is completed.
            isValidRoom = true;

            //Print each Room out to the Admin so they may see a complete list to choose from.
            //This will be their departing Room.
            //TODO: Currently only linking Rooms within the same Realm/Zone is supported. Need to fix that.
            foreach (Room r in zone.RoomCollection)
            {
                player.Send(r.Filename + " | ", false);
            }

            //As for the Admins selection, we will place the Admins input on the same line
            //as the last message sent to the Admin by setting the newLine parameter to false.
            player.Send("");
            player.Send("Selection: ", false);

            //Get the Admins input that should specify what Room they are wanting.
            input = player.ReadInput();

            //Check if the Admin entered 'Cancel'. If so, then cancel the Link process.
            if (input.ToLower() == "cancel")
            {
                player.Send("Room Linking aborted.");
                return;
            }

            //Query the referenced zone's Room collection, finding Rooms that match the filename entered by the Admin
            var roomQuery =
                from r in zone.RoomCollection
                where r.Filename.ToLower() == input.ToLower()
                select r;

            //Check if the query contains a Room.
            if (roomQuery.FirstOrDefault() != null)
            {
                //The query does in-fact contain a valid Room. Assign it to our departingRoom field for use later.
                departingRoom = roomQuery.FirstOrDefault();
                //We can set this to true, allowing us to exit out of the loop.
                isValidRoom = true;
            }
            //If the query does not contain a Room, then we ensure that the loop will continue by forcing
            //isValidRoom back to false.
            else
            {
                isValidRoom = false;
                //Let the Admin know that they entered an invalid Room name and that they need to try again.
                player.Send("That Room does not exist! Please try again.");
            }
        }

        //Let the Admin know that they need to now select Room they are wanting to link as the Arrival Room
        player.Send("");
        player.Send("Please select which Room that you wish to be the Arrival Room:");
        player.Send("");

        //Instance a new Room that we can store a reference to a existing Room.
        //We will use this reference to link the departure and arrival Rooms together.
        isValidRoom = false;
        Room arrivalRoom = new Room(player.ActiveGame);

        //Create a Loop incase the Admin enters an invalid Room, we can just loop
        //back and re-ask the Admin the enter a valid Room name.
        while (!isValidRoom)
        {
            //Now that we are in the loop, set the isValidRoom to true, as we will assume that
            //the user will enter a valid Room name on the first shot. If we can't find a matching
            //Room, then this will get set to false once iterating through Room is completed.
            isValidRoom = true;

            //Print each Room out to the Admin so they may see a complete list to choose from.
            //This will be their departing Room.
            //TODO: Currently only linking Rooms within the same Realm/Zone is supported. Need to fix that.
            foreach (Room r in zone.RoomCollection)
            {
                player.Send(r.Filename + " | ", false);
            }

            //As for the Admins selection, we will place the Admins input on the same line
            //as the last message sent to the Admin by setting the newLine parameter to false.
            player.Send("");
            player.Send("Selection: ", false);

            //Get the Admins input that should specify what Room they are wanting.
            input = player.ReadInput();

            //Check if the Admin entered 'Cancel'. If so, then cancel the Link process.
            if (input.ToLower() == "cancel")
            {
                player.Send("Room Linking aborted.");
                return;
            }

            //Query the referenced zone's Room collection, finding Rooms that match the filename entered by the Admin
            var roomQuery =
                from r in zone.RoomCollection
                where r.Filename.ToLower() == input.ToLower()
                select r;

            //Check if the query contains a Room.
            if (roomQuery.FirstOrDefault() != null)
            {
                //The query does in-fact contain a valid Room. Assign it to our departingRoom field for use later.
                departingRoom = roomQuery.FirstOrDefault();
                //We can set this to true, allowing us to exit out of the loop.
                isValidRoom = true;
            }
            //If the query does not contain a Room, then we ensure that the loop will continue by forcing
            //isValidRoom back to false.
            else
            {
                isValidRoom = false;
                //Let the Admin know that they entered an invalid Room name and that they need to try again.
                player.Send("That Room does not exist! Please try again.");
            }
        }

        //Let the Admin select the direction of travel required for a player to traverse from the
        //departing Room into the arriving Room.
        player.Send("Please select which direction you would like to travel while departing the departure Room.");

        //Store a array of existing values within the AvailableTravelDirection enum. 
        //These values are the legal travel directions that are supported by the game.
        Array values = Enum.GetValues(typeof(AvailableTravelDirections));

        //Create a reference to the AvailableTravelDirections enum that we can use to assign a travel
        //when we pass the Room linking off to the Rooms owning Zone.
        AvailableTravelDirections direction = new AvailableTravelDirections();
        Boolean isValidDirection = false;

        //Perform the direction corrections within a while loop, incase the Admin enteres
        //a invalid traveling direction and we need to re-acquire input from the Admin.
        while (!isValidDirection)
        {
            //Loop through the array, printing each travel direction we found in the enum array collection
            //to the screen for the user to see and select from.
            foreach (Int32 v in values)
            {
                //Since enum values are not strings, we can't simply assign a string value to the enum.
                //The enum needs to be queried to retrieve a value that matches that of 'v' and convert it to a String
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), v);

                //Print this current iterations value to the screen for the player.
                player.Send(displayName + " | ");
            }

            //As for the Admins selection, we will place the Admins input on the same line
            //as the last message sent to the Admin by setting the newLine parameter to false.
            player.Send("Enter Selection: ", false);
            input = player.ReadInput();

            //Loop through each value found in our original array or values acquired from the AvailableTravelDirections enum.
            foreach (Int32 v in values)
            {
                //Since enum values are not strings, we can't simply assign a string value to the enum.
                //The enum needs to be queried to retrieve a value that matches that of 'v' and convert it to a String
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), v);

                //Check if the Admins selected travel direction matches that of the current travel direction value found
                //within our array of values harvested from the AvailableTravelDirections enum
                if (displayName.ToLower() == input.ToLower())
                {
                    //The two match up. Now you have to convert the String version of the travel direction, back into
                    //an enum equivilant for use by the Rooms owning Zone during linking.
                    direction = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);

                    //We found a legal Travel Direction. Setting this to true will exit the while loop.
                    isValidDirection = true;
                    break;
                }
                else
                    isValidDirection = false;
            }

            //Check if we haven't found a valid direction yet. This will print to the Admin that the entered
            //direction was invalid, and then loop back to the top of the current loop.
            if (!isValidDirection)
            {
                player.Send("Invalid direction supplied!");
            }
            //The direction supplied by the Admin matched up and was concidered legal, so we shoud
            //have a converted AvailableTravelDirection now with the value matching that of the Admins.
            //Link the two rooms together, with the travel direction of the Admins choice.
            else
            {
                zone.LinkRooms(direction, arrivalRoom, departingRoom);
            }
        }
    }
}
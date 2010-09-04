/// <summary>
/// The WorldCalifornia script is used to construct the Game world.
/// </summary>
public class WorldCalifornia
{
    //Our current Active Game. Allows us to add content to the game.
    private Game _Game;

    /// <summary>
    /// Our Constructor.
    /// </summary>
    /// <param name="game"></param>
    public WorldCalifornia(Game game)
    {
        //Set _Game to reference the current Active Game.
        _Game = game;
    }

    /// <summary>
    /// Creates the world the player will interact with.
    /// The WorldCalifornia is used soley for creating the California Realm.
    /// It is recommended that the creation of other Realms be done in their own respective classes mimicking this one.
    /// </summary>
    public void Create()
    {
        //Get a new Instance of a Realm.
        Realm myRealm = new Realm(_Game);
        
        //Setup the Realm properties.
        myRealm.Name = "California";
        myRealm.Description = "The Beaches of California are relaxing and fun to be at.";

        //Setting the Realms IsInitialRealm to true will let the current Active Game know where to place newly created players.
        myRealm.IsInitialRealm = true;

        //Add the Realm to the current Active Game World.
        _Game.World.AddRealm(myRealm);

        //Get a new Instance of a Zone.
        Zone myZone = new Zone(_Game);

        //Setup the Zone properties
        myZone.Name = "San Diego";
        myZone.Description = "San Diego has many attractions, including Sea World!";

        //Place this Zone within our previously created Realm
        myZone.Realm = myRealm.Filename;
        
        //Setting the Zones IsInitialZone to true will let the current Active Game know where to place newly created players.
        myZone.IsInitialZone = true;

        //Add the Zone to the current Active Game World
        myRealm.AddZone(myZone);

        //Get a new Instance of a Room.
        Room myRoom = new Room(_Game);

        //Setup the Room properties
        myRoom.Name = "Hotel Room B33";
        myRoom.DetailedDescription.Add("Your Hotel Room is pretty clean, it is small but not to far off from the beach so you can't complain.");
        myRoom.DetailedDescription.Add("You can exit your Hotel Room by walking West");

        //Setting the Rooms IsInitialRoom to true will let the current Active Game know where to place newly created players.
        myRoom.IsInitialRoom = true;

        //Add the Room to the previously created Zone.
        //TODO: The engine needs to provide World.AddObject() support to Rooms, so that Rooms may be added in the same
        //manor as Realms and Zones.
        myZone.AddRoom(myRoom);

        //Get a new Instance of a Room
        Room myHallway = new Room(_Game);

        //Setup the Room properties
        myHallway.Name = "Hotel Hallway";
        myHallway.DetailedDescription.Add("The Hotel Hallway is fairly narrow, but there is plenty of room for people to traverse through it.");
        myHallway.DetailedDescription.Add("Your Hotel Room B33 is to the East.");
        myHallway.DetailedDescription.Add("Hotel Room B34 is to your West.");
        
        //Since more than one 'Hotel hallway' may exist, we will differentiate them by setting a custom filename.
        //Note that assigning a new Filename must be done AFTER assigning the objects Name property a value.
        //Each time a objects Name property is assigned a value, it automatically generates a filename.
        //If a Filename is assigned prior to assigning the Name property, your previous Filename will be over-wrote.
        myHallway.Filename = myHallway.Name + "1.Zone";

        //Add the Room to the previously created Zone.
        myZone.AddRoom(myHallway);

        //Link the two previously created Rooms together.
        //Player must walk 'West' to exit myRoom and enter myHallway.
        //Zone Linking automatically places a reverse doorway in the myHallway Room,
        //allowing players to walk 'East' to exit myHallway and enter myRoom once again.
        myZone.LinkRooms(AvailableTravelDirections.West, myHallway, myRoom);

        //Get a new Instance of a Room
        Room nextRoom = new Room(_Game);

        //Setup the Room properties
        nextRoom.Name = "Hotel Room B34";
        nextRoom.DetailedDescription.Add("This Hotel Room is pretty dirty, they must not have cleaned it yet.");
        nextRoom.DetailedDescription.Add("You can exit this room by walking East");

        //Add the Room to the previously created Zone
        myZone.AddRoom(nextRoom);

        //Link the new Room to the previously created myHallway.
        //Players must walk West out of the hallway to enter this room.
        myZone.LinkRooms(AvailableTravelDirections.East, myHallway, nextRoom);

        /* Current Room layout is as shown.
         * 
         * Room B33 -> Hallway <- Room B34
         * 
         */
    }
}
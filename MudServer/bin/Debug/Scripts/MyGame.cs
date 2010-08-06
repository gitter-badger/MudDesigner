public class MyGame : Game
{
    public MyGame() :base()
    {
        AutoSave = true;
        BaseCurrencyName = "Copper";
        BaseCurrencyAmount = 1;
        CompanyName = "Mud Designer";
        DayLength = 60 * 8;
        GameTitle = "Test Mud Project";
        HideRoomNames = false;
        PreCacheObjects = true;
        ProjectPath = FileManager.GetDataPath(SaveDataTypes.Root);
        TimeOfDay = TimeOfDayOptions.Transition;
        TimeOfDayTransition = 30;
        Version = "0.1";
        Website = "http://MudDesigner.Codeplex.com";
        ServerPort = 555;
        MaximumPlayers = 1000;

        ConstructRealm();

        PlayerCollection = new MyPlayer[MaximumPlayers];
    }

    void ConstructRealm()
    {
        //Instace our Realm
        Realm realm = new Realm(this);

        //Set it up.
        realm.Name = "Zeroth";
        realm.Description = "The land of " + realm.Name + " is fully of large forests and dangerous creatures.";
        realm.IsInitialRealm = true;
        
        //Add it to the Base Games Realm Collection
        this.AddRealm(realm);

        //Build Zones
        Zone zone = new Zone(this);
        zone.Name = "Home";
        zone.Description = "Your home is small and does not contain many items, but it's still your home and someplace you can relax after your battles.";
        zone.IsSafe = true;
        zone.StatDrain = false;
        zone.IsInitialZone = true;
        zone.Realm = realm.Name;
        realm.AddZone(zone);

        Room bedroom = new Room(this);
        bedroom.Name = "Bedroom";
        bedroom.DetailedDescription.Add("This is your bedroom, it's small but comfortable. You have a bed, a book shelf and a rug on the floor.");
        bedroom.DetailedDescription.Add("You may walk to the WEST to find you Closet.");
        bedroom.Zone = zone.Name;
        bedroom.Realm = realm.Name;
        bedroom.IsInitialRoom = true;
        zone.AddRoom(bedroom);

        Room closet = new Room(this);
        closet.Name = "Closet";
        closet.DetailedDescription.Add("Your closet contains clothing and some shoes.");
        closet.DetailedDescription.Add("You may walk to your EAST to find your Room.");
        closet.Zone = zone.Name;
        closet.Realm = realm.Name;
        zone.AddRoom(closet);

        zone.LinkRooms(AvailableTravelDirections.West, closet, bedroom);
    }
}
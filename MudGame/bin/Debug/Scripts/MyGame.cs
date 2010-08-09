public class MyGame : Game
{
    public MyGame()
        : base()
 {
     GameTitle = "Mud Designer Example Game";
     IsMultiplayer = true;

     CompanyName = "Mud Designer Team";
     Website = "Visit Http://MudDesigner.Codeplex.com for the latest News, Documentation and Releases.";
     Version = "Example Game Version 1.0";
     MaximumPlayers = 5000;

     //Instance our Realm
     Realm myRealm = new Realm(this);
     myRealm.Name = "California";
     myRealm.Description = "The Beaches of California are relaxing and fun to be at.";
     myRealm.IsInitialRealm = true;

     //Add the Realm to the Games RealmCollection
     AddRealm(myRealm);

     Zone myZone = new Zone(this);
     myZone.Name = "San Diego";
     myZone.Description = "San Diego has many attractions, including Sea World!";
     myZone.IsInitialZone = true;

     //Add the Zone to the Realm
     myRealm.AddZone(myZone);

     //Create our HotelRoom
     Room myRoom = new Room(this);
     myRoom.Name = "Hotel Room B33";
     myRoom.IsInitialRoom = true;
     myRoom.DetailedDescription.Add("Your Hotel Room is pretty clean, it is small but not to far off from the beach so you can't complain.");
     myRoom.DetailedDescription.Add("You can exit your Hotel Room by walking West");
     //Add the Hotel Room to the Zones Room Collection
     myZone.AddRoom(myRoom);

     Room myHallway = new Room(this);
     myHallway.Name = "Hotel Hallway";
     myHallway.DetailedDescription.Add("The Hotel Hallway is fairly narrow, but there is plenty of room for people to traverse through it.");
     myHallway.DetailedDescription.Add("Your Hotel Room B33 is to the East.");
     myHallway.DetailedDescription.Add("Hotel Room B34 is to your West.");
     //Add the Hallway to the Zones Room Collection
     myZone.AddRoom(myHallway);
     
     myZone.LinkRooms(AvailableTravelDirections.West, myHallway, myRoom);
     
     Room nextRoom = new Room(this);
     nextRoom.Name = "Hotel Room B34";
     nextRoom.DetailedDescription.Add("This Hotel Room is pretty dirty, they must not have cleaned it yet.");
     nextRoom.DetailedDescription.Add("You can exit this room by walking East");
     myZone.AddRoom(nextRoom);
     //Link
     myZone.LinkRooms(AvailableTravelDirections.East, myHallway, nextRoom);
 }
}
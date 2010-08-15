public class WorldCalifornia
{
 private Game _Game;

 public WorldCalifornia(Game game)
 {
  _Game = game;
 }

 public void Create()
 {
  //Instance our Realm
     Realm myRealm = new Realm(_Game);
     myRealm.Name = "California";
     myRealm.Description = "The Beaches of California are relaxing and fun to be at.";
     myRealm.IsInitialRealm = true;
     _Game.World.AddRealm(myRealm);

     Zone myZone = new Zone(_Game);
     myZone.Name = "San Diego";
     myZone.Realm = myRealm.Name;
     myZone.Description = "San Diego has many attractions, including Sea World!";
     myZone.IsInitialZone = true;
     myRealm.AddZone(myZone);

     //Create our HotelRoom
     Room myRoom = new Room(_Game);
     myRoom.Name = "Hotel Room B33";
     myRoom.IsInitialRoom = true;
     myZone.AddRoom(myRoom);
     myRoom.DetailedDescription.Add("Your Hotel Room is pretty clean, it is small but not to far off from the beach so you can't complain.");
     myRoom.DetailedDescription.Add("You can exit your Hotel Room by walking West");

     Room myHallway = new Room(_Game);
     myHallway.Name = "Hotel Hallway";
     myHallway.DetailedDescription.Add("The Hotel Hallway is fairly narrow, but there is plenty of room for people to traverse through it.");
     myHallway.DetailedDescription.Add("Your Hotel Room B33 is to the East.");
     myHallway.DetailedDescription.Add("Hotel Room B34 is to your West.");
     myZone.AddRoom(myHallway);
     myZone.LinkRooms(AvailableTravelDirections.West, myHallway, myRoom);

     Room nextRoom = new Room(_Game);
     nextRoom.Name = "Hotel Room B34";
     nextRoom.DetailedDescription.Add("This Hotel Room is pretty dirty, they must not have cleaned it yet.");
     nextRoom.DetailedDescription.Add("You can exit this room by walking East");
     myZone.AddRoom(nextRoom);
     //Link
     myZone.LinkRooms(AvailableTravelDirections.East, myHallway, nextRoom);
 }
}
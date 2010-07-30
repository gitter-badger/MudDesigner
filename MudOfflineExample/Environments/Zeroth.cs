using System;

using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;

namespace MUDGame
{
    internal class Zeroth
    {
        Game game;
        Realm realm;

        internal Zeroth(Game game)
        {
            this.game = game;
            realm = new Realm(game);
        }

        internal void BuildZeroth()
        {
            realm.Name = "Zeroth";
            realm.Description = "The land of " + realm.Name + " is fully of large forests and dangerous creatures.";
            realm.IsInitialRealm = true;
            game.AddRealm(realm);

            //Build Rooms.
            BuildHome();
        }

        private void BuildHome()
        {
            //Build Zones
            Zone zone = new Zone(game);
            zone.Name = "Home";
            zone.Description = "Your home is small and does not contain many items, but it's still your home and someplace you can relax after your battles.";
            zone.IsSafe = true;
            zone.StatDrain = false;
            zone.IsInitialZone = true;
            zone.Realm = realm.Name;
            realm.AddZone(zone);
            
            Room bedroom = new Room(game);
            bedroom.Name = "Bedroom";
            bedroom.Description = "This is your bedroom, it's small but comfortable. You have a bed, a book shelf and a rug on the floor.\nYou may walk to the WEST to find you Closet.";
            bedroom.Zone = zone.Name;
            bedroom.Realm = realm.Name;
            bedroom.IsInitialRoom = true;
            zone.AddRoom(bedroom);

            Room closet = new Room(game);
            closet.Name = "Closet";
            closet.Description = "Your closet contains clothing and some shoes.\nYou may walk to your EAST to find your Room.";
            closet.Zone = zone.Name;
            closet.Realm = realm.Name;
            zone.AddRoom(closet);

            zone.LinkRooms(AvailableTravelDirections.West, closet, bedroom);
        }
    }
}

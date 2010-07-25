using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;

namespace MUDGame.Environments
{
    class Zeroth
    {
        Game game;

        internal Zeroth(Game game)
        {
            this.game = game;
        }

        internal Realm BuildZeroth()
        {
            Realm realm = new Realm();
            realm.Name = "Zeroth";
            realm.Description = "The land of " + realm.Name + " is fully of large forests and dangerous creatures.";
            realm.IsInitialRealm = true;

            //Build Zones
            Zone zone = new Zone();
            zone.Name = "Home";
            zone.Description = "Your home is small and does not contain many items, but it's still your home and someplace you can relax after your battles.";
            zone.IsSafe = true;
            zone.StatDrain = false;
            zone.IsInitialZone = true;

            //Build Rooms.
            BuildHome(zone, realm);

            zone.Realm = realm.Name;

            realm.AddZone(zone);
            game.AddRealm(realm);

            return realm;
        }

        private void BuildHome(Zone zone, Realm realm)
        {
            Room bedroom = new Room();
            bedroom.Name = "Bedroom";
            bedroom.Description = "This is your bedroom, it's small but comfortable. You have a bed, a book shelf and a rug on the floor.";
            bedroom.Zone = zone.Name;
            bedroom.Realm = realm.Name;
            bedroom.IsInitialRoom = true;

            Room closet = new Room();
            closet.Name = "Closet";
            closet.Description = "Your closet contains clothing and some shoes.";
            closet.Zone = zone.Name;
            closet.Realm = realm.Name;
            
            Door door = new Door();
            door.TravelDirection = MudEngine.GameObjects.AvailableTravelDirections.West;
            door.ConnectedRoom = "Closet";
            door.Description = "To the West is your Closet.";
            bedroom.Doorways.Add(door);

            door = new Door();
            door.TravelDirection = MudEngine.GameObjects.AvailableTravelDirections.East;
            door.ConnectedRoom = "Bedroom";
            door.Description = "To the East is your Bedroom.";
            closet.Doorways.Add(door);

            //Todo: Should work once MUDEngine supports Types
            zone.AddRoom(bedroom);
            zone.AddRoom(closet);
        }
    }
}

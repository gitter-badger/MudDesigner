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
            zone.IsStartingZone = true;

            //Build Rooms.
            BuildHome(zone, realm);

            zone.Realm = realm.Name;
            //TODO: Remove this, as Zones and Rooms contain .IsStarting properties now.
            zone.EntranceRoom = "Bedroom";
            realm.Zones.Add(zone);

            return realm;
        }

        private void BuildHome(Zone zone, Realm realm)
        {
            Room bedroom = new Room();
            bedroom.Name = "Bedroom";
            bedroom.Description = "This is your bedroom, it's small but comfortable. You have a bed, a book shelf and a rug on the floor.";
            bedroom.Zone = zone.Name;
            bedroom.Realm = realm.Name;
            bedroom.IsStartingRoom = true;

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
            zone.Rooms.Add(bedroom);
            zone.Rooms.Add(closet);
        }
    }
}

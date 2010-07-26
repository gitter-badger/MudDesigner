using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;

namespace MUDGame.Environments
{
    internal class Zeroth
    {
        Game game;
        Realm realm;

        internal Zeroth(Game game)
        {
            this.game = game;
            realm = new Realm();
        }

        internal void BuildZeroth()
        {
            realm.Name = "Zeroth";
            realm.Description = "The land of " + realm.Name + " is fully of large forests and dangerous creatures.";
            realm.IsInitialRealm = true;

            //Build Rooms.
            BuildHome();
        }

        private void BuildHome()
        {
            //Build Zones
            Zone zone = new Zone();
            zone.Name = "Home";
            zone.Description = "Your home is small and does not contain many items, but it's still your home and someplace you can relax after your battles.";
            zone.IsSafe = true;
            zone.StatDrain = false;
            zone.IsInitialZone = true;

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

            zone.LinkRooms(AvailableTravelDirections.West, closet, bedroom);

            zone.Realm = realm.Name;

            realm.AddZone(zone);
            game.AddRealm(realm);
        }
    }
}

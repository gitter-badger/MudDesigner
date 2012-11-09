using System;
using System.Collections.Generic;
using System.Linq;

using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Properties;

namespace MudDesigner.Scripts.Default.Environment
{
    public class World : MudDesigner.Engine.Environment.World
    {
        public World()
        {
            Description = "The Default World is very dull and boring!";

            //Setup two Rooms first.
            Room bedroom = new Room();
            bedroom.Name = "Bedroom";
            bedroom.Description = "Your bedroom is fairly clean.";

            Room closet = new Room();
            closet.Name = "Closet";
            closet.Description = "There is not really anything of interest in your closet.";

            //Link our two rooms
            bedroom.AddDoorway(AvailableTravelDirections.West, closet);

            //Setup a Zone.
            Zone home = new Zone();
            home.Name = "My Home";
            home.AddRooms(new Room[] { bedroom, closet });

            //Setup a Realm.
            Realm california = new Realm();
            california.Name = "California";
            california.AddZone(home);

            //We are done setting up, so add Realm to our world.
            AddRealm(california);

            //Change our server to use the bedroom as our first room.
            EngineSettings.Default.LoginRoom = bedroom.ToString(); //Important to use ToString() as that returns Realm.Zone.Room, so we can find it later
        }
    }
}

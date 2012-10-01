using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Networking;
using Env = MudDesigner.Engine.Environment;
using MudDesigner.Scripts.Environment;
namespace MudDesigner.Scripts.Game
{
    public class Game : MudDesigner.Engine.Core.Game
    {
        public override bool Initialize(IServer startedServer, Env.IWorld world)
        {
            World w = new World();
            Zone zone = new Zone("Village", null);
            List<Room> rooms = new List<Room>();

            Room bedroom = new Room("Bedroom", zone);
            bedroom.Description = "This is your bedroom.  It's a real mess.";
            rooms.Add(bedroom);

            Room closet = new Room("Closet", zone);
            closet.Description = "Your closet is empty.";
            rooms.Add(closet);

            Room hallway = new Room("Hallway", zone);
            hallway.Description = "Your hallway is dark and cold.";
            rooms.Add(hallway);

            Room kitchen = new Room("Kitchen", zone);
            kitchen.Description = "Your kitchen is very clean and organized.";
            rooms.Add(kitchen);

            //This will link both of the rooms together with a door.
            bedroom.AddDoorway(Env.AvailableTravelDirections.North, hallway, true);

            /* Rather than add individually, you can just add a collection.
            zone.AddRoom(bedroom);
            zone.AddRoom(hallway);
            */
            zone.AddRooms(rooms.ToArray());

            //Create the world.
            w.Create("Azeroth");

            return base.Initialize(startedServer, w);
        }
    }
}

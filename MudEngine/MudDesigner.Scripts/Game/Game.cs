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

            Room bedroom = new Room("Bedroom", zone);
            
            Room hallway = new Room("Hallway", zone);

            //This will link both of the rooms together with a door.
            bedroom.AddDoorway(Env.AvailableTravelDirections.North, hallway, true);

            //Create the world.
            w.Create("Azeroth");

            return base.Initialize(startedServer, w);
        }
    }
}

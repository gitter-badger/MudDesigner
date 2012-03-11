using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.DAL;
using MudEngine.Game.Environment;
using MudEngine.Game;

namespace MudEngine.GameScripts
{
    public class SampleGame : StandardGame
    {
        public SampleGame(String name)
            : base(name)
        {
            this.Name = "Sample Mud Game";
            this.Debugging = true;
            this.Description = "A sample MUD game created using the Mud Designer engine.";
            this.Website = "http://muddesigner.codeplex.com";
        }

        public override Boolean Start(Int32 maxPlayers, Int32 maxQueueSize)
        {
            this.Server.ServerOwner = "Admin";
            base.Start(maxPlayers, maxQueueSize);
            
            
            //Quick demonstration on how to create the initial starting room for new players.
            this.World.CreateRealm("Azeroth", "Starting Realm for beginning players");
            Zone z = this.World.GetRealm("Azeroth").CreateZone("Bedlam", "Initial Zone for new players.");
            Room bedroom = z.CreateRoom("Bedroom", "This is your bedroom.");
            Room hallway = z.CreateRoom("Hallway", "This is the hallway.");

            //Save if the result of the Linkage.
            Boolean linked = bedroom.LinkRooms(AvailableTravelDirections.West, hallway);

            //Call our parent Start() method which will start the world and server
            //along with compile all of our commands and scripts.
            Boolean startOK = base.Start(maxPlayers, maxQueueSize);

            //If the parent started ok and our rooms were linked together
            //Set the starting location as our new room
            if (startOK && linked)
            {
                this.World.StartLocation = bedroom;
                this.World.Save();
                return true;
            }
            //Otherwise return false and prevent the game from running.
            else
            {
                this.Enabled = false;
                return false;
            }
             

            
        }
    }
}

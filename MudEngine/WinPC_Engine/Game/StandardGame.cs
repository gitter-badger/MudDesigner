using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using MudEngine.Networking;
using MudEngine.Core;

namespace MudEngine.Game
{
    public class StandardGame
    {
        public String Name { get; set; }

        public String Website { get; set; }

        public String Description { get; set; }

        public String Version { get; set; }

        public Boolean HiddenRoomNames { get; set; }

        public Boolean Multiplayer { get; set; }

        public Int32 MinimumPasswordSize { get; set; }

        public Boolean AutoSave { get; set; }

        public Boolean Enabled { get; private set; }

        public Boolean Debugging { get; set; }

        public Server Server { get; protected set; }

        public StandardGame(String name) : this(name, 4000)
        {
        }

        public StandardGame(String name, Int32 port)
        {
            Logger.WriteLine("Initializing Standard Mud Game");
            this.Name = name;
            this.Website = "http://scionwest.net";
            this.Description = "A sample Mud game created using the Mud Designer kit.";
            this.Version = "1.0";
            this.Multiplayer = true;
            this.MinimumPasswordSize = 8;
            this.AutoSave = true;
         
            //Setup our server.
            this.Server = new Server(this, port);
        }

        public Boolean Start(Int32 maxPlayers, Int32 maxQueueSize)
        {
            Logger.WriteLine("Starting up Standard Game");
            
            //Instance Script Engine

            //Compile any scripts
            
            //Load our Commands
            CommandSystem.LoadCommands();

            //Load World

            //Start our server.
            this.Server.Start(maxPlayers, maxQueueSize);

            //If the server started without error, flag the Game as enabled.
            if (this.Server.Enabled)
                this.Enabled = true;

            return this.Enabled;
        }

        public void Stop()
        {
            //Save the world.

            this.Server.Stop();

            this.Enabled = false;
        }
    }
}

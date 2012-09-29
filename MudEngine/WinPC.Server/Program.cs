using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

using MudDesigner.Engine.Abstract.Core;
using MudDesigner.Engine.Abstract.Environment;
using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Networking;

namespace MudDesigner.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup the engines log system
            Logger.LogFilename = "StandardGame.Log";
            Logger.Enabled = true;
            Logger.ConsoleOutPut = true;
            Logger.ClearLog(); //Delete previous file.
            Logger.WriteLine("Server app starting...");

            IServer server = new MudDesigner.Engine.Networking.Server(port: 4000);

            //Pull the custom game info that will be used by this MUD
            IGame game = Game.GetCustomGame(MudDesigner.Engine.Properties.Engine.Default.DefaultGame);
            
            //Setup up the world.
            IWorld world = new World();
            //world.Create("Azeroth");

            //It does not matter in what order this is performed, however it is best to start the server
            //after the game.initialize() method is called.  This ensures the game is loaded and ready to go
            //prior to clients connecting to the server.
            game.Initialize(server, world);
            server.Start(maxConnections: 100, maxQueueSize: 20, game: game);

            while (server.Enabled)
            {
            }

            server = null;
        }
    }
}

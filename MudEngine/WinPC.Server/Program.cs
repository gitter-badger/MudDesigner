using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPC.Engine;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Core;
using WinPC.Engine.Networking;

namespace WinPC.Server
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

            IServer server = new WinPC.Engine.Networking.Server(port: 4000);

            //Pull the custom game info that will be used by this MUD
            IGame game = Game.GetCustomGame(WinPC.Engine.Properties.Engine.Default.DefaultGame);

            server.Start(maxConnections: 100, maxQueueSize: 20, game: game);

            while (server.Enabled)
            {
                
            }

            server = null;
        }
    }
}

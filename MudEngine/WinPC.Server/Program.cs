using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MudDesigner.Engine;
using MudDesigner.Engine.Abstract.Core;
using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Core;
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

            string file = Path.Combine(Directory.GetCurrentDirectory(), "Saves", MudDesigner.Engine.Properties.Engine.Default.WorldFile);

            string fileName = Path.GetFileName(MudDesigner.Engine.Properties.Engine.Default.WorldFile);
            string path = Path.GetFullPath(file.Substring(0, file.Length - fileName.Length));
            string root = Path.GetPathRoot(file);

            root = Path.GetDirectoryName(file);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //Pull the custom game info that will be used by this MUD
            IGame game = Game.GetCustomGame(MudDesigner.Engine.Properties.Engine.Default.DefaultGame);

            server.Start(maxConnections: 100, maxQueueSize: 20, game: game);

            while (server.Enabled)
            {
                
            }

            server = null;
        }
    }
}

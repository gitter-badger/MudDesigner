using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Scripting;
namespace MudDesigner.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                //Perform compilation of scripts.
                if (args[1].ToLower() == "-compile")
                {
                    
                }
            }
            //Setup the engines log system
            Logger.LogFilename = "StandardGame.Log";
            Logger.Enabled = true;
            Logger.ConsoleOutPut = true;
            Logger.ClearLog(); //Delete previous file.
            Logger.WriteLine("Server app starting...");

            //Compile the game scripts
            CompileEngine.AddAssemblyReference("MudDesigner.Engine.dll");
            CompileEngine.Compile(MudDesigner.Engine.Properties.Engine.Default.ScriptsPath);
            
            //Add the engine assembly to the Script Factory
            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());
            //Add the compiled scripts assembly to the Script Factory
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);
            //Add any additional assemblies that might have been compiled elsewhere (downloadable assemblies)
            if (MudDesigner.Engine.Properties.Engine.Default.ScriptLibrary.Count != 0)
            {
                foreach (string assembly in MudDesigner.Engine.Properties.Engine.Default.ScriptLibrary)
                {
                    //Make sure the assembly actually exists first.
                    if (File.Exists(assembly))
                        ScriptFactory.AddAssembly(assembly);
                }
            }

            IServer server = new MudDesigner.Engine.Networking.Server(port: 4000);

            //game.Initialize(server, world);

            //Pull the custom game info that will be used by this MUD
            IGame game = (IGame)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.DefaultGameType, null);
            IWorld world = (IWorld)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.DefaultWorldType, null);
            
            //It does not matter in what order this is performed, however it is best to start the server
            //after the game.initialize() method is called.  This ensures the game is loaded and ready to go
            //prior to clients connecting to the server.

            server.Start(maxConnections: 100, maxQueueSize: 20, game: game);

            while (server.Enabled)
            {
            }

            server = null;
        }
    }
}

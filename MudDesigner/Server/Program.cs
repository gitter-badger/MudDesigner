/* Program
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: A stand-alone server that will run the Mud Game Engine when configured properly.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using log4net;

namespace MudDesigner.Server
{
    /// <summary>
    /// A stand-alone server that will run the Mud Game Engine when configured properly.
    /// </summary>
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program)); 
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                //Perform compilation of scripts.
                if (args[1].ToLower() == "-compile")
                {
                    //TODO: Build a separate compiler app
                }
            }

            //Setup the engines log system
            Log.Info("Server app starting...");

            //Loop through each reference mentioned in the engines properties and add them.
            //This provides support for 3rd party pre-compiled "Mods" scripts
            foreach (string reference in EngineSettings.Default.ScriptLibrary)
            {
                string path = Path.Combine(System.Environment.CurrentDirectory, reference);
                CompileEngine.AddAssemblyReference(path);
            }

            //Compile the scripts within the engine properties 'ScriptsPath'
            bool results = CompileEngine.Compile(MudDesigner.Engine.Properties.EngineSettings.Default.ScriptsPath);
            if (!results)
            {
                Console.WriteLine(CompileEngine.Errors);
                Console.WriteLine("Press any key to close.");
                Console.ReadKey();
                return;
            }

            //Add the compiled scripts assembly to the Script Factory
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);

            //Now add all of the pre-compiled scripts to the script factory so we
            //can instance them if we need to.
            foreach (string reference in EngineSettings.Default.ScriptLibrary)
            {
                ScriptFactory.AddAssembly(reference);
            }

            //Instance the server.
            IGame game = (IGame)ScriptFactory.GetScript(EngineSettings.Default.GameScript, null);
            if (game == null)
            {
                game = (IGame)ScriptFactory.FindInheritedScript("MudDesigner.Engine.Core.Game", null);

                if (game == null)
                {
                    Console.WriteLine("Could not locate a Game script to run the server with. Server will not start.");
                    Log.Error("Failed to locate a Game script to run the server. Server failed to start.");
                    Console.ReadKey();
                    return;
                }
            }

            IServer server = new MudDesigner.Engine.Networking.Server(port: 4000);
            game.Initialize(server);

            game.RestoreWorld();

            //It does not matter in what order this is performed, however it is best to start the server
            //after the game.initialize() method is called.  This ensures the game is loaded and ready to go
            //prior to clients connecting to the server.

            //Start the server.
            server.Start(maxConnections: 500, maxQueueSize: 20, game: game);

            Console.WriteLine("Server running...");
            Log.Info("Server startup completed.");
            while (server.Enabled)
            {
            }

            server = null;
        }
    }
}

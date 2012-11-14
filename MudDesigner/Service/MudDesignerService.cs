using System;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Scripting;
using log4net;

namespace Service
{
    public class MudDesignerService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MudDesignerService)); 

        IServer _server = new MudDesigner.Engine.Networking.Server(port: 4000);
        public bool IsEnabled { get {return _server.Enabled; } }

        public void Start(string[] args)
        {
              if (args.Length == 2)
            {
                //Perform compilation of scripts.
                if (args[1].ToLower() == "-compile")
                {
                    
                }
            }
            //Setup the engines log system
            //Logger.LogFilename = "StandardGame.Log";
            //Logger.Enabled = true;
            //Logger.ConsoleOutPut = true;
            //Logger.ClearLog(); //Delete previous file.
            //Logger.WriteLine("Server app starting...");
            Log.Info("Server app starting...");

            //Compile the game scripts
            CompileEngine.AddAssemblyReference(Environment.CurrentDirectory + "\\MudDesigner.Engine.dll");
            CompileEngine.AddAssemblyReference(Environment.CurrentDirectory + "\\log4net.dll");

            //Compile the scripts within the engine properties 'ScriptsPath'
            CompileEngine.Compile(MudDesigner.Engine.Properties.EngineSettings.Default.ScriptsPath);
            
            //Add the compiled scripts assembly to the Script Factory
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);

            //Instance the server.
           

            //Pull the custom game info that will be used by this MUD from the engine properties file
            IGame game = (IGame)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.GameScript, null);

            //Initialize the custom game class, passing along the server.
            game.Initialize(_server);
            
            //It does not matter in what order this is performed, however it is best to start the server
            //after the game.initialize() method is called.  This ensures the game is loaded and ready to go
            //prior to clients connecting to the server.

            //Start the server.
            _server.Start(maxConnections: 100, maxQueueSize: 20, game: game);

            while (_server.Enabled)
            {
            }

            _server = null;
        }

        public void StopServer()
        {
            _server.Stop();
        }

        
        
    }
}
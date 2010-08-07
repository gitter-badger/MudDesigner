using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;

using MudEngine.FileSystem;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Characters;
using MudEngine.Scripting;
using MUDGame; //Pulling this from the example game, no sense re-writing what already exists.

namespace MudServer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Log.Write("Launching...");
            ScriptEngine scriptEngine;
            Game game;

            //Re-create the settings file if it is missing
            if (!File.Exists("Settings.ini"))
            {
                Log.Write("Settings.ini missing!");
                FileManager.WriteLine("Settings.ini", "Scripts", "ScriptPath");
                FileManager.WriteLine("Settings.ini", ".cs", "ScriptExtension");
                Log.Write("Settings.ini re-created with default values");
            }

            Log.Write("Loading settings...");
            scriptEngine = new ScriptEngine(new Game(), ScriptEngine.ScriptTypes.SourceFiles);
            scriptEngine.ScriptPath = FileManager.GetData("Settings.ini", "ScriptPath");
            scriptEngine.ScriptExtension = FileManager.GetData("Settings.ini", "ScriptExtension");

            //scriptEngine.CompileScripts();
            Log.Write("Initializing Script Engine for Script Compilation...");
            scriptEngine.Initialize();

            GameObject obj = scriptEngine.GetObjectOf("Game");
            Console.WriteLine(Log.GetMessages());
            Log.FlushMessages();

            if (obj == null)
            {
                Log.Write("Setting up the Default Engine Game Manager...");
                game = new Game();
                obj = new GameObject(game, "Game");
                scriptEngine = new ScriptEngine((Game)obj.Instance, ScriptEngine.ScriptTypes.Assembly);
            }
            else
            {
                Log.Write("Setting up " + obj.GetProperty().GameTitle + " Manager...");
                game = (Game)obj.Instance;
                scriptEngine = new ScriptEngine(game, ScriptEngine.ScriptTypes.Assembly);
            }
            scriptEngine.ScriptPath = FileManager.GetDataPath(SaveDataTypes.Root);
            //Force TCP
            game.ServerType = ProtocolType.Tcp;

            //Setup the scripting engine and load our script library
            //MUST be called before game.Start()
            //scriptEngine.Initialize();
            //game.scriptEngine = scriptEngine; //Pass this script engine off to the game to use now.
            Log.Write("Starting " + obj.GetProperty().GameTitle + "...");
            Console.WriteLine(Log.GetMessages());
            Log.FlushMessages();

            game.Start();

            while (game.IsRunning)
            {
                Console.Write(Log.GetMessages());
                Log.FlushMessages();
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}

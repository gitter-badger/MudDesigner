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
            ScriptEngine scriptEngine;
            Game game;

            //Re-create the settings file if it is missing
            if (!File.Exists("Settings.ini"))
            {
                Log.Write("Settings.ini missing!");
                FileManager.WriteLine("Settings.ini", "Scripts", "ScriptPath");
                FileManager.WriteLine("Settings.ini", ".cs", "ScriptExtension");
            }

            scriptEngine = new ScriptEngine(new Game(), ScriptEngine.ScriptTypes.SourceFiles);
            scriptEngine.ScriptPath = FileManager.GetData("Settings.ini", "ScriptPath");
            scriptEngine.ScriptExtension = FileManager.GetData("Settings.ini", "ScriptExtension");

            //scriptEngine.CompileScripts();

            scriptEngine.Initialize();

            GameObject obj = scriptEngine.GetObjectOf("Game");
            Log.Write(Log.GetMessages());
            Log.FlushMessages();

            if (obj == null)
            {
                Log.Write("Setting up the Engine Game Manager...");
                game = new Game();
                obj = new GameObject(game, "Game");
                scriptEngine = new ScriptEngine((Game)obj.Instance, ScriptEngine.ScriptTypes.Assembly);
            }
            else
            {
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

            game.Start();

            while (game.IsRunning)
            {
                Console.Write(Log.GetMessages());
                Log.FlushMessages();
                System.Threading.Thread.Sleep(1);
            }
                

            /*
            Game game = new Game();
            Zeroth realm = new Zeroth(game);
            
            realm.BuildZeroth();

            //BaseCharacter serverAdmin = new BaseCharacter(game);
            
            //Setup the game
            game.AutoSave = true;
            game.BaseCurrencyName = "Copper";
            game.BaseCurrencyAmount = 1;
            game.CompanyName = "Mud Designer";
            game.DayLength = 60 * 8;
            game.GameTitle = "Test Mud Project";
            game.HideRoomNames = false;
            game.PreCacheObjects = true;
            game.ProjectPath = FileManager.GetDataPath(SaveDataTypes.Root);
            game.TimeOfDay = Game.TimeOfDayOptions.Transition;
            game.TimeOfDayTransition = 30;
            game.Version = "0.1";
            game.Website = "http://MudDesigner.Codeplex.com";
            game.ServerType = ProtocolType.Tcp;
            game.ServerPort = 555;
            game.MaximumPlayers = 1000;
            Game.IsDebug = false;

            game.Start();
            
            while (game.IsRunning)
            {
                Console.Write(Log.GetMessages());
                Log.FlushMessages();
                System.Threading.Thread.Sleep(1);
            */
        }
    }
}

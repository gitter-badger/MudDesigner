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

namespace MudGame
{
    static class Program
    {
        const String SettingsFile = "Settings.ini";
        static Game game;

        static void Main(String[] args)
        {
            //Re-create the settings file if it is missing. Don't push any log messages until we know that this is
            //verbose or not
            Log.Write("Loading Settings...", false);
            if (!File.Exists(SettingsFile))
            {
                Log.Write("Settings.ini missing!", false);
                FileManager.WriteLine(SettingsFile, "Scripts", "ScriptPath");
                FileManager.WriteLine(SettingsFile, ".cs", "ScriptExtension");
                FileManager.WriteLine(SettingsFile, "True", "ServerEnabled");
                Log.Write("Settings.ini re-created with default values", false);
            }

            if (FileManager.GetData(SettingsFile, "ServerEnabled").ToLower() == "false")
                Log.IsVerbose = true;
            else if (FileManager.GetData(SettingsFile, "ServerEnabled").ToLower() == "")
                Log.IsVerbose = false;
            else
                Log.IsVerbose = false;

            //Get are cached log messages and go forward from here.
            Console.Write(Log.GetMessages());
            Log.FlushMessages();

            Log.Write("Launching...", true);
            ScriptEngine scriptEngine;
            scriptEngine = new ScriptEngine(new Game(), ScriptEngine.ScriptTypes.Both);

            //scriptEngine.CompileScripts();
            Log.Write("Initializing Script Engine for Script Compilation...", true);
            scriptEngine.Initialize();

            GameObject obj = scriptEngine.GetObjectOf("Game");
            //Console.WriteLine(Log.GetMessages());
            //Log.FlushMessages();

            if (obj == null)
            {
                game = new Game();
                obj = new GameObject(game, "Game");
                scriptEngine = new ScriptEngine((Game)obj.Instance, ScriptEngine.ScriptTypes.Both);
            }
            else
            {
                game = (Game)obj.Instance;
                scriptEngine = new ScriptEngine(game, ScriptEngine.ScriptTypes.Both);
            }
            //Force TCP
            game.ServerType = ProtocolType.Tcp;

            //Setup the scripting engine and load our script library
            //MUST be called before game.Start()
            //scriptEngine.Initialize();
            //game.scriptEngine = scriptEngine; //Pass this script engine off to the game to use now.
            Log.Write("");
            Log.Write("Starting " + obj.GetProperty().GameTitle + "...", true);
            Log.Write("");
            //Console.WriteLine(Log.GetMessages());
            //Log.FlushMessages();

            //Server is only enabled if the option is in the settings file
            //Allows developers to remove the option from the settings file and letting
            //people host multiplayer games with the singleplayer MUD.
            //People won't know that it's an option if the option doesn't exist so if no 
            //option is found in the sttings file, then we assume offline play.
            if (FileManager.GetData(SettingsFile, "ServerEnabled").ToLower() == "false")
                game.IsMultiplayer = false;
            else if (FileManager.GetData(SettingsFile, "ServerEnabled").ToLower() == "")
                game.IsMultiplayer = false;
            else
                game.IsMultiplayer = true;

            game.Start();

            //Make sure the Game is in fact running.
            if (!game.IsRunning)
            {
                Log.Write("Error starting game!\nReview Log file for details.", true);
                return;
            }

            //If the game isn't in multiplayer mode, then the server doesn't create an instance of the players
            //We need to make sure that the Game created one. The default game handles this, but inherited Game
            //scripts might miss this, so we check for it.
            if (!game.IsMultiplayer)
            {
                if ((game.PlayerCollection[0] == null) || (game.PlayerCollection[0].Name == "New BaseCharacter"))
                {
                    Log.Write("Error! No player available for creation!", true);
                    return;
                }
            }

            Console.Title = game.GameTitle;

            if (game.IsMultiplayer)
                Console.Title += " server running.";

            List<char> buf = new List<char>();

            try
            {
                while (game.IsRunning)
                {
                    game.Update();
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                Log.Write("Critical Error! " + ex.Message);
            }
            //The Game should save itself during shutdown, requiring to save it on the runtime end will
            //present possible issues with 3rd party runtimes not saving if they don't know better to do so.
            //game.Save();
        }
    }
}
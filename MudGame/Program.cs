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

        static void Main(String[] args)
        {
            Game game = new Game();

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

            //Search for a custom Game Type before we launch our game.
            //Compile the scripts
            rScripting.CompileEngine compiler = new rScripting.CompileEngine(".cs");
            compiler.Compiler = "MudScriptCompiler";
            if (!compiler.Compile("Scripts"))
            {
                Log.Write("Failed compiling script files.");
                Log.Write(compiler.Errors);
            }

            //If there were errors during compilation, then skip the custom scripts and use the default Game Type.
            if (!compiler.HasErrors)
            {
                //Search the scripts for a Type inheriting from Game
                rScripting.LateBinding.ScriptFactory factory = new rScripting.LateBinding.ScriptFactory(compiler.CompiledAssembly);
                foreach (Type t in compiler.CompiledAssembly.GetTypes())
                {
                    if (t.BaseType.Name == "Game")
                    {
                        rScripting.LateBinding.ScriptObject obj = factory.GetScript(t.Name);
                        game = (Game)obj.Instance;
                        break;
                    }
                }
            }

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

            //Start the game.
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
                if ((game.GetPlayerCollection()[0] == null) || (game.GetPlayerCollection()[0].Name == "New BaseCharacter"))
                {
                    Log.Write("Error! No player available for creation!", true);
                    return;
                }
            }

            Console.Title = game.GameTitle;

            if (game.IsMultiplayer)
                Console.Title += " server running.";

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
        }
    }
}
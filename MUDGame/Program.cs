using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using MUDGame.Environments;

namespace MUDGame
{
    class Program
    {
        //Setup our Fields
        static MudEngine.GameManagement.Game game;
        static MudEngine.GameManagement.CommandEngine commands;
        static MudEngine.GameObjects.Characters.BaseCharacter player;

        static List<MudEngine.GameObjects.Environment.Realm> realmCollection;

        static void Main(string[] args)
        {
            //Initialize them
            game = new MudEngine.GameManagement.Game();
            commands = new MudEngine.GameManagement.CommandEngine();
            realmCollection = new List<MudEngine.GameObjects.Environment.Realm>();
            
            //Setup the game
            game.AutoSave = true;
            game.BaseCurrencyName = "Copper";
            game.BaseCurrencyAmount = 1;
            game.CompanyName = "Mud Designer";
            game.DayLength = 60 * 8;
            game.GameTitle = "Test Mud Project";
            game.HideRoomNames = false;
            game.PreCacheObjects = true;
            game.ProjectPath = MudEngine.FileSystem.FileManager.GetDataPath(MudEngine.FileSystem.SaveDataTypes.Root);
            game.TimeOfDay = MudEngine.GameManagement.Game.TimeOfDayOptions.Transition;
            game.TimeOfDayTransition = 30;
            game.Version = "0.1";
            game.Website = "http://MudDesigner.Codeplex.com";
            game.ServerType = ProtocolType.Tcp;
            game.ServerPort = 555;
            game.MaximumPlayers = 1000;

            //Create the world
            BuildRealms();

            //Load all of the available commands from the engine
            MudEngine.GameManagement.CommandEngine.LoadAllCommands();

            //Player must be instanced AFTER BuildRealms as it needs Game.InitialRealm.InitialZone.InitialRoom
            //property so that it can set it's starting room correctly.
            player = new MudEngine.GameObjects.Characters.BaseCharacter(game);

            // Start the game & server
            if (!game.Start())
                Console.WriteLine("Error starting game!\nReview Log file for details.");

            game.IsRunning = true;

            game.PlayerCollection.Add(player);

            while (game.IsRunning)
            {
                Console.Write("Command: ");
                string command = Console.ReadLine();

                //TODO: Does the CommandResult really need to return an array of Objects?
                //All object management should be dealt with by the Game and Player so this should just be an array of strings.
                object[] result = player.ExecuteCommand(command).Result;
                
                //Search through each object in the array returned to us from the command execution and print the strings.
                foreach (object o in result)
                {
                    if (o is string)
                        Console.WriteLine(o.ToString());
                }
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadKey();
        }

        static private void BuildRealms()
        {
            Zeroth zeroth = new Zeroth(game);
            zeroth.BuildZeroth();
        }
    }
}

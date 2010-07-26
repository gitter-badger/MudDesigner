using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using MUDGame.Environments;
using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;
using MudEngine.FileSystem;

namespace MUDGame
{
    class Program
    {
        //Setup our Fields
        static Game game;
        static BaseCharacter player;

        static void Main(string[] args)
        {
            //Initialize them
            game = new Game();
            
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

            //Create the world
            BuildRealms();

            // Start the game & server.
            game.Start();
            
            if (!game.IsRunning)
                Console.WriteLine("Error starting game!\nReview Log file for details.");

            //Player must be instanced AFTER BuildRealms as it needs Game.InitialRealm.InitialZone.InitialRoom
            //property so that it can set it's starting room correctly.
            player = new BaseCharacter(game);
            //Add the player to the game.
            //Note once the server is fully implemented the player will be generated automatically by Game.
            game.PlayerCollection.Add(player);
            
            //Send game info to player
            Console.WriteLine(game.GameTitle);
            Console.WriteLine(game.Version);
            Console.WriteLine(game.Website);
            Console.WriteLine(game.Story);
            Console.WriteLine();

            //Simple Help info
            Console.WriteLine("Available Commands are\n  Look\n  Exit\n  Walk 'direction' where direction = north/south/east/west/up/down\n");

            //Invoke the Look command so the player knows whats around him/her
            Console.WriteLine(player.ExecuteCommand("Look"));

            while (game.IsRunning)
            {
                Console.Write("Command: ");
                Console.WriteLine(player.ExecuteCommand(Console.ReadLine()));
            }

            // - Exit command handles this now - game.Shutdown();
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

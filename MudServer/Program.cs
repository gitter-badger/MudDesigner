using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

using MudEngine.FileSystem;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Characters;
using MUDGame; //Pulling this from the example game, no sense re-writing what already exists.

namespace MudServer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Zeroth realm = new Zeroth(game);
            
            realm.BuildZeroth();

            BaseCharacter serverAdmin = new BaseCharacter(game);
            
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
            Game.IsDebug = true;

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

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;

using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.GameScripts;
using MudEngine.Core;
using MudEngine.DAL;

namespace WinPC_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup the engines log system
            Logger.LogFilename = "StandardGame.Log";
            Logger.Enabled = true;
            Logger.ConsoleOutPut = true;

            //Instance and setup our game
            StandardGame game = new StandardGame("Sample Game");
            game.AutoSave = true;
            game.Debugging = true;
            game.Description = "This is a very simple game that was created to demonstrate MUD game creation with the Mud Designer Game Engine.";
            game.HiddenRoomNames = false;
            game.Multiplayer = true;
            game.Server.MOTD = "Welcome to the Sample Game demonstration server!  This is the Servers MOTD!";
            game.Version = "1.0";
            game.Website = "http://muddesigner.codeplex.com";
            game.Server.ServerOwner = "Akiyuki";

            //Start the game and server.
            game.Start(100, 20);

            //Setup our Server console input class
            /*
            ConsoleInput input = new ConsoleInput();

            //Run the console input on its own thread.
            Thread inputThread = new Thread(input.GetInput);
            inputThread.Start();
            */

            //Game loops until it is disabled.
            while (game.Enabled)
            {
                //Only Game World components are updated.
                //All Players are updated independently of this
                //on their own Threads.
                game.Update();
            }

            //Kill the Console Input thread.
            /*
            inputThread.Abort();
            inputThread = null;
            input = null;
            */
            game = null;

            System.Windows.Forms.Application.Exit();
        }
    }
}
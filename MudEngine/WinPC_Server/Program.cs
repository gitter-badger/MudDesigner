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
            StandardGame game = new StandardGame("Sample Mud Game"); 
            StandardGame g = game.Initialize();

            //Check if 'g' is null.  If it's not, then we have a custom game
            //that was found and we will use that.  Otherwise use the default.
            if (g != null)
                game = g;
            
            //Start the game and server.
            game.Start(100, 20);

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
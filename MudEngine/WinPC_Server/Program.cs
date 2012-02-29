using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MudEngine.Game;
using MudEngine.Core;

namespace SimpleMUD
{
    public class ServerInput
    {
        public String Message;

        public ServerInput()
        {
            Message = String.Empty;
        }

        public void GetInput()
        {
            while (true)
            {
                Console.WriteLine("Enter a test message: ");
                Message = Console.ReadLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Logger.LogFilename = "StandardGame.Log";
            Logger.Enabled = true;
            Logger.ConsoleOutPut = true;

            StandardGame game = new StandardGame("Sample Game");
            game.Start();

            Boolean msgReturn = false;
            ServerInput input = new ServerInput();

            Thread inputThread = new Thread(input.GetInput);
            inputThread.Start();

            while (game.Enabled)
            {
                if (input.Message.Equals("exit"))
                {
                    game.Stop();
                }
                else if (input.Message.Equals("test") && (msgReturn == false))
                {
                    Console.WriteLine("Message Works");
                    input.Message = String.Empty;
                }
            }

            inputThread.Abort();
        }
    }
}
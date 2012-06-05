using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPC.Engine;
using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Core;
using WinPC.Engine.Networking;

namespace WinPC.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup the engines log system
            Logger.LogFilename = "StandardGame.Log";
            Logger.Enabled = true;
            Logger.ConsoleOutPut = true;
            Logger.ClearLog(); //Delete previous file.
            Logger.WriteLine("Server app starting...");

            IServer server = new Engine.Networking.Server(4000);

            server.Start(100,20);

            while (server.Enabled)
            {
                
            }

            server = null;
            //System.Windows.Forms.Application.Exit();


        }
    }
}

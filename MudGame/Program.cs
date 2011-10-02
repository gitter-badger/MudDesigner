using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MudGame
{
    static class Program
    {
        static void Main(String[] args)
        {
            MudGame game = new MudGame();
            game.Initialize();

            while (game.IsRunning)
            {
                game.Update();
            }
        }
    }
}
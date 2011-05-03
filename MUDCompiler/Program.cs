using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Scripting;

namespace MUDCompiler
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("===========================");
            Console.WriteLine("MUD Engine Content Compiler");
            Console.WriteLine("Version 0.1");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("1): Compile Scripts");
            Console.WriteLine("2): Exit Compiler");
            //Console.Write("Enter Selection: ");
            Console.WriteLine("Out of engine compiling is currently not supported.");

            String command = Console.ReadLine();

            //command error checking.
            if (String.IsNullOrEmpty(command))
            {
                Console.WriteLine("Invalid Command!");
                System.Threading.Thread.Sleep(1000); //wait before shutting down so user sees invalid command
            }
            else if (Convert.ToInt16(command) >= 3)
            {
                Console.WriteLine("Invalid Command!");
                System.Threading.Thread.Sleep(1000); //wait before shutting down so user sees invalid command
            }

            switch (Convert.ToInt16(command))
            {
                case 1:
                    CompileScripts();
                    break;
                case 2:
                    return;
            }
        }

        static void CompileScripts()
        {
        }
    }
}

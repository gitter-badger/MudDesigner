using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================");
            Console.WriteLine("MUD Engine Content Compiler");
            Console.WriteLine("Version 0.1");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("1): Compile Scripts");
            Console.WriteLine("2): Exit Compiler");
            Console.Write("Enter Selection: ");

            string command = Console.ReadLine();

            if (String.IsNullOrEmpty(command))
            {
                Console.WriteLine("Invalid Command!");
                System.Threading.Thread.Sleep(1000); //wait before shutting down so user sees invalid command
            }
        }
    }
}

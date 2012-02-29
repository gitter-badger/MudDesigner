using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinPC_Server
{
    class ConsoleInput
    {
        /// <summary>
        /// Current queued console message entered by the user
        /// </summary>
        public String Message { get; set; }

        public ConsoleInput()
        {
            Message = String.Empty;
        }

        /// <summary>
        /// Retrieves input from the user and queues it in ConsoleInput.Message
        /// </summary>
        public void GetInput()
        {
            while (true)
            {
                Console.WriteLine("Enter a Server Command: ");
                //Accept input from the user and store it for use.
                Message = Console.ReadLine();
            }
        }
    }
}

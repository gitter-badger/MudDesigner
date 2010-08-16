using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

// README: This is just a bare down tcp client. It'll work much better with the GUI, also in the receive thread, instead
//  of receiving one byte at a time your going to want to do what I did with the server and receive the msgs then check them
//  and act like a telnet client would for example the clear function.
// Lastly for some reason I can't relog into my account it could possibly be because with client's Send, it'll add \n, you may
//  need to add \r\n or \n\r I'm not sure which order.
// Any questions feel free to msg me on msn: u8sand@verizon.net or e-mail me u8sand@gmail.com

namespace MudClient
{
    class Program
    {
        static MudClient.Networking.Client client = new MudClient.Networking.Client();
        static Boolean quit = false;

        static void ReceiveThread()
        {
            string output;
            while (!quit)
            {
                if (!client.Receive(out output, 1))
                    quit = true;
                else
                    Console.Write(output);
            }
        }
        static void Main(string[] args)
        {
            
            string input,ip;
            int port;
            do
            {
                Console.Clear();
                Console.Write("IP: ");
                ip = Console.ReadLine();
                Console.Write("Port: ");
                input = Console.ReadLine();
                int.TryParse(input, out port);
            } while (!client.Initialize(ip, port));
            if (!client.Connect() || !client.Send("666", false)) // test send + client data
                Console.WriteLine("Failed to connect to server.");
            else
            {
                Thread r = new Thread(ReceiveThread);
                r.Start();
                while (!quit)
                {
                    input = Console.ReadLine();
                    if (!client.Send(input, true))
                        quit = true;
                }
                r.Abort();
            }
        }
    }
}

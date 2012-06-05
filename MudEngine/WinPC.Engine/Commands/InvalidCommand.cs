using System;
using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Commands
{
    public class InvalidCommand : ICommand
    {
        private Socket Connection { get; set; }
        public InvalidCommand(Socket connnection)
        {
            Connection = connnection;
        }

        public void Execute()
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            Connection.Send(encoding.GetBytes("Invalid Command!" + "\n\r"));
            
        }
         
    }
}
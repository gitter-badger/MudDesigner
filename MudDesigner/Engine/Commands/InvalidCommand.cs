using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Commands
{
    [HelpAttribute("Invalid Command is a result of entering a command that the game does not recognize.")]
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
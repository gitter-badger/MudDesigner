using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Commands
{
    public class LoginFailureCommand : ICommand
    {
        private Socket Connection { get; set; }


        public LoginFailureCommand(Socket connnection)
        {
            Connection = connnection;
        }
        public void Execute()
        {
            var encoding = new ASCIIEncoding();
            Connection.Send(encoding.GetBytes("Login Failure! <@This is a todo item>" + "\n\r"));
        }
    }
}
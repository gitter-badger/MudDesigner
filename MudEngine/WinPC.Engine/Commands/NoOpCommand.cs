using System.Net.Sockets;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Commands
{
    public class NoOpCommand : ICommand
    {
        private Socket Connection { get; set; }

        public NoOpCommand(Socket connection)
        {
            Connection = connection;
        }
        public void Execute()
        {
            // We are doing nothing on purpose.
            // This is a No operation command, aka do nothing.
            // good for silently changing states or modes.
        }
    }
}
using System.Net.Sockets;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Commands
{
    public class NoOpCommand : ICommand
    {
        public void Execute()
        {
            // We are doing nothing on purpose.
            // This is a No operation command, aka do nothing.
            // good for silently changing states or modes.
        }
    }
}
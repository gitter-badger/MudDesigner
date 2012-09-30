using System.Net.Sockets;

namespace MudDesigner.Engine.Commands
{
    public interface ICommand
    {
        void Execute();
    }
}
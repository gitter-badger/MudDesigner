using System.Net.Sockets;

namespace MudDesigner.Engine.Core
{
    public interface ICommand
    {
        void Execute();
    }
}
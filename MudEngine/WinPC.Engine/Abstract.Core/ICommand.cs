using System.Net.Sockets;

namespace MudDesigner.Engine.Abstract.Core
{
    public interface ICommand
    {
        void Execute();
    }
}
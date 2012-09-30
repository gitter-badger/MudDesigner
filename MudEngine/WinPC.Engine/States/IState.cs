using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Core;
namespace MudDesigner.Engine.States
{
    public interface IState
    {
        void Render(IPlayer player);
        ICommand GetCommand();
    }
}
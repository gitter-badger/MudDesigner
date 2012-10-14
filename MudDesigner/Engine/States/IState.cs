using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;
namespace MudDesigner.Engine.States
{
    public interface IState
    {
        void Render(IPlayer player);
        ICommand GetCommand();
    }
}
namespace MudDesigner.Engine.Core
{
    public interface IState
    {
        void Render(IPlayer player);
        ICommand GetCommand();
    }
}
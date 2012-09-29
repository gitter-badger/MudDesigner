namespace MudDesigner.Engine.Abstract.Core
{
    public interface IState
    {
        void Render(IPlayer player);
        ICommand GetCommand();
    }
}
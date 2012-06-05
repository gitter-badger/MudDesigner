namespace WinPC.Engine.Abstract.Core
{
    public interface IState
    {
        void Render(int index);
        ICommand GetCommand();

    }
}
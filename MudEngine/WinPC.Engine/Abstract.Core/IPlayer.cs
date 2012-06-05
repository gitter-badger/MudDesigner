namespace WinPC.Engine.Abstract.Core
{
    public interface IPlayer
    {
        IState CurrentState { get; }

        void Disconnect();
    }
}
namespace WinPC.Engine.Abstract.Core
{
    public interface IPlayer
    {
        IState CurrentState { get; }
        string Name { get; set; }
        void Disconnect();
    }
}
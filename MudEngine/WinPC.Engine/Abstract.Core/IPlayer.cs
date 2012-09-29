using System.Net.Sockets;
using System.Collections.Generic;

namespace WinPC.Engine.Abstract.Core
{
    public interface IPlayer
    {
        IState CurrentState { get; }
        Socket Connection { get; }
        List<byte> Buffer { get; set; }
        bool IsConnected { get; }

        string Name { get; set; }

        void Initialize(IState initialState, Socket connection);
        void Disconnect();
        void SwitchState(IState state);
        void SendMessage(string message, bool newLine = true);
    }
}
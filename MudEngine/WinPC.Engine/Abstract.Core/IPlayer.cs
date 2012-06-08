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
        void Disconnect();
        void SwitchState(IState state);
    }
}
using System.Net.Sockets;
using System.Collections.Generic;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Mobs
{
    public interface IPlayer : IGameObject
    {
        IState CurrentState { get; }
        Socket Connection { get; }
        bool IsConnected { get; }
        List<byte> Buffer { get; set; }

        void Initialize(IState initialState, Socket connection);
        void Disconnect();
        void SwitchState(IState state);
        void SendMessage(string message, bool newLine = true);
        void Move(Room room);

        void OnLevel(IPlayer player);
        void OnLogin();
    }
}
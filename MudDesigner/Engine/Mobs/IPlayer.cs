using System.Net.Sockets;
using System.Collections.Generic;

using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Mobs
{
    public interface IPlayer : IMob
    {
        string Username { get; set; }
        string Password {get; set;}
        IState CurrentState { get; }
        
        Socket Connection { get; }
        bool IsConnected { get; }
        List<byte> Buffer { get; set; }

        void Initialize(IState initialState, Socket connection);
        void Connect(IState initialState);
        void Disconnect();

        //QUESTION - Move into IMob for NPC and Monster use? - JS
        void SwitchState(IState state);
        
        void OnConnect(IState initialState);
        void OnDisconnect();
        void OnLevel(IPlayer player);
    }
}
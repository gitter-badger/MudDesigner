using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.States;

namespace MudDesigner.Engine.Directors
{
    public interface IServerDirector
    {
        Dictionary<IPlayer, Thread> ConnectedPlayers { get; }
        IState InitialConnectionState { get; }

        void AddConnection(Socket connection);
        void ReceiveDataThread(Object index);
        void DisconnectAll();


    }
}
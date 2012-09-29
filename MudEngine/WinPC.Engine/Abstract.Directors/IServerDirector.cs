using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Abstract.Directors
{
    public interface IServerDirector
    {
        Dictionary<IPlayer, Thread> ConnectedPlayers { get; }

        void AddConnection(Socket connection);
        void ReceiveDataThread(Object index);
        void DisconnectAll();


    }
}
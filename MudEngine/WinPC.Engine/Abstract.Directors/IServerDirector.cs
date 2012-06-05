using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace WinPC.Engine.Abstract.Directors
{
    public interface IServerDirector
    {

        List<Thread> ConnectionThreads { get; }

        void AddConnection(Socket connection);
        void ReceiveDataThread(Object index);
        void DisconnectAll();


    }
}
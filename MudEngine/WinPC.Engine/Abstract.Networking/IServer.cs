using System;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Abstract.Networking
{
    public enum ServerStatus
    {
        Stopped = 0,
        Starting = 1,
        Running = 2
    }


    public interface IServer
    {
        int Port { get; }
        int MaxConnections { get; }
        int MaxQueuedConnections { get; }
        bool Enabled { get; }
        ServerStatus Status { get; }

        string MOTD { get; }
        string ServerOwner { get; }

        IGame Game { get; }

        void Start(Int32 maxConnections, Int32 maxQueueSize);
        void Stop();
        void Running();
 
    }
}
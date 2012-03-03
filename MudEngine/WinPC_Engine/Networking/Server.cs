using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MudEngine.Core;
using MudEngine.Core.Interfaces;
using MudEngine.Game;
using MudEngine.Game.Characters;

namespace MudEngine.Networking
{
    public enum ServerStatus
    {
        Stopped = 0,
        Starting = 1,
        Running = 2,
    } 

    [Category("Networking")]
    public class Server
    {
        public ServerStatus Status { get; private set; }

        public Int32 Port { get; private set; }

        public Int32 MaxConnections { get; private set; }

        public Int32 MaxQueuedConnections { get; private set; }

        public ConnectionManager ConnectionManager { get; private set; }

        public Boolean Enabled { get; private set; }

        public String MOTD { get; set; }

        public Server(StandardGame game, Int32 port)
        {
            this.Port = port;
            this.Status = ServerStatus.Stopped;
            this.MaxConnections = 100;
            this.MaxQueuedConnections = 10;

            this._Game = game;
            this._Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(Int32 maxConnections, Int32 maxQueueSize)
        {
            if (this.Status != ServerStatus.Stopped)
                return;

            this.Status = ServerStatus.Starting;

            this.MaxConnections = maxConnections;
            this.ConnectionManager = new ConnectionManager(this.MaxConnections);

            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, this.Port);
                this._Server.Bind(ip);
                this._Server.Listen(this.MaxQueuedConnections);

                this.Status = ServerStatus.Running;
                this.Enabled = true;

                this._ServerThread = new Thread(ServerLoop);
                this._ServerThread.Start();
            }
            catch
            {
                this.Status = ServerStatus.Stopped;
            }
        }

        public void Stop()
        {
            this.ConnectionManager.DisconnectAll();
         
            this._ServerThread.Abort();

            this._Server.Close();
            this._Server = null;

            this.Enabled = false;
            this.Status = ServerStatus.Stopped;
        }

        private void ServerLoop()
        {
            while (this.Status == ServerStatus.Running)
            {
                this.ConnectionManager.AddConnection(this._Game, this._Server.Accept());
            }
        }

        private StandardGame _Game;
        private Socket _Server;
        private Thread _ServerThread;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

using MudEngine.Core;

namespace MudEngine.Communication
{
    public class Server : BaseServer
    {
        //Listens for incoming connections
        private TcpListener _Listener;

        //Dedicated thread for accepting and processing new connections
        private Thread _Connect;

        private Dictionary<BaseCharacter, Thread> _ClientCollection;

        /// <summary>
        /// Server constructor.  Requires a currently active game to start.
        /// </summary>
        /// <param name="game"></param>
        public Server(BaseGame game) : base(game)
        {
            this._Listener = new TcpListener(IPAddress.Any, 555);
            this._ClientCollection = new Dictionary<BaseCharacter, Thread>();
        }

        /// <summary>
        /// Server constructor.  Requires a currently active game and desired port number to start.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="port"></param>
        public Server(BaseGame game, int port)
            : base(game)
        {
            this._Listener = new TcpListener(IPAddress.Any, port);
            this._ClientCollection = new Dictionary<BaseCharacter, Thread>();
        }

        //Listens for incoming client connections.
        private void ListenForConnections()
        {
            this._Listener.Start();

            while (true)
            {
                TcpClient client = this._Listener.AcceptTcpClient();

                //Someone connects, send them to a new thread.
                Thread newClient = new Thread(new ParameterizedThreadStart(OnConnect));
                newClient.Start(client);
            }
        }

        public override void Initialize()
        {
            this._Connect = new Thread(new ThreadStart(ListenForConnections));
            this._Connect.Start();
        }

        public override void Shutdown()
        {
        }

        public override void OnConnect(object client)
        {
            TcpClient c = (TcpClient)client;
            NetworkStream ns = c.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            ns.Write(encoder.GetBytes("WELCOME!!!!"), 0, "WELCOME!!!!".Length);
            ns.Flush();
           // this.ActiveGame.OnConnect((TcpClient)client);
        }

        public override void OnDisconnect(object client)
        {
            throw new NotImplementedException();
        }

        public override void RecieveData(string data)
        {
            //TODO: command needs to be executed.
        }

        public override void SendData(string data)
        {
        }
    }
}

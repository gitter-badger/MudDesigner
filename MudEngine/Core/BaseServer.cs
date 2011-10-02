using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MudEngine.Core
{
    public abstract class BaseServer : BaseObject, ICommunicate
    {
        public int Port { get; set; }

        public ICommand LoginCommand { get; set; }

        public BaseGame ActiveGame { get; private set; }
        
        public BaseServer(BaseGame game)
        {
            this.ActiveGame = game;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }

        public abstract void OnConnect(object client);

        public abstract void OnDisconnect(object client);

        public abstract void RecieveData(string data);

        public abstract void SendData(string data);
    }
}

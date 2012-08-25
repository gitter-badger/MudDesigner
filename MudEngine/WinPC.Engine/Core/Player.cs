using System.Collections.Generic;
using System.Net.Sockets;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Core
{
    public class Player : EnginePlayer
    {
        public override void Initialize(IState initialState, Socket connection)
        {
            this.Connection = connection;
            initialState.Render(this);
        }
    }
}
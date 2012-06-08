using System.Collections.Generic;
using System.Net.Sockets;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Core
{
    public class Player : IPlayer
    {
        public Socket Connection { get; private set; }
        public IState CurrentState { get; private set; }

        public List<byte> Buffer { get; set; }

        public bool IsConnected
        {
            get
            {
                return Connection.Connected;
            }
        }

        public string Name { get; set; }

        public Player(IState initialState, Socket connection)
        {
            Connection = connection;
            CurrentState = initialState;
            Name = "Player";

            Buffer = new List<byte>();
        }

        public void Disconnect()
        {
            Connection.Close();
            Connection = null;
        }

        public void SwitchState(IState state)
        {
            CurrentState = state;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
using System.Collections.Generic;
using System.Net.Sockets;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Core
{
    public class Player : IPlayer
    {
        public Socket Connection { get; private set; }
        public IState CurrentState { get; private set; }

        public List<byte> buffer = new List<byte>();

        public string Name { get; set; }

        public Player(IState initialState, Socket connection)
        {
            Connection = connection;
            CurrentState = initialState;
            Name = "Player";
        }

        public void Disconnect()
        {
            Connection.Close();
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
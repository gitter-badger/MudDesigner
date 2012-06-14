using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace WinPC.Engine.Abstract.Core
{
    public abstract class EnginePlayer : IPlayer
    {
        public IState CurrentState { get; private set; }

        public Socket Connection { get; private set; }

        public List<byte> Buffer { get; set; }

        public bool IsConnected { get; }

        public string Name { get; set; }

        public void Initialize(IState initialState, Socket connection)
        {
            Connection = connection;
            CurrentState = initialState;
            Name = "Unnamed Player";

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
            return Name;
        }
    }
}

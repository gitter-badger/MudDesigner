using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Core
{
    public abstract class Player : BaseGameObject, IPlayer
    {
        public IState CurrentState { get; protected set; }
        public Socket Connection { get; private set; }
        public List<byte> Buffer { get; set; }
        public bool IsConnected { get; set; }

        private Guid Id { get; set; }
        //TODO: I will probably normalize this into a PlayerDetails class... - MC
        public string Username { get; set; }
        //TODO: I don't even think we need a Password property at all.  It can be stored in the saved file
        //and when the user enters the password via telnet, it checks against the saved file.
        //No need to store it at all inside of any Types - JS.
        public string Password { get; set; } // i dont like this but its just here temporarily - MC

        public string CharacterName { get; set; }
        public string Class { get; set; } // ... really need to code up a design spec - MC

        public Player()
        {
            Buffer = new List<byte>();
        }

        public virtual void Initialize(IState initialState, Socket connection)
        {
            this.Connection = connection;
            initialState.Render(this);
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

        public void SendMessage(string message, bool newLine = true)
        {
            if (newLine)
                message += System.Environment.NewLine;
            Connection.Send(new ASCIIEncoding().GetBytes(message));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
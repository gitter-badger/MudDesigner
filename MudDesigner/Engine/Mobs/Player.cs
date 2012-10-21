using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    public abstract class Player : BaseGameObject, IPlayer
    {
        public IState CurrentState { get; protected set; }
        public Socket Connection { get; private set; }
        public List<byte> Buffer { get; set; }

        public bool IsConnected
        {
            get
            {
                return Connection.Connected;
            }
        }

        private Guid Id { get; set; }
        //TODO: I will probably normalize this into a PlayerDetails class... - MC
        public string Username { get; set; }
        //TODO: I don't even think we need a Password property at all.  It can be stored in the saved file
        //and when the user enters the password via telnet, it checks against the saved file.
        //No need to store it at all inside of any Types - JS.
        public string Password { get; set; } // i dont like this but its just here temporarily - MC

        public string CharacterName { get; set; }

        public string Class { get; set; } // ... really need to code up a design spec - MC

        public Room CurrentRoom { get; protected set; }

        public Player()
        {
            Buffer = new List<byte>();

            OnLevelEvent += new OnLevelHandler(OnLevel);
            OnLoginEvent += new OnLoginHandler(OnLogin);
        }

        public virtual void Initialize(IState initialState, Socket connection)
        {
            this.Connection = connection;
            
            //Store reference to the initial state.
            CurrentState = initialState;

            //Call the login event.
            OnLoginEvent();

            //Render the state after the login event.  This allows scripts to replace the initialState
            //if they want with something custom and have the engine render it.
            CurrentState.Render(this);
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

        public void Move(Room room)
        {
            if (CurrentRoom == room)
                return;

            CurrentRoom = room;
            SendMessage(CurrentRoom.Description);
        }

        public override string ToString()
        {
            return Name;
        }

        public delegate void OnLevelHandler(IPlayer player);
        public event OnLevelHandler OnLevelEvent;
        public virtual void OnLevel(IPlayer player)
        {
        }

        public delegate void OnLoginHandler();
        public event OnLoginHandler OnLoginEvent;
        public virtual void OnLogin()
        {
        }
    }
}
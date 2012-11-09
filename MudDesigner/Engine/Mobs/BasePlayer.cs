using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

using Newtonsoft.Json;

namespace MudDesigner.Engine.Mobs
{
    public abstract class BasePlayer : GameObject, IPlayer
    {  
        public IGender Gender { get; set; }

        public IRace Race { get; set; }

        public IRoom Location { get; protected set; }

        public bool CanTalk { get; set; }

        public Dictionary<Guid, IInventory> Inventory { get; protected set; }

        //TODO - IPlayer.Username and Password need to be protected with a IPlayer.Validate(username, password) method. - JS
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore()]
        public IState CurrentState { get; protected set; }

        [JsonIgnore()]
        public Socket Connection { get; private set; }

        [JsonIgnore()]
        public bool IsConnected
        {
            get
            {
                if (Connection == null)
                    return false;
                else
                    return Connection.Connected;
            }
        }

        [JsonIgnore()]
        public List<byte> Buffer { get; set; }

        //TODO - Create IClass and IRace interfaces
        public string Class { get; set; }

        public BasePlayer()
        {
            Buffer = new List<byte>();
            CanTalk = true;
            Inventory = new Dictionary<Guid, IInventory>();

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

            if (CurrentState == null)
            {
                Logger.WriteLine("Failed to locate the current state for character '" + Name + "'", Logger.Importance.Critical);
            }
            else
                CurrentState.Render(this);
        }

        public String RecieveInput()
        {
            string input = String.Empty;

            while (true)
            {
                try
                {
                    byte[] buf = new byte[1];

                    if (!Connection.Connected)
                        return "Disconnected.";

                    Int32 recved = Connection.Receive(buf);

                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && Buffer.Count > 0)
                        {
                            if (Buffer[Buffer.Count - 1] == '\r')
                                Buffer.RemoveAt(Buffer.Count - 1);

                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                            input = enc.GetString(Buffer.ToArray());
                            Buffer.Clear();
                            //Return a trimmed string.
                            return input;
                        }
                        else
                            Buffer.Add(buf[0]);
                    }
                    else if (recved == 0) //Disconnected
                    {
                        //   ConnectedPlayers[index]. Connected = false;
                        //  this.LoggedIn = false;
                        return "Disconnected.";
                    }
                }
                catch (Exception e)
                {
                    //Flag as disabled 
                    //  this.Connected = false;
                    //  this.LoggedIn = false;
                    return e.Message;
                }
            }
        }

        public void SetPlayerCredentials(string password)
        {
            Password = password;
        }

        public void Disconnect()
        {
            if (IsConnected)
            { } //TODO - Move to OnDisconnect event.
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

        public void Move(IRoom room)
        {
            if (Location == room)
                return;

            Location = room;
            SendMessage(Location.Description);
        }

        public void Connect(IState initialState)
        {
            throw new NotImplementedException();
        }

        public void Create(string charName, IRoom location)
        {
            throw new NotImplementedException();
        }

        public void AddInventoryItem(IInventory inventoryItem)
        {
            throw new NotImplementedException();
        }

        public void UseInventoryItem(IInventory inventoryItem)
        {
            throw new NotImplementedException();
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

        public delegate void OnConnectHandler(IState initialState);
        public event OnConnectHandler OnConnectEvent;
        public void OnConnect(IState initialState)
        {
            throw new NotImplementedException();
        }

        public delegate void OnDisconnectHandler();
        public event OnDisconnectHandler OnDisconnectEvent;
        public void OnDisconnect()
        {
            throw new NotImplementedException();
        }

        public delegate void OnCreateHandler(string charName, IRoom location);
        public event OnCreateHandler OnCreateEvent;
        public void OnCreate(string charName, IRoom location)
        {
            throw new NotImplementedException();
        }

        public delegate void OnLeaveHandler(IRoom arrivalRoom, bool cancel = false);
        public event OnLeaveHandler OnLeaveEvent;
        public void OnLeave(IRoom arrivalRoom, bool cancel = false)
        {
            throw new NotImplementedException();
        }

        public delegate void OnEnterHandler(IRoom departingRoom);
        public event OnEnterHandler OnEnterEvent;
        public void OnEnter(IRoom departingRoom)
        {
            throw new NotImplementedException();
        }

        public delegate void OnAttackHandler(IMob[] target);
        public event OnAttackHandler OnAttackEvent;
        public void OnAttack(IMob[] target)
        {
            throw new NotImplementedException();
        }

        public delegate void OnDealDamageHandler(IMob[] target);
        public event OnDealDamageHandler OnDealDamageEvent;
        public void OnDealDamage(IMob[] target)
        {
            throw new NotImplementedException();
        }

        public delegate void OnRecieveDamageHandler(IGameObject target);
        public event OnRecieveDamageHandler OnRecieveDamageEvent;
        public void OnRecieveDamage(IGameObject target)
        {
            throw new NotImplementedException();
        }

        public delegate void OnDeathHandler(IGameObject target);
        public event OnDeathHandler OnDeathEvent;
        public void OnDeath(IGameObject target)
        {
            throw new NotImplementedException();
        }
    }
}
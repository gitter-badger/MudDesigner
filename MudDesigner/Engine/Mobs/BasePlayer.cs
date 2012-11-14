/* BasePlayer
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The Base class for all Players in the game world.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Objects;

//Newtonsoft JSon using statements
using Newtonsoft.Json;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// The Base class for all Players in the game world
    /// </summary>
    public abstract class BasePlayer : BaseMob, IPlayer
    {
        //TODO - IPlayer.Username and Password need to be protected with a IPlayer.Validate(username, password) method. - JS
        /// <summary>
        /// Gets or Sets the username for this player
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets the character role for this player in the world
        /// </summary>
        public CharacterRoles Role { get; private set; }

        /// <summary>
        /// Gets the current State that the player is in.
        /// </summary>
        [JsonIgnore()]
        public IState CurrentState { get; protected set; }

        /// <summary>
        /// Gets the players unlying network connection
        /// </summary>
        [JsonIgnore()]
        public Socket Connection { get; private set; }

        /// <summary>
        /// Gets the input that the player has entered via their client
        /// </summary>
        public string ReceivedInput { get; protected set; }

        /// <summary>
        /// Gets if the player is connected to the server or not.
        /// </summary>
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

        /// <summary>
        /// Gets or Sets the buffer used by the players network connection.
        /// </summary>
        [JsonIgnore()]
        public List<byte> Buffer { get; set; }

        private string Password { get; set; }

        private bool lastMessageHadNewLine = true;

        public BasePlayer()
        {
            Buffer = new List<byte>();
            CanTalk = true;

            OnLevelEvent += new OnLevelHandler(OnLevel);
            OnLoginEvent += new OnLoginHandler(OnLogin);
        }

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref dynamic copyTo)
        {
            if (copyTo is IPlayer)
            {
                Scripting.ScriptObject newObject = new Scripting.ScriptObject(copyTo);

                newObject.SetProperty("Username", Username, null);
                newObject.SetProperty("Password", Password, null);
                newObject.SetProperty("CurrentState", CurrentState, null);
                newObject.SetProperty("Connection", Connection, null);
                newObject.SetProperty("IsConnected", IsConnected, null);
                newObject.SetProperty("Buffer", Buffer, null);
            }

            base.CopyState(ref copyTo);
        }

        /// <summary>
        /// Initializes the player with a default state and provides its network connection for storage.
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="connection"></param>
        public virtual void Initialize(IState initialState, Socket connection, IServerDirector director)
        {
            this.Connection = connection;
            Director = director;

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

        /// <summary>
        /// Receives player input through the network
        /// </summary>
        /// <returns></returns>
        public String ReceiveInput()
        {
            //The input s tring
            string input = String.Empty;
            ReceivedInput = String.Empty;

            //This loop will forever run until we have received \n from the player
            while (true && Connection != null)
            {
                try
                {
                    byte[] buf = new byte[1];

                    //Make sure we are still connected
                    if (!Connection.Connected)
                        return "Disconnected.";

                    //Receive input from the socket connection
                    Int32 recved = Connection.Receive(buf);

                    //If we have received data, prep it for use
                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && Buffer.Count > 0)
                        {
                            if (Buffer[Buffer.Count - 1] == '\r')
                                Buffer.RemoveAt(Buffer.Count - 1);

                            //Format the input
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                            //Convert the bytes into a s tring
                            input = enc.GetString(Buffer.ToArray());

                            //Clear out our buffer
                            Buffer.Clear();

                            //Return a trimmed string.
                            ReceivedInput = input;
                            return input;
                        }
                        else
                            //otherwise keep adding the input to our bufer
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

            return "Disconnected.";
        }

        /// <summary>
        /// Sets the players credentials
        /// </summary>
        /// <param name="password">The password the player has provided.</param>
        public void SetPlayerCredentials(string userPassword)
        {
            //Only set them if the password is not empty.
            //To reset the password, we will require the user to enter the
            //original password using ChangePassword()
            if (String.IsNullOrEmpty(userPassword))
                Password = userPassword;
        }

        /// <summary>
        /// Allows the player to change their password.
        /// </summary>
        /// <param name="oldPassword">The old password that they used</param>
        /// <param name="newPassword">The new password that they want to use</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (Password == oldPassword)
            {
                Password = newPassword;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Disconnects the player from the server.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (IsConnected)
                { } //TODO - Move to OnDisconnect event.
                if (Connection != null)
                {
                    Connection.Close();
                    Connection = null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Switches the players State from one, to another
        /// </summary>
        /// <param name="state">The state to switch to.</param>
        public void SwitchState(IState state)
        {
            CurrentState = state;
        }

        /// <summary>
        /// Sends a message to the players terminal for reading.
        /// </summary>
        /// <param name="message">The message you want to send</param>
        /// <param name="newLine">If false, no no line will be printed and the next message will be printed on the same line.</param>
        public override void SendMessage(string message, bool newLine = true)
        {
            if (newLine && !lastMessageHadNewLine)
                message = message.Insert(0, System.Environment.NewLine);

            if (newLine)
            {
                message += System.Environment.NewLine;
                lastMessageHadNewLine = true;
            }
            else
                this.lastMessageHadNewLine = false;

            //Make sure we are still connected
            try
            {
                if (IsConnected)
                    Connection.Send(new ASCIIEncoding().GetBytes(message));
            }
            catch (Exception ex)
            {
                //No connection was made, so make sure we clean up
                if (!IsConnected)
                    Disconnect();
            }
        }

        /// <summary>
        /// Called when the player connects to the server.
        /// </summary>
        /// <param name="initialState"></param>
        public void Connect(IState initialState)
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
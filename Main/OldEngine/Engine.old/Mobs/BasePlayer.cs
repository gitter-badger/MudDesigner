//-----------------------------------------------------------------------
// <copyright file="BasePlayer.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Objects;
using Newtonsoft.Json;
using log4net;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// The Base class for all Players in the game world
    /// </summary>
    public abstract class BasePlayer : BaseMob, IPlayer
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BasePlayer)); 

        // TODO - IPlayer.Username and Password need to be protected with a IPlayer.Validate(username, password) method. - JS
        /// <summary>
        /// Gets or Sets the username for this player
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The Password is a hash generated. 
        /// </summary>
        [JsonProperty("Password")]
        protected byte[] Password { get; set; }

        /// <summary>
        /// The Salt is unique per password, per user. 
        /// Used for validating the Hash
        /// </summary>
        [JsonProperty("Salt")]
        protected byte[] Salt { get; set; }

        /// <summary>
        /// Gets the character role for this player in the world
        /// </summary>
        public CharacterRoles Role { get; protected set; }

        /// <summary>
        /// Gets the current State that the player is in.
        /// </summary>
        [JsonIgnore()]
        public IState CurrentState { get; protected set; }

        /// <summary>
        /// Gets the players unlying network connection
        /// </summary>
        [JsonIgnore()]
        [DisableStateCopy()]
        public Socket Connection { get; private set; }

        /// <summary>
        /// Gets the input that the player has entered via their client
        /// </summary>
        [JsonIgnore()]
        [DisableStateCopy()]
        public string ReceivedInput { get; protected set; }

        /// <summary>
        /// Gets if the player is connected to the server or not.
        /// </summary>
        [JsonIgnore()]
        [DisableStateCopy()]
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
        [DisableStateCopy()]
        public List<byte> Buffer { get; set; }

        /// <summary>
        /// The last message had new line
        /// </summary>
        private bool lastMessageHadNewLine = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlayer"/> class.
        /// </summary>
        public BasePlayer()
        {
            Buffer = new List<byte>();
            CanTalk = true;

            OnLevelEvent += new OnLevelHandler(OnLevel);
            OnLoginEvent += new OnLoginHandler(OnLogin);
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

            // Store reference to the initial state.
            CurrentState = initialState;

            // Call the login event.
            OnLoginEvent();

            // Render the state after the login event.  This allows scripts to replace the initialState
            // if they want with something custom and have the engine render it.

            if (CurrentState == null)
            {
                Log.Error(string.Format("Failed to locate the current state for character '{0}'", Name));
                
            }
            else
                CurrentState.Render(this);
        }

        /// <summary>
        /// Receives player input through the network
        /// </summary>
        /// <returns></returns>
        public string ReceiveInput()
        {
            // The input s tring
            string input = String.Empty;
            ReceivedInput = String.Empty;

            // This loop will forever run until we have received \n from the player
            while (true && Connection != null)
            {
                try
                {
                    byte[] buf = new byte[1];

                    // Make sure we are still connected
                    if (!Connection.Connected)
                        return "Disconnected.";

                    // Receive input from the socket connection
                    Int32 recved = Connection.Receive(buf);

                    // If we have received data, prep it for use
                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && Buffer.Count > 0)
                        {
                            if (Buffer[Buffer.Count - 1] == '\r')
                                Buffer.RemoveAt(Buffer.Count - 1);

                            // Format the input
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                            // Convert the bytes into a s tring
                            input = enc.GetString(Buffer.ToArray());

                            // Clear out our buffer
                            Buffer.Clear();

                            // Return a trimmed string.
                            ReceivedInput = input;
                            return input;
                        }
                        else
                            // otherwise keep adding the input to our bufer
                            Buffer.Add(buf[0]);
                    }
                    else if (recved == 0) // Disconnected
                    {
                        //   ConnectedPlayers[index]. Connected = false;
                        //  this.LoggedIn = false;
                        return "Disconnected.";
                    }
                }
                catch (Exception e)
                {
                    // Flag as disabled 
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
        /// <param name="userPassword">The password the player has provided.</param>
        public void SetPlayerCredentials(string userPassword)
        {
            if (userPassword == null)
            {
                Log.Error(string.Format("User password was null for player {0}!",Name));
                throw new ArgumentNullException("userPassword");
            }
            // Only set them if the password is not empty.
            // To reset the password, we will require the user to enter the
            // original password using ChangePassword()
            if (Password != null) 
                return;
            
            Salt = Crypt.GenerateSalt(userPassword.Length);
            Password = Crypt.GenerateSaltedHash(userPassword, Salt);
            
        }

        /// <summary>
        /// Allows the player to change their password.
        /// </summary>
        /// <param name="oldPassword">The old password that they used</param>
        /// <param name="newPassword">The new password that they want to use</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            // If the current hash password equals the oldpassword passed in. 
            if (Crypt.CrypCompare(Password, Crypt.GenerateSaltedHash(oldPassword, Salt)))
            {
                if (!String.IsNullOrEmpty(newPassword))
                {
                    Salt = Crypt.GenerateSalt(newPassword.Length);
                    Password = Crypt.GenerateSaltedHash(newPassword, Salt);
                    return true;
                }
                else
                {
                    SendMessage("The new password was blank, please re-enter.");
                    return false;
                }

            }
            else
            {
                SendMessage("Failed to update password, password entered doesn't match records.");
                return false;
            }
        }

        /// <summary>
        /// Used for Validating password for logins
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            return Crypt.CrypCompare(Password, Crypt.GenerateSaltedHash(password, Salt));
        }

        /// <summary>
        /// Disconnects the player from the server.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (IsConnected)
                { } // TODO - Move to OnDisconnect event.
                if (Connection != null)
                {
                    Connection.Close();
                    Connection = null;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Location.RemoveCharacter(this, AvailableTravelDirections.None);
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
        // When printing properties that don't have values, they'll
        // be null.
        if (message == null)
            return;

        if (newLine && !lastMessageHadNewLine)
            message = message.Insert(0, System.Environment.NewLine);

        if (newLine)
        {
            message += System.Environment.NewLine;
            lastMessageHadNewLine = true;
        }
        else
            this.lastMessageHadNewLine = false;

        // Make sure we are still connected
        try
        {
            if (IsConnected)
                Connection.Send(new ASCIIEncoding().GetBytes(message));
        }
        catch (Exception ex)
        {
            // No connection was made, so make sure we clean up
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">The player.</param>
        public delegate void OnLevelHandler(IPlayer player);

        /// <summary>
        /// Occurs when [on level event].
        /// </summary>
        public event OnLevelHandler OnLevelEvent;

        /// <summary>
        /// Called when [level].
        /// </summary>
        /// <param name="player">The player.</param>
        public virtual void OnLevel(IPlayer player)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void OnLoginHandler();

        /// <summary>
        /// Occurs when [on login event].
        /// </summary>
        public event OnLoginHandler OnLoginEvent;

        /// <summary>
        /// Called when [login].
        /// </summary>
        public virtual void OnLogin()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        public delegate void OnConnectHandler(IState initialState);

        /// <summary>
        /// Occurs when [on connect event].
        /// </summary>
        public event OnConnectHandler OnConnectEvent;

        /// <summary>
        /// Called when [connect].
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnConnect(IState initialState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void OnDisconnectHandler();

        /// <summary>
        /// Occurs when [on disconnect event].
        /// </summary>
        public event OnDisconnectHandler OnDisconnectEvent;

        /// <summary>
        /// Disconnects the player from the server.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnDisconnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charName">Name of the character.</param>
        /// <param name="location">The location.</param>
        public delegate void OnCreateHandler(string charName, IRoom location);

        /// <summary>
        /// Occurs when [on create event].
        /// </summary>
        public event OnCreateHandler OnCreateEvent;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="charName">Name of the character.</param>
        /// <param name="location">The location.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnCreate(string charName, IRoom location)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrivalRoom">The arrival room.</param>
        /// <param name="cancel">if set to <c>true</c> [cancel].</param>
        public delegate void OnLeaveHandler(IRoom arrivalRoom, bool cancel = false);

        /// <summary>
        /// Occurs when [on leave event].
        /// </summary>
        public event OnLeaveHandler OnLeaveEvent;

        /// <summary>
        /// Called when [leave].
        /// </summary>
        /// <param name="arrivalRoom">The arrival room.</param>
        /// <param name="cancel">if set to <c>true</c> [cancel].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnLeave(IRoom arrivalRoom, bool cancel = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departingRoom">The departing room.</param>
        public delegate void OnEnterHandler(IRoom departingRoom);

        /// <summary>
        /// Occurs when [on enter event].
        /// </summary>
        public event OnEnterHandler OnEnterEvent;

        /// <summary>
        /// Called when [enter].
        /// </summary>
        /// <param name="departingRoom">The departing room.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnEnter(IRoom departingRoom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">The target.</param>
        public delegate void OnAttackHandler(IMob[] target);

        /// <summary>
        /// Occurs when [on attack event].
        /// </summary>
        public event OnAttackHandler OnAttackEvent;

        /// <summary>
        /// Called when [attack].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnAttack(IMob[] target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">The target.</param>
        public delegate void OnDealDamageHandler(IMob[] target);

        /// <summary>
        /// Occurs when [on deal damage event].
        /// </summary>
        public event OnDealDamageHandler OnDealDamageEvent;

        /// <summary>
        /// Called when [deal damage].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnDealDamage(IMob[] target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">The target.</param>
        public delegate void OnRecieveDamageHandler(IGameObject target);

        /// <summary>
        /// Occurs when [on recieve damage event].
        /// </summary>
        public event OnRecieveDamageHandler OnRecieveDamageEvent;

        /// <summary>
        /// Called when [recieve damage].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnRecieveDamage(IGameObject target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">The target.</param>
        public delegate void OnDeathHandler(IGameObject target);

        /// <summary>
        /// Occurs when [on death event].
        /// </summary>
        public event OnDeathHandler OnDeathEvent;

        /// <summary>
        /// Called when [death].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnDeath(IGameObject target)
        {
            throw new NotImplementedException();
        }
    }
}
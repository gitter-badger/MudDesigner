using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;
using System.Text.RegularExpressions;

using MudEngine.GameScripts;
using MudEngine.Core.Interfaces;
using MudEngine.Networking;
using MudEngine.Core;

namespace MudEngine.Game.Characters
{
    /// <summary>
    /// Standard Character class used by all character based objects
    /// </summary>
    public class StandardCharacter : BaseScript, INetworked, ISavable, IGameComponent
    {
        /// <summary>
        /// Gets a reference to the currently active game.
        /// </summary>
        public StandardGame Game { get; private set; }

        public string Filename
        {
            get
            {
                return this.Game.SavePaths.GetFilePath(DAL.DataTypes.Players, this.Name);
            }
        }

        /// <summary>
        /// Gets what this Characters role on the server is.
        /// </summary>
        public CharacterRoles Role { get; protected set; }

        /// <summary>
        /// Gets what this characters stats are.
        /// </summary>
        public CharacterStats Stats { get; protected set; }

        //TODO: Should be Private/Protected?
        public String Password { get; set; }

        /// <summary>
        /// Flags this object as non-movable in the world.
        /// </summary>
        public Boolean Immovable { get; set; }

        /// <summary>
        /// Gets or Sets if this character is enabled.
        /// When disabled, they can not enter commands.
        /// Characters can have LoggedIn and Connected TRUE with Enabled false resulting in a player connected to
        /// the server but unable to interact.
        /// </summary>
        public Boolean Enabled { get; set; }

        /// <summary>
        /// Gets or Sets if this client is fully logged into the account and located in the World.
        /// </summary>
        public Boolean LoggedIn
        {
            get
            {
                return this._LoggedIn;
            }
            private set
            {
                if (value)
                    this.OnLoginEvent();
                this._LoggedIn = value;
            }
        }

        /// <summary>
        /// Gets if the user is currently connected to a server or not.
        /// </summary>
        public Boolean Connected { get; set; }

        //TODO: Add current location to characters
        //public IEnvironment CurrentLocation

        protected CommandSystem Commands { get; private set; }

        public StandardCharacter(StandardGame game, String name, String description)
            : base(game, name, description)
        {
            this.Game = game;

            this.Role = CharacterRoles.Player;

            //Instance this Characters personal Command System with a copy of the command
            //collection already loaded and prepared by the Active Game.
            this.Commands = new CommandSystem(CommandSystem.Commands);

            this.OnConnectEvent += new OnConnectHandler(OnConnect);
            this.OnDisconnectEvent += new OnDisconnectHandler(OnDisconnect);
            this.OnLoginEvent += new OnLoginHandler(OnLogin);
        }

        public StandardCharacter(StandardGame game, String name, String description, Socket connection)
            : this(game, name, description)
        {
            this._Connection = connection;

            this._Reader = new StreamReader(new NetworkStream(this._Connection, false));
            this._Writer = new StreamWriter(new NetworkStream(this._Connection, true));

            this._Writer.AutoFlush = true; //Flushes the stream automatically.
        }

        public void Initialize()
        {
            //throw new NotImplementedException();
            this.Enabled = true;
        }

        /// <summary>
        /// Destroys any resources used by this character.
        /// Assumes that Save() has already been invoked.
        /// </summary>
        public void Destroy()
        {
            this.Commands = null;
        }

        public override bool Save(String filename)
        {
            return this.Save(filename, true);
        }

        public override bool Save(String filename, Boolean verbose)
        {
            base.Save(filename, true);

            SaveData.AddSaveData("Immovable", Immovable.ToString());
            SaveData.AddSaveData("Password", Password);

            if (this.SaveData.Save(filename))
            {
                if (!verbose)
                    this.SendMessage("Save completed.");
            }

            return true;
        }

        public override void Load(string filename)
        {
            base.Load(filename);

            this.Immovable = Convert.ToBoolean(this.SaveData.GetData("Immovable"));
            this.Password = this.SaveData.GetData("Password");
        }

        /// <summary>
        /// Executes the specified command if it exists in the Command library.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Boolean ExecuteCommand(string command)
        {
            if (this.Enabled)
            {
                Boolean result = Commands.Execute(command, this);

                SendMessage("");
                SendMessage("Command:", false);

                return result;
            }
            else
                return false;
        }

        /// <summary>
        /// Executes the spcified command if it exists in the Command library.
        /// This method is verbose.  Only messages sent from the Commands will be presented to the player.
        /// The standard new line and "Command: " message is skipped with this method.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Boolean ExecuteSilentCommand(string command)
        {
            if (this.Enabled)
            {
                Boolean result = Commands.Execute(command, this);

                return result;
            }
            else
                return false;
        }

        public void Connect()
        {
            //this.Initialize();
            this.Connected = true;
            OnConnectEvent();
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnecting...");

            //Purge all of this characters commands.
            this.Destroy();

            //Close our currently open socket.
            this._Connection.Close();

            this.LoggedIn = false;

            this.OnDisconnectEvent();
        }

        public void SendMessage(String data)
        {
            this.SendMessage(data, true);
        }

        public void SendMessage(String data, Boolean newLine)
        {
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                if (newLine)
                    data += "\n\r";

                this._Connection.Send(encoding.GetBytes(data));
            }
            catch (Exception)
            {
                Disconnect();
            }
        }

        public String GetInput()
        {
            string input = String.Empty;

            while (true)
            {
                try
                {
                    byte[] buf = new byte[1];
                    Int32 recved = this._Connection.Receive(buf);

                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && buffer.Count > 0)
                        {
                            if (buffer[buffer.Count - 1] == '\r')
                                buffer.RemoveAt(buffer.Count - 1);

                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                            input = enc.GetString(buffer.ToArray());
                            buffer.Clear();
                            //Return a trimmed string.
                            return input;
                        }
                        else
                            buffer.Add(buf[0]);
                    }
                    else if (recved == 0) //Disconnected
                    {
                        this.Enabled = false;
                        return "Disconnected.";
                    }
                }
                catch (Exception e)
                {
                    //Flag as disabled 
                    this.Enabled = false;
                    return e.Message;
                }
            }
        }

        public void FlushBuffer()
        {
        }

        public delegate void OnConnectHandler();
        public event OnConnectHandler OnConnectEvent;
        public void OnConnect()
        {
            //Greet the user with the game information
            this.SendMessage(this.Game.Name);
            this.SendMessage(this.Game.Description);
            this.SendMessage(String.Empty);

            //Log the user in.
            Boolean result = this.ExecuteSilentCommand("Login");
            this.SendMessage(String.Empty);

            //Flags the character as fully logged in.
            //This will automatically invoke the OnLoggedIn method
            //which will present the user with the server MOTD
            this.LoggedIn = result;
            //Provide a blank line between the character loggin info
            //and the characters actual first in-game Command prompt.
            this.SendMessage(String.Empty);

            //Make sure we are logged in.
            if (!this.LoggedIn)
            {
                this.Disconnect();
            }

            this.ExecuteCommand("Look");
        }

        public delegate void OnDisconnectHandler();
        public event OnDisconnectHandler OnDisconnectEvent;
        public void OnDisconnect()
        {
            Console.WriteLine("Disconnect Complete.");
        }

        public delegate void OnLoginHandler();
        public event OnLoginHandler OnLoginEvent;
        public void OnLogin()
        {
            this.SendMessage(this.Game.Server.MOTD);

            //Check to see if this user is the servers owner.
            if (this.Name == this.Game.Server.ServerOwner)
            {
                //Set the role accordingly.
                this.Role = CharacterRoles.Admin;
            }
        }

        public void SetRole(StandardCharacter adminCharacter, StandardCharacter subordinateCharacter, CharacterRoles role)
        {
            //Check to make sure the admin character is truly a admin
            if (adminCharacter.Role == CharacterRoles.Admin)
            {
                subordinateCharacter.Role = role;
            }
            else
            {
                adminCharacter.SendMessage("You do not have the rights to perform this command.");
            }
        }

        private Socket _Connection;
        private StreamReader _Reader;
        private StreamWriter _Writer;
        private Boolean _LoggedIn;
        private List<byte> buffer = new List<byte>();
    }
}

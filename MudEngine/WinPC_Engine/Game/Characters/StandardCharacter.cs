using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MudEngine.GameScripts;
using MudEngine.Core.Interfaces;
using MudEngine.Networking;
using MudEngine.Core;

namespace MudEngine.Game.Characters
{
    /// <summary>
    /// Standard Character class used by all character based objects
    /// </summary>
    public class StandardCharacter : BaseScript, INetworked, ISavable, IUpdatable, IGameComponent
    {
        /// <summary>
        /// Gets a reference to the currently active game.
        /// </summary>
        public StandardGame Game { get; private set; }

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

        //TODO: Add current location to characters
        //public IEnvironment CurrentLocation

        protected CommandSystem Commands { get; private set; }

        public StandardCharacter(String name, String description, StandardGame game) : base(name, description)
        {
            this.Game = game;

            //Instance this Characters personal Command System with a copy of the command
            //collection already loaded and prepared by the Active Game.
            this.Commands = new CommandSystem(CommandSystem.Commands);

            this.OnConnectEvent += new OnConnectHandler(OnConnect);
        }

        public StandardCharacter(String name, String description, StandardGame game, Socket connection) : this(name, description, game)
        {
            this._Connection = connection;

            this._Reader = new StreamReader(new NetworkStream(this._Connection, false));
            this._Writer = new StreamWriter(new NetworkStream(this._Connection, true));

            this._Writer.AutoFlush = true; //Flushes the stream automatically.
            this._InitialMessage = true; //Strips Telnet client garbage text from initial message sent from client.
        }

        internal void ExecuteCommand(string command)
        {
            //Process commands here.
            if (this._InitialMessage)
            {
                command = this.CleanString(command);
                this._InitialMessage = false;
            }

            this.Commands.Execute(command, this);
        }

        public void SendMessage(string message)
        {
            lock (this)
            {
                _Writer.WriteLine(message);
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnecting...");

            //Close our currently open socket.
            this._Connection.Close();

            //Remove this character from the Connection Manager
            ConnectionManager.RemoveConnection(this);
            Console.WriteLine("Disconnect Complete.");

            //Stop the Update() thread.
            this._LoopThread.Abort();
        }

        public void Connect()
        {
            _LoopThread = new Thread(Update);
            _LoopThread.Start();

            this.SendMessage("");
            OnConnectEvent();
        }

        public void Update()
        {
            try
            {
                while (this.Game.Enabled)
                {
                    _Writer.Flush();
                    String line = _Reader.ReadLine();
                    ExecuteCommand(line);
                }
            }
            catch
            {
            }
            finally
            {
                this.Disconnect();
            }
        }

        String CleanString(string line)
        {
            if ((!String.IsNullOrEmpty(line)) && (line.Length > 0))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder(line.Length);
                foreach (char c in line)
                {
                    if (char.IsSymbol(c)) continue;

                    sb.Append(char.IsControl(c) ? ' ' : c);
                }
                String newLine = sb.ToString().Trim().Substring(2).Trim();
                return newLine;
            }
            else
                return String.Empty;
        }

        public delegate void OnConnectHandler();
        public event OnConnectHandler OnConnectEvent;
        public void OnConnect()
        {
            _Writer.WriteLine(this.Game.Server.MOTD);
        }

        private Socket _Connection;
        private Thread _LoopThread;
        private StreamReader _Reader;
        private StreamWriter _Writer;
        private Boolean _InitialMessage;

        public string Filename
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public void Load(string filename)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}

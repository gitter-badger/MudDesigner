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
    public class StandardCharacter : BaseScript, INetworked, ISavable, IUpdatable, IGameComponent
    {
        /// <summary>
        /// Gets a reference to the currently active game.
        /// </summary>
        public StandardGame Game { get; private set; }

        public string Filename { get; set; }

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

        public Boolean Enabled { get; set; }

        //TODO: Add current location to characters
        //public IEnvironment CurrentLocation

        protected CommandSystem Commands { get; private set; }

        public StandardCharacter(StandardGame game, String name, String description)
            : base(game, name, description)
        {
            this.Game = game;

            //Instance this Characters personal Command System with a copy of the command
            //collection already loaded and prepared by the Active Game.
            this.Commands = new CommandSystem(CommandSystem.Commands);

            this.OnConnectEvent += new OnConnectHandler(OnConnect);
        }

        public StandardCharacter(StandardGame game, String name, String description, Socket connection)
            : this(game, name, description)
        {
            this._Connection = connection;

            this._Reader = new StreamReader(new NetworkStream(this._Connection, false));
            this._Writer = new StreamWriter(new NetworkStream(this._Connection, true));

            this._Writer.AutoFlush = true; //Flushes the stream automatically.
            this._InitialMessage = true; //Strips Telnet client garbage text from initial message sent from client.
        }

        public override bool Save(String filename)
        {
            base.Save(filename, true);

            SaveData.AddSaveData("Immovable", Immovable.ToString());
            SaveData.AddSaveData("Password", Password);

            this.SaveData.Save(filename);

            return true;
        }


        public override void Load(string filename)
        {
            base.Load(filename);
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

        internal void ExecuteCommand(string command)
        {
            if (this.Enabled)
            {
                Commands.Execute(command, this);

                SendMessage("");
                SendMessage("Command:", false);
            }
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

        public void Disconnect()
        {
            Console.WriteLine("Disconnecting...");

            //Purge all of this characters commands.
            this.Destroy();

            //Close our currently open socket.
            this._Connection.Close();
            //this._LoopThread.Abort();
            //Remove this character from the Connection Manager
            //ConnectionManager.RemoveConnection(this, );
            Console.WriteLine("Disconnect Complete.");
        }

        public void Connect(Socket connection)
        {
            this._Connection = connection;

            OnConnectEvent();
        }

        public void Update()
        {
            try
            {
                while (this.Game.Enabled)
                {
                    _Writer.Flush();
                    //String line = CleanString(GetInput());
                    //Console.WriteLine(line);
                    //ExecuteCommand(line);
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

        String CleanString(String line)
        {
            /*
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
             * */

            Regex invalidChars = new Regex(
    @"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]",
    RegexOptions.Compiled);

            if (String.IsNullOrEmpty(line))
                return "";
            else
                return invalidChars.Replace(line, "");
        }

        public delegate void OnConnectHandler();
        public event OnConnectHandler OnConnectEvent;
        public void OnConnect()
        {
            this.SendMessage(this.Game.Server.MOTD);
        }

        public delegate void OnDisconnectHandler();
        public event OnDisconnectHandler OnDisconnectEvent;
        public void OnDisconnect()
        {

        }

        public delegate void OnLoginHandler();
        public event OnLoginHandler OnLoginEvent;
        public void OnLogin()
        {

        }

        private Socket _Connection;
        private StreamReader _Reader;
        private StreamWriter _Writer;
        private Boolean _InitialMessage;
        private List<byte> buffer = new List<byte>();
    }
}

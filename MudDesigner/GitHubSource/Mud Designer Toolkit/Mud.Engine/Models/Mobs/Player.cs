// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

// Mud Designer Framework
using Mud.Commands;
using Mud.Core;
using Mud.Networking;
using Mud.Models.Environment;
using Mud.Scripting;

namespace Mud.Models.Mobs
{
    public class Player : IPlayer, IConnectionState
    {
        private bool initialConnection;

        public Player()
        {
            this.BufferSize = 1024;
            this.Buffer = new byte[this.BufferSize];

            this.initialConnection = true;
        }

        // Events
        public event EventHandler OnConnect;

        // Properties
        public Socket Connection { get; set; }

        public byte[] Buffer { get; set; }
        public int BufferSize { get; set; }

        public string ReceivedInput { get; set; }

        public IGame Game { get; set; }

        public IRoom Location { get; set; }

        public IGender Gender { get; set; }

        public bool IsMute { get; set; }

        public int MaximumInventorySize { get; set; }

        public void ReceiveData(IAsyncResult result)
        {
            int bytesRead = this.Connection.EndReceive(result);

            if (bytesRead > 0)
            {
                // TODO: Parse data received by the user.
                var str = Encoding.UTF8.GetString(this.Buffer, 0, bytesRead);

                if (!this.initialConnection)
                    this.ProcessCommand(str);
                else
                    this.initialConnection = false;

                // Queue the next receive data.
                this.Connection.BeginReceive(this.Buffer, 0, this.BufferSize, SocketFlags.None, new AsyncCallback(this.ReceiveData), this);
            }
            else
            {
                this.Disconnect(result);
            }
        }

        public void Connect()
        {
            if (this.OnConnect != null)
            {
                Console.WriteLine("Player connected.");
                this.OnConnect(this, new EventArgs());
            }
        }

        public void Disconnect()
        {
            this.Connection.Close();
            this.Game.Server.Disconnect(this);
        }

        public void Disconnect(IAsyncResult e)
        {
            this.Connection.Close();
            this.Game.Server.Disconnect(this);
        }

        public bool ProcessCommand(byte[] data)
        {
            return this.ProcessCommand(Encoding.UTF8.GetString(data));
        }

        public bool ProcessCommand(string command)
        {
            var availableCommands = ScriptFactory.GetTypesWithInterface<IMudCommand>();

            if (availableCommands.Length == 0)
                return false;

            

            return true;
        }

        public bool GetNative(IMudCommand command)
        {
            return false;
        }

        public void SendMessage(byte[] data)
        {
            if (this.Connection == null || !this.Connection.Connected)
                return;

            lock (this.Connection)
            {
                this.Connection.Send(data, data.Length, SocketFlags.None);
            }
            return;
        }

        public void SendMessage(string message, bool newLine = true)
        {
            if (newLine)
                this.SendMessage(Encoding.UTF8.GetBytes(message + "\r"));
            else
                this.SendMessage(Encoding.UTF8.GetBytes(message));
        }

        public void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room)
        {
            throw new NotImplementedException();
        }

        public void Talk(string message, IMob target)
        {
            throw new NotImplementedException();
        }

        public void Talk(string message, IMob[] group)
        {
            throw new NotImplementedException();
        }
    }
}

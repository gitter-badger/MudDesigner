using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using WinPC.Engine.Abstract.Directors;
using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Core;
using WinPC.Engine.States;
using WinPC.Engine.Abstract.Core;

namespace WinPC.Engine.Directors
{
    public class ServerDirector : IServerDirector
    {

        public List<Thread> ConnectionThreads { get; private set; }
        public List<Player> ConnectedPlayers { get; private set; }

        public Dictionary<Player, Thread> ConnectedUsers { get; private set; }

        public IServer Server { get; set; }

        public ServerDirector(IServer server)
        {
            Server = server;
            ConnectedPlayers = new List<Player>(Server.MaxConnections);
            ConnectionThreads = new List<Thread>(Server.MaxConnections);
            ConnectedUsers = new Dictionary<Player, Thread>();
        }

        public void AddConnection(Socket connection)
        {
            //TODO: Allow support for custom Player Types from scripts.
            var player = new Player(new ConnectState(this), connection);
            ConnectedPlayers.Add(player);

            Thread userThread = new Thread(ReceiveDataThread);
            
            //TODO: Replace ConnectedPlayers and ConnectionThreads with ConnectedUsers
            ConnectedUsers.Add(player, userThread);

            ConnectionThreads.Add(userThread);
            var index = ConnectionThreads.Count - 1;

            player.Name = "Player";
            userThread.Start(player);
        }

        public void ReceiveDataThread(object player)
        {
            var connectedPlayer = (IPlayer)player;

            while (Server.Enabled)
            {
                connectedPlayer.CurrentState.Render(connectedPlayer);
                var command = connectedPlayer.CurrentState.GetCommand();
                command.Execute();
            }
        }

        public void DisconnectAll()
        {
            foreach (var player in ConnectedPlayers)
            {
                player.Disconnect();
            }

            foreach (var thread in ConnectionThreads)
            {
                thread.Abort();

            }

            ConnectedPlayers.Clear();
            ConnectionThreads.Clear();
        }


        public String RecieveInput(IPlayer player)
        {
            string input = String.Empty;

            while (true)
            {
                try
                {
                    byte[] buf = new byte[1];
                    
                    Int32 recved = player.Connection.Receive(buf);

                    if (recved > 0)
                    {
                        if (buf[0] == '\n' && player.Buffer.Count > 0)
                        {
                            if (player.Buffer[player.Buffer.Count - 1] == '\r')
                                player.Buffer.RemoveAt(player.Buffer.Count - 1);

                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                            input = enc.GetString(player.Buffer.ToArray());
                            player.Buffer.Clear();
                            //Return a trimmed string.
                            return input;
                        }
                        else
                            player.Buffer.Add(buf[0]);
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

        /// <summary>
        /// Returns a reference to the specified player if s/he is connected to the server.
        /// </summary>
        /// <param name="player">Name of the player to return</param>
        /// <returns></returns>
        public bool GetPlayer(string name, out IPlayer player)
        {
            var connectedPlayer = from p in ConnectedUsers
                                  where p.Key.Name == name
                                  select p.Key;

            player = connectedPlayer.First();

            return player == null ? true : false;
        }
    }
}
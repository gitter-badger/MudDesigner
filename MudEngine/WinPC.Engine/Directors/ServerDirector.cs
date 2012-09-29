using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using MudDesigner.Engine.Abstract.Directors;
using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Directors
{
    public class ServerDirector : IServerDirector
    {
        public Dictionary<IPlayer, Thread> ConnectedPlayers { get; private set; }

        public IServer Server { get; set; }

        public ServerDirector(IServer server)
        {
            Server = server;
            ConnectedPlayers = new Dictionary<IPlayer, Thread>(Server.MaxConnections);
        }

        public void AddConnection(Socket connection)
        {
            //TODO: Allow support for custom Player Types from scripts.
            var player = new Player();
            player.Initialize(new ConnectState(this), connection);
            
            Thread userThread = new Thread(ReceiveDataThread);

            ConnectedPlayers.Add(player, userThread);

            userThread.Start(player);
        }

        public void ReceiveDataThread(object player)
        {
            var connectedUser = (IPlayer)player;

            while (Server.Enabled)
            {
                if (!connectedUser.IsConnected)
                {
                    DisconnectPlayer(connectedUser);
                    break;
                }

                //When a user closes the telnet terminal, an exception can be generated
                //due to accessing a connection that no longer exists.  This will catch that
                //and ignore, letting the next loop iteration find the disconnect and process.
                try
                {
                    connectedUser.CurrentState.Render(connectedUser);
                    var command = connectedUser.CurrentState.GetCommand();
                    command.Execute();
                }
                catch(Exception ex)
                {
                    Logger.WriteLine(ex.Message);
                }
            }
        }

        private void DisconnectPlayer(IPlayer connectedUser)
        {
            Thread playerThread = ConnectedPlayers[connectedUser];
            ConnectedPlayers.Remove(connectedUser);

            connectedUser.Disconnect();
            connectedUser = null;
            playerThread.Abort();
        }

        public void DisconnectAll()
        {
            foreach (var player in ConnectedPlayers.Keys)
            {
                player.Disconnect();
                ConnectedPlayers[player].Abort(); //Kill the thread
            }

            ConnectedPlayers.Clear();
        }


        public String RecieveInput(IPlayer player)
        {
            string input = String.Empty;

            while (true)
            {
                try
                {
                    byte[] buf = new byte[1];

                    if (!player.Connection.Connected)
                        return "Disconnected.";

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
            var connectedPlayer = from p in ConnectedPlayers
                                  where p.Key.Name == name
                                  select p.Key;

            player = connectedPlayer.First();

            return player == null ? true : false;
        }
    }
}
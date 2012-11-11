using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;
 

namespace MudDesigner.Engine.Directors
{
    public class ServerDirector : IServerDirector
    {
        public Dictionary<IPlayer, Thread> ConnectedPlayers { get; private set; }

        public IServer Server { get; set; }

        public IState InitialConnectionState
        {
            get
            {
                IState state = (IState)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.LoginState, this);
                return state;
            }
        }

        public ServerDirector(IServer server)
        {
            Server = server;
            ConnectedPlayers = new Dictionary<IPlayer, Thread>(Server.MaxConnections);
        }

        public void AddConnection(Socket connection)
        {
            var player = (IPlayer)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.PlayerScript, null);
            player.Initialize(InitialConnectionState, connection);
            
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
                    Logger.WriteLine(connectedUser.Name + " disconnected.");
                    DisconnectPlayer(connectedUser);
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
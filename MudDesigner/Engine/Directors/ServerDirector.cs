/* ServerDirector
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Server Director is responsible for managing the user connections.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;
using log4net;

namespace MudDesigner.Engine.Directors
{
    /// <summary>
    /// Server Director is responsible for managing the user connections.
    /// </summary>
    public class ServerDirector : IServerDirector
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServerDirector)); 
        /// <summary>
        /// Gets a reference to the collection of players that are currently connected
        /// </summary>
        public Dictionary<IPlayer, Thread> ConnectedPlayers { get; private set; }

        /// <summary>
        /// The server that is running, allowing players to connect
        /// </summary>
        public IServer Server { get; set; }

        /// <summary>
        /// The initial state a player is in when he initially connects
        /// </summary>
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

        /// <summary>
        /// Adds a connected player to the server
        /// </summary>
        /// <param name="connection">Connected player using a .NET Socket</param>
        public void AddConnection(Socket connection)
        {
            var player = (IPlayer)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.PlayerScript, null);
            player.Initialize(InitialConnectionState, connection, this);
            
            Thread userThread = new Thread(ReceiveDataThread);

            ConnectedPlayers.Add(player, userThread);

            userThread.Start(player);
        }

        /// <summary>
        /// Receives data from the connected player. Should be run on its own thread.
        /// </summary>
        /// <param name="index"></param>
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
                    try
                    {
                        connectedUser.CurrentState.Render(connectedUser);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }

                    var command = connectedUser.CurrentState.GetCommand();
                    if (command == null)
                        continue;
                    else
                    {
                        try
                        {
                            command.Execute(connectedUser);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Log.Info(string.Format("{0} disconnected", connectedUser.Name));
                    Log.Error(string.Format("Exception occured! {0}", ex.Message));
                    //Logger.WriteLine(connectedUser.Name + " disconnected.");
                    DisconnectPlayer(connectedUser);
                }
            }
        }

        /// <summary>
        /// Disconnects all players from the server
        /// </summary>
        private void DisconnectPlayer(IPlayer connectedUser)
        {
            try
            {
                Thread playerThread = ConnectedPlayers[connectedUser];
                ConnectedPlayers.Remove(connectedUser);

                connectedUser.Disconnect();
                connectedUser = null;
                playerThread.Abort();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error when disconnecting players. {0}", ex.Message));
                
                
            }
        }

        public void DisconnectAll()
        {
            Log.Info("Disconnecting all users");
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MudEngine.Game;
using MudEngine.Game.Characters;

namespace MudEngine.Networking
{
    public class ConnectionManager
    {
        //Collection of currently connected players.
        internal List<Thread> _ConnectedThreads;
        internal List<StandardCharacter> _ConnectedCharacters;

        public ConnectionManager(Int32 maxPlayers)
        {
            this._ConnectedCharacters = new List<StandardCharacter>(maxPlayers);
            this._ConnectedThreads = new List<Thread>(maxPlayers);
        }

        /// <summary>
        /// Creates a new character for the player and sets it up on the server.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="connection"></param>
        public void AddConnection(StandardGame game, Socket connection)
        {
            //Instance a new character and provide it with the Socket.
            StandardCharacter character = new StandardCharacter(game, "New Player", "New networked client.",  connection);

            //Invoke the Characters Server connection method
            this._ConnectedCharacters.Add(character);
            this._ConnectedThreads.Add(new Thread(ReceiveDataThread));

            Int32 index = this._ConnectedThreads.Count - 1;
            this._ConnectedThreads[index].Name = character.Name;
            this._ConnectedThreads[index].Start(index);
        }

        public StandardCharacter GetConnectedCharacter(String characterName)
        {
            var v = from player in this._ConnectedCharacters
                    where characterName == player.Name.ToLower()
                    select player;

            return v.First();
        }
        
        public StandardCharacter[] GetConnectedCharacters()
        {
            return this._ConnectedCharacters.ToArray();
        }

        private void ReceiveDataThread(Object index)
        {
            StandardCharacter character = this._ConnectedCharacters[(Int32)index];

            character.Initialize();
            character.Connect();

            while (character.Game.Server.Status == ServerStatus.Running &&
                character.Connected)
            {
                try
                {
                    character.ExecuteCommand(character.GetInput());
                }
                catch
                {
                    RemoveConnection(character);
                }
            }

            RemoveConnection(character);
        }

        /// <summary>
        /// Removes the specified player character from the server.
        /// </summary>
        /// <param name="character"></param>
        public void RemoveConnection(StandardCharacter character)
        {
            character.Disconnect();
            foreach (StandardCharacter c in this._ConnectedCharacters)
            {
                if (c.ID == character.ID)
                {
                    Int32 index = _ConnectedCharacters.IndexOf(c);
                    this._ConnectedCharacters.Remove(character);
                    Thread t = this._ConnectedThreads[index];
                    this._ConnectedThreads.Remove(this._ConnectedThreads[index]);
                    t.Abort();
                }
            }
        }

        /// <summary>
        /// Disconnects all of the currently connected clients.
        /// </summary>
        public void DisconnectAll()
        {
            for (Int32 i = 0; i < this._ConnectedCharacters.Count; i++)
            {
                this._ConnectedCharacters[i].Disconnect();
                this._ConnectedThreads[i].Abort();
            }

            this._ConnectedCharacters.Clear();
            this._ConnectedThreads.Clear();
        }
    }
}

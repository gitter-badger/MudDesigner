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
    public static class ConnectionManager
    {
        //Collection of currently connected players.
        public static List<StandardCharacter> Connections { get; set; }

        /// <summary>
        /// Creates a new character for the player and sets it up on the server.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="connection"></param>
        public static void AddConnection(StandardGame game, Socket connection)
        {
            //Exception checking.
            if (Connections == null)
                Connections = new List<StandardCharacter>();

            //Instance a new character and provide it with the Socket.
            StandardCharacter character = new StandardCharacter("New Player", "New networked client.", game, connection);
            
            //Add it to the Connections collection
            Connections.Add(character);

            //Invoke the Characters Server connection method
            character.Connect();
        }

        /// <summary>
        /// Removes the specified player character from the server.
        /// </summary>
        /// <param name="character"></param>
        public static void RemoveConnection(StandardCharacter character)
        {
            Connections.Remove(character);
        }

        /// <summary>
        /// Disconnects all of the currently connected clients.
        /// </summary>
        public static void DisconnectAll()
        {
            if (Connections == null)
                return;

            foreach (StandardCharacter character in Connections)
            {
                character.Disconnect();
            }
            Connections.Clear();
        }
    }
}

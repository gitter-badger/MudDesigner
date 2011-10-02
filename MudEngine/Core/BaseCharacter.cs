using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MudEngine.Core
{
    /// <summary>
    /// Collection of Roles that can be assigned to a character, either controlled or non-controlled.
    /// </summary>
    public enum CharacterRoles
    {
        Admin,
        Immortal,
        GM,
        QuestGiver,
        Player,
        NPC
    }

    public struct CharacterStats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Experience { get; set; }
    }

    public abstract class BaseCharacter : BaseObject, ICharacter
    {
        private TcpClient _ConnectedClient;

        /// <summary>
        /// Gets the currently active game.
        /// </summary>
        [Browsable(false)]
        public BaseGame ActiveGame { get; private set; }

        /// <summary>
        /// Gets or Sets the Role that this character has in the game.
        /// </summary>
        [Browsable(false)]
        public CharacterRoles Role { get; set; }

        /// <summary>
        /// Gets the current stats for this character.
        /// </summary>
        [Browsable(false)]
        public CharacterStats Stats { get; protected set; }

        /// <summary>
        /// Gets or Sets the password used to log into this character
        /// </summary>
        [Browsable(false)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets if this character can move about the world or not.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Gets if this character is controlled by Human or AI
        /// </summary>
        public bool IsAI { get; private set; }

        /// <summary>
        /// Gets the current location of this character within the world.
        /// </summary>
        public IEnvironment CurrentLocation { get; private set; }

        public BaseCharacter(BaseGame game)
        {
            this.ActiveGame = game;
            this.ID = this.ActiveGame.GetAvailableID();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void OnConnect(TcpClient client)
        {
            this._ConnectedClient = client;

            NetworkStream clientStream = this._ConnectedClient.GetStream();

            //Stores messages recieved from the client
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break; //error occured.  Re-try
                }

                if (bytesRead == 0)
                {
                    break; //Disconnected.
                }

                ASCIIEncoding encoder = new ASCIIEncoding();
                string data = encoder.GetString(message, 0, bytesRead);
                //RecieveData(data);
            }

            //TODO: Call ActiveGame.OnDisconnect();
            //newClient.Close(); //Disconnect the client.
        }

        public void OnTravel(World.AvailableTravelDirections travelDirection)
        {
            throw new NotImplementedException();
        }

        public void OnTalk(string message, ICharacter instigator)
        {
            throw new NotImplementedException();
        }
    }
}

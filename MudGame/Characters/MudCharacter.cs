using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using MudEngine.Core;

namespace MudGame.Characters
{
    public class MudCharacter : BaseCharacter
    {
        private TcpClient client;

        public MudCharacter(BaseGame game)
            : base(game)
        {
            this.Role = CharacterRoles.NPC;
        }
        
        public override void OnConnect(TcpClient client)
        {
            this.client = client;

        }

        public override void OnTravel(MudEngine.World.AvailableTravelDirections travelDirection)
        {
            throw new NotImplementedException();
        }

        public override void Send(string message)
        {
            NetworkStream ns = this.client.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] buffer = encoder.GetBytes(message);

            ns.Write(buffer, 0, buffer.Length);
            ns.Flush();
        }

        public override void OnTalk(string message, ICharacter instigator)
        {
            if (instigator == this)
            {
                
            }
        }
    }
}

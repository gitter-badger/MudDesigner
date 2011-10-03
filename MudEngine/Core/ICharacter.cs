using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using MudEngine.World;

namespace MudEngine.Core
{
    public interface ICharacter : IGameComponent
    {
        CharacterRoles Role { get; set; }
        string Password { get; set; }
        bool IsStatic { get; set; }
        bool IsAI { get; }
        IEnvironment CurrentLocation { get; }

        void OnTravel(AvailableTravelDirections travelDirection);
        void OnTalk(string message, ICharacter instigator);
        void OnConnect(TcpClient client);
        void Send(string message);
    }
}

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Core
{
    public class Player : EnginePlayer
    {
        private Guid Id { get; set; }
        // @ToDO: I will probably normalize this into a PlayerDetails class...
        public string Username { get; set; }
        public string Password { get; set; } // i dont like this but its just here temporarily

        public string CharacterName { get; set; }
        public string Class { get; set; } // ... really need to code up a design spec

        public override void Initialize(IState initialState, Socket connection)
        {
            this.Connection = connection;
            initialState.Render(this);
        }
    }
}
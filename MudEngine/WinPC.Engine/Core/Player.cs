using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Core
{
    public class Player : EnginePlayer
    {
        private Guid Id { get; set; }
        //TODO: I will probably normalize this into a PlayerDetails class... - MC
        public string Username { get; set; }
        //TODO: I don't even think we need a Password property at all.  It can be stored in the saved file
        //and when the user enters the password via telnet, it checks against the saved file.
        //No need to store it at all inside of any Types - JS.
        public string Password { get; set; } // i dont like this but its just here temporarily - MC

        public string CharacterName { get; set; }
        public string Class { get; set; } // ... really need to code up a design spec - MC

        public override void Initialize(IState initialState, Socket connection)
        {
            this.Connection = connection;
            initialState.Render(this);
        }
    }
}
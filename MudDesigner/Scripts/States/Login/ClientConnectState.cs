using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Mobs;
using MudDesigner.Scripts.States.CreateCharacter;

namespace MudDesigner.Scripts.States.Login
{
    public class ClientConnectState : IState
    {
        private ServerDirector director;
        private IPlayer connectedPlayer;

        public ClientConnectState(ServerDirector serverDirector)
        {
            director = serverDirector;
        }

        public void Render(Engine.Mobs.IPlayer player)
        {
            //Store a reference for the GetCommand() method to use.
            connectedPlayer = player;

            //Output the game information
            player.SendMessage(director.Server.Game.Name);
            player.SendMessage(director.Server.Game.Description);

            //Output the server MOTD
            player.SendMessage(director.Server.MOTD);
            player.SendMessage(string.Empty); //blank line

            //Switch the the Login state for the player
            player.SwitchState(new ClientLoginState(director));
        }

        public Engine.Commands.ICommand GetCommand()
        {
            //No commands are accepted at this point.
            return new NoOpCommand();
        }
    }
}

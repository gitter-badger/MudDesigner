using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.States.ClientLogin
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
            connectedPlayer = player;

            player.SendMessage(director.Server.Game.Name);
            player.SendMessage(director.Server.Game.Description);
            player.SendMessage(string.Empty); //blank line.

            player.SendMessage(director.Server.MOTD);
            player.SendMessage(string.Empty); //blank line

            player.SwitchState(new ClientLoginState(director));
        }

        public Engine.Commands.ICommand GetCommand()
        {
            return new NoOpCommand(connectedPlayer.Connection);
        }
    }
}

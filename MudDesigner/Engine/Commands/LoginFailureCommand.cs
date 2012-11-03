using System.Net.Sockets;
using System.Text;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    public class LoginFailureCommand : ICommand
    {
        private IPlayer player;

        public LoginFailureCommand(IPlayer connectedPlayer)
        {
            player = connectedPlayer;
        }

        public void Execute()
        {
            player.SendMessage("Login Failure! <@This is a todo item>");
        }
    }
}
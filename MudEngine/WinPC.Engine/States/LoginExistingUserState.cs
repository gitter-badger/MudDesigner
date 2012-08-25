using System;
using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Commands;
using WinPC.Engine.Directors;
using WinPC.Engine.Core;

namespace WinPC.Engine.States
{
    public class LoginExistingUserState : IState
    {
        public ServerDirector Director { get; private set; }

        private Socket connection;
        private ASCIIEncoding encoding;
        private IPlayer player;

        public LoginExistingUserState(ServerDirector director)
        {
            Director = director;
            encoding = new ASCIIEncoding();

        }
        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            player = connectedPlayer;

            player.SendMessage("Enter your name: ");
        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(player);

            if (String.IsNullOrEmpty(input))
            {
                player.SendMessage("Invalid username.");
                player.SendMessage("Enter your name: ");
            }
            else
            {
                return new SwitchStateCommand(Director, new MainMenuState(Director), player);
            }

            return new InvalidCommand(connection);
        }
    }
}
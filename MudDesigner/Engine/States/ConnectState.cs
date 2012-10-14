using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.States
{
    public class ConnectState : IState
    {
        public ServerDirector Director { get; private set; }
        private int IsLoggingin { get; set; }

        private Socket connection;
        private ASCIIEncoding encoding;
        private IPlayer player;

        public ConnectState(ServerDirector director)
        {
            Director = director;
            encoding = new ASCIIEncoding();
        }

        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            player = connectedPlayer;

            player.SendMessage("Welcome to AllocateThis's Mud Engine!" + "\n\r");
            player.SendMessage(Director.Server.MOTD + "\n\r");

            //Now that the player is connected, start the login process.
            player.SwitchState(new LoginState(Director));
        }
        public ICommand GetCommand()
        {
            /*
            var input = Director.RecieveInput(player);

            if (input == "menu")
            {
                return new SwitchStateCommand(Director, new MainMenuState(Director), player);
            }
            */
            return new NoOpCommand(connection);
        }
    }
}
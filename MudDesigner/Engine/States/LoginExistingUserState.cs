using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.States
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
                IState state = (IState)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.LoginSuccessState, Director);
                return new SwitchStateCommand(Director, state, player);
            }

            return new InvalidCommand(connection);
        }
    }
}
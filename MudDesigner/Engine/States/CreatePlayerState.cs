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
    public class CreatePlayerState : IState
    {
        public ServerDirector Director { get; private set; }

        private Socket connection;
        private ASCIIEncoding encoding;
        private IPlayer _player;

        public CreatePlayerState(ServerDirector director, IPlayer player)
        {
            Director = director;
            encoding = new ASCIIEncoding();
            _player = player;

        }
        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            _player = connectedPlayer;

            _player.SendMessage("Could not detect any previous made characters, please enter a character name: ");
        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(_player);

            if (string.IsNullOrWhiteSpace(input))
                return new InvalidCommand(_player.Connection);  // FYI we can turn this into InvalidCharacterNameCommand or something.

            var player = _player as Player;
            if(player != null)
            {
                player.CharacterName = input;
                IState state = (IState)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.LoginSuccessState, Director);
                return new SwitchStateCommand(Director, state, _player);
            }

            // Otherwise lets return invalid.
            return new InvalidCommand(connection);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.States;

namespace MudDesigner.Scripts.States.ClientLogin
{
    public class ClientLoginState : IState
    {
        private ServerDirector director;
        private IPlayer connectedPlayer;

        private enum CurrentState
        {
            EnteringName,
            EnteringPassword,
            CreatingNewCharacter,
        }
        private CurrentState currentState;

        public ClientLoginState(ServerDirector serverDirector)
        {
            director = serverDirector;
        }

        public void Render(IPlayer player)
        {
            connectedPlayer = player;

            switch(currentState)
            {
                case CurrentState.EnteringName:
                    player.SendMessage("What is your name, adventurer?", false);
                    currentState = CurrentState.EnteringName;
                    break;
            }
        }

        public ICommand GetCommand()
        {
            switch (currentState)
            {
                case CurrentState.EnteringName:
                    var input = director.RecieveInput(connectedPlayer);
                    if (!ValidateName(input))
                    {
                        connectedPlayer.SendMessage("You have entered an invalid name, please try again!");
                        return new NoOpCommand(connectedPlayer.Connection);
                    }

                    break;
            }

            return new NoOpCommand(connectedPlayer.Connection);
        }

        private bool ValidateName(string userName)
        {
            return !string.IsNullOrEmpty(userName);
        }
    }
}

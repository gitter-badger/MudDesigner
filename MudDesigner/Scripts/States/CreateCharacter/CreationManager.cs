using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.States;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Commands;

namespace MudDesigner.Scripts.States.CreateCharacter
{
    public class CreationManager : IState
    {
        private IPlayer connectedPlayer;
        private ServerDirector director;

        public enum CreationState
        {
            CharacterCreation,
            GenderSelect,
            Completed,
        }
        private CreationState currentCreationState;

        public CreationManager(ServerDirector serverDirector, CreationState initialState)
        {
            currentCreationState = initialState;
            director = serverDirector;
        }

        public void Render(Engine.Mobs.IPlayer player)
        {
            connectedPlayer = player;
        }

        public Engine.Commands.ICommand GetCommand()
        {
            switch (currentCreationState)
            {
                case CreationState.CharacterCreation:
                    connectedPlayer.SwitchState(new CreateNewCharacter(director));
                    break;
                case CreationState.GenderSelect:
                    connectedPlayer.SwitchState(new GenderSelect(director));
                    break;
                case CreationState.Completed:
                    connectedPlayer.SendMessage("You have completed the character creation process! Enjoy your stay in our world!");
                    connectedPlayer.SendMessage(string.Empty);//blank line.

                    //Display the main menu
                    connectedPlayer.SwitchState(new MainMenuState(director));
                    break;
            }

            return new NoOpCommand(connectedPlayer.Connection);
        }
    }
}

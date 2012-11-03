using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.States;

namespace MudDesigner.Scripts.States.CreateCharacter
{
    public class CreateNewCharacter : IState
    {
        private ServerDirector director;
        private IPlayer connectedPlayer;

        //Used to manage the state of our login in a more readable manor
        private enum CurrentState
        {
            InitialWelcome,
            EnteringPasswordFirst,
            EnteringPasswordSecond,
        }
        private CurrentState currentState;

        private string firstPassword, secondPassword;

        public CreateNewCharacter(ServerDirector serverDirector)
        {
            director = serverDirector;
            currentState = CurrentState.InitialWelcome;
        }

        public void Render(IPlayer player)
        {
            connectedPlayer = player;

            switch (currentState)
            {
                case CurrentState.InitialWelcome:
                    {
                        player.SendMessage("Welcome " + player.Name + "!");
                        break;
                    }
                case CurrentState.EnteringPasswordFirst:
                    {
                        player.SendMessage("Please provide a password for your character: ", false);
                        break;
                    }
                case CurrentState.EnteringPasswordSecond:
                    {
                        player.SendMessage("Please re-enter your password for this character: ", false);
                        break;
                    }
            }
        }

        public ICommand GetCommand()
        {
            //Check which state we are in
            switch (currentState)
            {
                    //If we are at the initial welcome for the new character, just change the state and move on
                case CurrentState.InitialWelcome:
                    currentState = CurrentState.EnteringPasswordFirst;
                    break;
                    //We need to get the first out of two password entries and make sure it's legal.
                case CurrentState.EnteringPasswordFirst:
                    {
                        GetPassword(PasswordPhase.FirstPassword);
                        break;
                    }
                case CurrentState.EnteringPasswordSecond:
                    {
                        GetPassword(PasswordPhase.SecondPassword);
                        break;
                    }
            }

            return new NoOpCommand();
        }

        //Used by the GetPassword method
        private enum PasswordPhase
        {
            FirstPassword,
            SecondPassword
        }
        private void GetPassword(PasswordPhase password)
        {
            //Get the users input.
            var input = director.RecieveInput(connectedPlayer);

            //See if the password is empty
            if (string.IsNullOrEmpty(input))
            {
                connectedPlayer.SendMessage("Your password is invalid, please try again.");
                return;
            }

            //Make sure the length of the password meets the minimum requirement of the server
            if (input.Length < director.Server.MinimumPasswordSize)
            {
                connectedPlayer.SendMessage("Your password must be at least " + director.Server.MinimumPasswordSize + " characters long.");
                return;
            }

            //All things checkout, save the password for later.
            if (password == PasswordPhase.FirstPassword)
            {
                firstPassword = input;
                currentState = CurrentState.EnteringPasswordSecond;
            }
            else
            {
                secondPassword = input;

                if (firstPassword != secondPassword)
                {
                    connectedPlayer.SendMessage("The two passwords you entered do not match! Please try again.");
                    currentState = CurrentState.EnteringPasswordFirst;
                    return;
                }

                //We have the passwords verified and the username stored so lets finish up creating the character
                //and pass it on to the character customization states
                FinishSetup();
            }
        }

        private void FinishSetup()
        {
            //We use connectedPlayer.Name here, but could be something like connectedPlayer.Account etc.
            //connectedPlayer.CreateAuthorization(connectedPlayer.Name, firstPassword);
            //connectedPlayer.Save();

            connectedPlayer.SwitchState(new CreationManager(director, CreationManager.CreationState.GenderSelect));
        }
    }
}

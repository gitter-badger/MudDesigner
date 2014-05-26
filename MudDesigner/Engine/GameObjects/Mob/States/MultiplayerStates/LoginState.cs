using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Commands;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.Networking;
using MudEngine.Engine.GameObjects.Mob.States;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob.States.MultiplayerStates
{
    public class LoginState : IState
    {
        /// <summary>
        /// The connected player
        /// </summary>
        private IPlayer connectedPlayer;

        private enum CurrentState
        {
            FetchUserName,
            FetchPassword,
            InvalidUser,
        }

        private CurrentState currentState;

        public void Render(IMob mob)
        {
            if (!(mob is IPlayer))
            {
                throw new NullReferenceException("ConnectState can only be used with a player object implementing IPlayer");
            }

            //Store a reference for the GetCommand() method to use.
            this.connectedPlayer = mob as IPlayer;
            var server = mob.Game as IServer;
            this.connectedPlayer.ReceivedMessage += connectedPlayer_ReceivedMessage;

            if (server == null)
            {
                throw new NullReferenceException("LoginState can only be set to a player object that is part of a server.");
            }

            this.currentState = CurrentState.FetchUserName;

            switch (this.currentState)
            {
                case CurrentState.FetchUserName:
                    this.connectedPlayer.Send(new InputMessage("Please enter your user name"));
                    break;
                case CurrentState.FetchPassword:
                    this.connectedPlayer.Send(new InputMessage("Please enter your password"));
                    break;
                case CurrentState.InvalidUser:
                    this.connectedPlayer.Send(new InformationalMessage("Invalid username/password specified."));
                    this.currentState = CurrentState.FetchUserName;
                    this.connectedPlayer.Send(new InputMessage("Please enter your user name"));
                    break;
            }
        }

        void connectedPlayer_ReceivedMessage(object sender, IMessage e)
        {
            ICommand command = this.GetCommand(e);

            // Be good memory citizens and clean ourself up after receiving a message.
            // Not doing this results in duplicate events being registered and memory leaks.
            this.connectedPlayer.ReceivedMessage -= connectedPlayer_ReceivedMessage;
        }

        public Commands.ICommand GetCommand(IMessage command)
        {
            if (this.currentState == CurrentState.FetchUserName)
            {
                this.connectedPlayer.Name = command.Message;
                this.currentState = CurrentState.FetchPassword;
            }
            else if (this.currentState == CurrentState.FetchPassword)
            {
                // find user
            }

            return new NoOpCommand();
        }
    }
}

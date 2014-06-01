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
            None,
            FetchUserName,
            FetchPassword,
            InvalidUser,
        }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        public bool IsCompleted { get; set; }

        private CurrentState currentState;

        /// <summary>
        /// Renders the current state to the players terminal.
        /// </summary>
        /// <param name="mob"></param>
        /// <exception cref="System.NullReferenceException">
        /// ConnectState can only be used with a player object implementing IPlayer
        /// or
        /// LoginState can only be set to a player object that is part of a server.
        /// </exception>
        public void Render(IMob mob)
        {
            if (!(mob is IPlayer))
            {
                throw new NullReferenceException("ConnectState can only be used with a player object implementing IPlayer");
            }

            //Store a reference for the GetCommand() method to use.
            this.connectedPlayer = mob as IPlayer;
            var server = mob.Game as IServer;

            // Register to receive new input from the user.
            mob.ReceivedMessage += connectedPlayer_ReceivedMessage;

            if (server == null)
            {
                throw new NullReferenceException("LoginState can only be set to a player object that is part of a server.");
            }

            if (this.currentState == CurrentState.None)
            {
                this.currentState = CurrentState.FetchUserName;
            }

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

        /// <summary>
        /// Receives the players input.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void connectedPlayer_ReceivedMessage(object sender, IMessage e)
        {
            // Be good memory citizens and clean ourself up after receiving a message.
            // Not doing this results in duplicate events being registered and memory leaks.
            this.connectedPlayer.ReceivedMessage -= connectedPlayer_ReceivedMessage;
            
            ICommand command = this.UpdateState(e);
        }

        /// <summary>
        /// Gets the Command that the player entered and preps it for execution.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the ICommand specified.</returns>
        public Commands.ICommand UpdateState(IMessage command)
        {
            if (this.currentState == CurrentState.FetchUserName)
            {
                this.connectedPlayer.Name = command.Message;
                this.currentState = CurrentState.FetchPassword;
            }
            else if (this.currentState == CurrentState.FetchPassword)
            {
                this.currentState = CurrentState.InvalidUser;
            }

            return new NoOpCommand();
        }

        /// <summary>
        /// Cleanups this instance during a state change.
        /// </summary>
        public void Cleanup()
        {
            // If we have a player instance, we clean up the registered event.
            if (this.connectedPlayer != null)
            {
                this.connectedPlayer.ReceivedMessage -= this.connectedPlayer_ReceivedMessage;
            }
        }
    }
}

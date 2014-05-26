using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudEngine.Engine.Core;
using MudEngine.Engine.Commands;
using MudEngine.Engine.GameObjects.Mob.States;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.Networking;

namespace MudEngine.Engine.GameObjects.Mob.States.MultiplayerStates
{
    /// <summary>
    /// A state that the player enters upon first starting or connecting to the game.
    /// This state is compatible with both online and offline game play.
    /// </summary>
    public class ConnectState : IState
    {
        /// <summary>
        /// The connected player
        /// </summary>
        private IMob connectedPlayer;

        public void Render(IMob mob)
        {
            if (!(mob is IPlayer))
            {
                throw new NullReferenceException("ConnectState can only be used with a player object implementing IPlayer");
            }

            //Store a reference for the GetCommand() method to use.
            this.connectedPlayer = mob as IPlayer;
            var server = mob.Game as IServer;

            if (server == null)
            {
                throw new NullReferenceException("LoginState can only be set to a player object that is part of a server.");
            }

            //Output the game information
            this.connectedPlayer.Send(new InformationalMessage(this.connectedPlayer.Game.Name));
            this.connectedPlayer.Send(new InformationalMessage(this.connectedPlayer.Game.Description));

            //Output the server MOTD
            server.MessageOfTheDay.ForEach(message => this.connectedPlayer.Send(new InformationalMessage(message)));
            this.connectedPlayer.Send(new InformationalMessage(string.Empty)); //blank line

            this.connectedPlayer.StateManager.SwitchState(new LoginState());
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns no operation required.</returns>
        public Commands.ICommand GetCommand(IMessage message)
        {
            return new NoOpCommand();
        }
    }
}

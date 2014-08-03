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

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        public bool IsCompleted { get; set; }

        public void Render(IMob mob)
        {
            if (!(mob is IPlayer))
            {
                throw new NullReferenceException("ConnectState can only be used with a player object implementing IPlayer");
            }

            //Store a reference for the GetCommand() method to use.
            this.connectedPlayer = mob as IPlayer;
            var server = mob.Game as IServer;
            var game = mob.Game as IGame;

            // It is not guaranteed that mob.Game will implement IServer. We are only guaranteed that it will implement IGame.
            if (server == null)
            {
                throw new NullReferenceException("LoginState can only be set to a player object that is part of a server.");
            }

            //Output the game information
            mob.Send(new InformationalMessage(game.Name));
            mob.Send(new InformationalMessage(game.Description));
            mob.Send(new InformationalMessage(string.Empty)); //blank line
            
            //Output the server MOTD information
            mob.Send(new InformationalMessage(string.Join("\n", server.MessageOfTheDay)));
            mob.Send(new InformationalMessage(string.Empty)); //blank line

            this.connectedPlayer.StateManager.SwitchState<LoginState>();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns no operation required.</returns>
        public Commands.ICommand UpdateState(IMessage message)
        {
            return new NoOpCommand();
        }

        /// <summary>
        /// Cleanups this instance during a state change.
        /// </summary>
        public void Cleanup()
        {
            // We have nothing to clean up.
            return;
        }
    }
}

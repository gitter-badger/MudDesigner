using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Commands;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob.States
{
    /// <summary>
    /// Manages the state of an object.
    /// </summary>
    public class StateManager
    {
        /// <summary>
        /// Gets the current state.
        /// </summary>
        public IState CurrentState { get; private set; }

        /// <summary>
        /// Gets the states available for use.
        /// </summary>
        /// <value>
        public List<IState> States { get; private set; }

        /// <summary>
        /// Gets the commands available.
        /// </summary>
        public List<ICommand> Commands { get; private set; }

        /// <summary>
        /// Gets the mob that this manager controls the state of.
        /// </summary>
        public IMob Mob { get; private set; }

        public void Initialize(IMob mob, IState initialState = null)
        {
            this.Mob = mob;

            if (initialState != null)
            {
                this.CurrentState = initialState;
                this.CurrentState.Render(this.Mob);
            }
        }

        /// <summary>
        /// Performs the command.
        /// </summary>
        /// <param name="message">The message.</param>
        public void PerformCommand(IMessage message)
        {
            if (this.CurrentState != null)
            {
                ICommand command = this.CurrentState.GetCommand(message);
                if (command is NoOpCommand)
                {
                    // NoOperation commands indicate that the current state is not finished yet.
                    this.CurrentState.Render(this.Mob);
                }
                else if (command != null)
                {
                    command.Execute(this.Mob);
                }
                else if (command == null)
                {
                    new InvalidCommand().Execute(this.Mob);
                }
            }
        }

        public void SwitchState(IState state)
        {
            this.CurrentState = state;
            this.CurrentState.Render(this.Mob);
        }
    }
}

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
        /// Gets the states.
        /// </summary>
        public Stack<IState> States { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        public IState CurrentState { get; private set; }

        /// <summary>
        /// Gets the mob that this manager controls the state of.
        /// </summary>
        public IMob Mob { get; private set; }

        /// <summary>
        /// Initializes the specified mob.
        /// </summary>
        /// <param name="mob">The mob.</param>
        /// <param name="initialState">The initial state.</param>
        public void Initialize(IMob mob)
        {
            this.Mob = mob;
            this.States = new Stack<IState>();
        }

        /// <summary>
        /// Performs the command.
        /// </summary>
        /// <param name="message">The message.</param>
        public void PerformCommand(IMessage message)
        {
            if (this.CurrentState != null)
            {
                this.CurrentState.UpdateState(message);

                if (!this.CurrentState.IsCompleted)
                {
                    this.CurrentState.Render(this.Mob);
                }
                else
                {
                    // Restore the previous state.
                    this.CurrentState = this.States.Pop();
                }
            }
        }

        /// <summary>
        /// Switches the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void SwitchState<T>(bool preserveCurrentState = false) where T : class, IState, new()
        {
            if (this.CurrentState != null)
            {
                this.CurrentState.Cleanup();

                if (preserveCurrentState)
                {
                    this.States.Push(this.CurrentState);
                }
            }

            this.CurrentState = new T();
            this.CurrentState.Render(this.Mob);
        }
    }
}

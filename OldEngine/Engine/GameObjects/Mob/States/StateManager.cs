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
        /// Gets or sets commands with their shorthand values.
        /// </summary>
        private Dictionary<ShorthandNameAttribute, ICommand> shorthandCommands;

        /// <summary>
        /// The command stack.
        /// </summary>
        private Stack<ICommand> previousCommands;

        /// <summary>
        /// The current command
        /// </summary>
        private ICommand currentCommand;

        /// <summary>
        /// Gets the states.
        /// </summary>
        public Stack<IState> States { get; private set; }

        /// <summary>
        /// Gets the commands for this user.
        /// </summary>
        public IEnumerable<ICommand> Commands { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        public IState CurrentState { get; private set; }

        /// <summary>
        /// Gets the mob that this manager controls the state of.
        /// </summary>
        public IMob Mob { get; private set; }

        public StateManager()
        {
            this.shorthandCommands = new Dictionary<ShorthandNameAttribute, ICommand>();
            this.previousCommands = new Stack<ICommand>();
            this.States = new Stack<IState>();
        }

        /// <summary>
        /// Initializes the specified mob.
        /// </summary>
        /// <param name="mob">The mob.</param>
        /// <param name="initialState">The initial state.</param>
        public void Initialize(IMob mob, IEnumerable<ICommand> commands)
        {
            this.Mob = mob;
            this.States = new Stack<IState>();
            this.Commands = commands;

            var commandsWithShorthand = commands.Where(command =>
                Attribute.IsDefined(command.GetType(), typeof(ShorthandNameAttribute)));

            foreach(ICommand command in commandsWithShorthand)
            {
                ShorthandNameAttribute shorthandAttribute =
                    Attribute.GetCustomAttribute(command.GetType(), typeof(ShorthandNameAttribute)) as ShorthandNameAttribute;
                
                if (!this.shorthandCommands.Keys.Any(key => 
                    key.Shorthand.ToLower() == shorthandAttribute.Shorthand ||
                    key.Command == shorthandAttribute.Command))
                {
                    this.shorthandCommands.Add(shorthandAttribute, command);
                }
            }
        }

        /// <summary>
        /// Performs the command.
        /// </summary>
        /// <param name="message">The message.</param>
        public void PerformCommand(IMessage message)
        {
            string inputCommand = message.FormatMessage();
            var commands = this.Commands.Where(command =>
                command.GetType().Name.ToLower().StartsWith(inputCommand)).ToList();
            ICommand validCommand = null;

            // We do not care about this anymore. If a command does not have a ShorthandNameAttribute
            // it can not be executed by the user. Only the engine can execute non-attribute decorated commands.
            /*
            if (commands.Count != 0)
            {
                // See if we can find the command by naming convention
                try
                {
                    validCommand = commands.FirstOrDefault();
                }
                catch(Exception)
                {
                    throw;
                }
            }
            else */
            if (this.shorthandCommands.Keys.Any(key => 
                key.Shorthand.ToLower() == inputCommand ||
                key.Command.ToLower() == inputCommand))
            {
                // If we get this far, then commands is empty.
                // Find a shorthand command.
                try
                {
                    validCommand = this.shorthandCommands.FirstOrDefault(key =>
                        key.Key.Command.ToLower() == inputCommand ||
                        key.Key.Shorthand.ToLower() == inputCommand).Value as ICommand;
                }
                catch(Exception)
                {
                    throw;
                }
            }
            else if (this.previousCommands.Count > 0)
            {
                validCommand = this.previousCommands.Pop();
            }
            else
            {
                validCommand = new InvalidCommand();
            }

            this.currentCommand = validCommand;
            validCommand.Execute(this.Mob, inputCommand);

            // Check if the command needs to continue executing.
            if (validCommand.IsIncomplete)
            {
                this.previousCommands.Push(validCommand);
            }

            this.currentCommand = validCommand;
        }

        /// <summary>
        /// Switches the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void SwitchState<T>() where T : class, IState, new()
        {
            this.CurrentState = new T();
            this.CurrentState.Render(this.Mob);
        }
    }
}

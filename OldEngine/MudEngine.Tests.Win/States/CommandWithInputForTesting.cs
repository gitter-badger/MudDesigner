using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Commands;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.GameObjects.Mob.States;

namespace MudEngine.Tests.Win.States
{
    public class CommandWithInputForTesting : ICommand
    {
        public string CommandInput { get; private set; }

        public bool IsIncomplete { get; private set; }

        public void Execute(IMob mob, string input)
        {
            if (!this.IsIncomplete)
            {
                mob.Send(new InformationalMessage(string.Format("You entered {0}. More info is required.", input)));
                this.CommandInput = input;
                this.IsIncomplete = true;
                mob.StateManager.SwitchState<ReceivingInputState>();
            }
            else
            {
                this.IsIncomplete = false;
                mob.Send(new InformationalMessage(string.Format("Your {0} data is associated with {1}", this.CommandInput, input)));
                mob.StateManager.SwitchState<TestState>();
            }
        }
    }
}

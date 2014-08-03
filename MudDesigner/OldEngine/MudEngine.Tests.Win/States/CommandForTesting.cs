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
    [ShorthandName("MyTestCommand", "MTC")]
    public class CommandForTesting : ICommand
    {
        public string CommandInput { get; private set; }

        public bool IsIncomplete { get; private set; }

        public void Execute(IMob mob, string input)
        {
            mob.StateManager.SwitchState<TestState>();
        }
    }
}

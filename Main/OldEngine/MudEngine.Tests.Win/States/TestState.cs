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
    public class TestState : IState
    {
        public bool IsCompleted { get; private set; }

        public void Render(IMob mob)
        {
            mob.Send(new InformationalMessage("Message from TestState."));
        }

        public ICommand UpdateState(IMessage command)
        {
            throw new NotImplementedException();
        }

        public void Cleanup()
        {
            throw new NotImplementedException();
        }
    }
}

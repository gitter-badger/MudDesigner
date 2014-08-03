using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.GameObjects.Mob.States
{
    public class ReceivingInputState : IState
    {
        public bool IsCompleted { get; private set; }

        public void Render(IMob mob)
        {
            return;
        }

        public Commands.ICommand UpdateState(Core.IMessage command)
        {
            throw new NotImplementedException();
        }

        public void Cleanup()
        {
            throw new NotImplementedException();
        }
    }
}

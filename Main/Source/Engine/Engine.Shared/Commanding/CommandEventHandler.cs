using Mud.Engine.Shared.Character;
using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Commanding
{
    public class CommandEventHandler : EventArgs
    {
        public CommandEventHandler(ICharacter sender, IMessage message, bool handled)
        {
            this.Handled = handled;
            this.Message = message;
            this.Invoker = sender;
        }

        public IMessage Message { get; private set; }

        public bool Handled { get; private set; }

        public ICharacter Invoker { get; private set; }

        public void SetAsHandled()
        {
            this.Handled = true;
        }
    }
}

using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Engine.Runtime.Core
{
    public class MessageFixture : IMessage
    {
        public string Message
        {
            get { return "Fixture Message"; }
        }
    }
}

using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Tests.Engine.Core.Fixtures
{
    public class MessageFixture : IMessage
    {
        public MessageFixture() : this("")
        {
        }

        public MessageFixture(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}

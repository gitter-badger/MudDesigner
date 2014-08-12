using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    public interface ILogger
    {
        void Log<TMessage>(TMessage message) where TMessage : IMessage, new();
    }
}
